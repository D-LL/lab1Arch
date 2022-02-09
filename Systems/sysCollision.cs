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
        //get entities with a position and collidable(all circles)

        //get entities with also speed (dynamic circle)

        //detect collisions between dynamic and all

        //change size
            //if size max remove collidable (entities list already copied so equivalent to using a command buffer)

        //change speed

        //teleport (make sure position is in screen too)

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
                Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
                bool xCol = pos[0]-size/2 < -stageDimensions[0] || pos[0]+size/2 > stageDimensions[0];
                bool yCol = pos[1]-size/2 < -stageDimensions[1] || pos[1]+size/2 > stageDimensions[1];
                if ( xCol || yCol)
                {
                    int idxSpeed = (((Speed)World.world["Speed"]).entities).IndexOf(e);
                    if (xCol)
                    {
                        (((Speed)World.world["Speed"]).speed)[idxSpeed] = new Vector2(
                            -(((Speed)World.world["Speed"]).speed)[idxSpeed][0],
                             (((Speed)World.world["Speed"]).speed)[idxSpeed][1]);
                    }
                    if (yCol)
                    {
                        (((Speed)World.world["Speed"]).speed)[idxSpeed] = new Vector2(
                             (((Speed)World.world["Speed"]).speed)[idxSpeed][0],
                            -(((Speed)World.world["Speed"]).speed)[idxSpeed][1]);
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
