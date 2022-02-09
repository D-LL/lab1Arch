using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sysCollision : ISystem
{
    public string Name { get; }
    public sysCollision()
    {
        Name = "sysCollision";
    }
    public void UpdateSystem()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        //handle collision within circles
        {
            //get entities with a position and collidable(all circles)
            List<EntityComponent>[] req = { (((Position)World.world["Position"]).entities), (((Collidable)World.world["Collidable"]).entities) };
            List<EntityComponent> entities = World.getIntersect(req);
            //get entities with also speed (dynamic circle)
            List<EntityComponent>[] req2 = { entities, (((Speed)World.world["Speed"]).entities) };
            List<EntityComponent> dynEntities = World.getIntersect(req2);
            //detect collisions between dynamic and all
            foreach (EntityComponent e in dynEntities)
            {
                int idxPos = (((Position)World.world["Position"]).entities).IndexOf(e);
                int idxSize = (((Size)World.world["Size"]).entities).IndexOf(e);
                int idxSpeed = (((Speed)World.world["Speed"]).entities).IndexOf(e);
                Vector2 pos = (((Position)World.world["Position"]).position)[idxPos];
                float size = (((Size)World.world["Size"]).size)[idxSize];
                foreach(EntityComponent f in entities)
                {
                    int idxPosf = (((Position)World.world["Position"]).entities).IndexOf(f);
                    int idxSizef = (((Size)World.world["Size"]).entities).IndexOf(f);
                    Vector2 posf = (((Position)World.world["Position"]).position)[idxPosf];
                    float sizef = (((Size)World.world["Size"]).size)[idxSizef];
                    //detect collision
                    if (e.id != f.id && (pos-posf).magnitude < (size + sizef)/2)
                    {
                        //change size
                        (((Size)World.world["Size"]).size)[idxSize] *= 2;
                        size *= 2;
                        //if size max remove collidable (entities list already copied so equivalent to using a command buffer)
                        if (size >= ECSManager.Instance.Config.maxSize)
                        {
                            (((Size)World.world["Size"]).size)[idxSize] = ECSManager.Instance.Config.maxSize;
                            size = ECSManager.Instance.Config.maxSize;
                            (((Collidable)World.world["Collidable"]).entities).Remove(e);
                        }
                        //change speed
                        (((Speed)World.world["Speed"]).speed)[idxSpeed] *= -1;
                        //teleport
                        (((Position)World.world["Position"]).position)[idxPos] += (size/4)*(pos-posf)/(pos-posf).magnitude;
                    }
                }
            }
        }
        //handle collision with border
        {
            //get entities with position, speed and size (disregarding collidable)
            List<EntityComponent>[] req = {
                 (((Position)World.world["Position"]).entities),
                 (((Speed)World.world["Speed"]).entities),
                 (((Size)World.world["Size"]).entities) };
            List<EntityComponent> entities = World.getIntersect(req);
            foreach (EntityComponent e in entities)
            {
                int idxPos = (((Position)World.world["Position"]).entities).IndexOf(e);
                int idxSize = (((Size)World.world["Size"]).entities).IndexOf(e);
                Vector2 pos = (((Position)World.world["Position"]).position)[idxPos];
                float size = (((Size)World.world["Size"]).size)[idxSize];
                bool xCol = pos[0]-size/2 < -stageDimensions[0] || pos[0]+size/2 > stageDimensions[0];
                bool yCol = pos[1]-size/2 < -stageDimensions[1] || pos[1]+size/2 > stageDimensions[1];
                if ( xCol || yCol)
                {
                    int idxSpeed = (((Speed)World.world["Speed"]).entities).IndexOf(e);
                    if (xCol)
                    {
                        float m = (pos[0]<0)?1:-1;
                        //make speed[x] go toward center of screen (in case collisions last more than one frame)
                        (((Speed)World.world["Speed"]).speed)[idxSpeed] = new Vector2(
                            m*Mathf.Abs((((Speed)World.world["Speed"]).speed)[idxSpeed][0]),
                             (((Speed)World.world["Speed"]).speed)[idxSpeed][1]);
                    }
                    if (yCol)
                    {
                        float m = (pos[1]<0)?1:-1;
                        //make speed[y] go toward center of screen
                        (((Speed)World.world["Speed"]).speed)[idxSpeed] = new Vector2(
                             (((Speed)World.world["Speed"]).speed)[idxSpeed][0],
                            m*Mathf.Abs((((Speed)World.world["Speed"]).speed)[idxSpeed][1]));
                    }
                    //reset size
                    (((Size)World.world["Size"]).size)[idxSize] = (((Size)World.world["Size"]).originalSize)[idxSize];
                    //add collidable if not already there
                    if ((((Collidable)World.world["Collidable"]).entities).Contains(e) == false)
                    {
                        ((Collidable)World.world["Collidable"]).entities.Add(e);
                    }
                }
            }
        }
    }
}
