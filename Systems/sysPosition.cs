using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sysPosition : ISystem
{
    public string Name { get; }
    public sysPosition()
    {
        Name = "sysPosition";
    }
    public void UpdateSystem()
    {
        //find entities with position and speed
        List<EntityComponent>[] req = { (((Position)World.world["Position"]).entities), (((Speed)World.world["Speed"]).entities) };
        List<EntityComponent> entities = World.getIntersect(req);
        if (((LeftSide) World.world["LeftSide"]).frame % 4 != 0)
        {
            List<EntityComponent>[] reqL = { entities, ((LeftSide) World.world["LeftSide"]).entities };
            entities = World.getIntersect(reqL);
        }
        
        foreach (EntityComponent e in entities)
        {
            if (IfShouldBeMoving(e))
            {
                int idxPos = (((Position)World.world["Position"]).entities).IndexOf(e);
                int idxSpeed = (((Speed)World.world["Speed"]).entities).IndexOf(e);

                if ((((LeftSide) World.world["LeftSide"]).entities).Contains(e))
                {
                    (((Position)World.world["Position"]).position)[idxPos] = (((Position)World.world["Position"]).position)[idxPos] + 
                                                                            Time.deltaTime * (((Speed)World.world["Speed"]).speed)[idxSpeed];
                }
                else
                {
                    (((Position)World.world["Position"]).position)[idxPos] = (((Position)World.world["Position"]).position)[idxPos] + 
                                                        ((LeftSide) World.world["LeftSide"]).deltaTimeRight * (((Speed)World.world["Speed"]).speed)[idxSpeed];
                }
            }
        }
    }

    //Used to make the rewind copies move after 3 seconds
    private bool IfShouldBeMoving(EntityComponent e)
    {
        //If part of the main run, should move
        if (!((Rewind) World.world["Rewind"]).entities.Contains(e)) return true;
        //If part of the rewind run and the should be moving, return true
        if ( ((Rewind) World.world["Rewind"]).entities.Contains(e) && ((Rewind) World.world["Rewind"]).moveRewindCopies) return true;
        //Otherwise, return false
        return false;

    }
}
