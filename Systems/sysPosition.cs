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

        foreach (EntityComponent e in entities)
        {
            int idxPos = (((Position)World.world["Position"]).entities).IndexOf(e);
            int idxSpeed = (((Speed)World.world["Speed"]).entities).IndexOf(e);

            (((Position)World.world["Position"]).position)[idxPos] = (((Position)World.world["Position"]).position)[idxPos] + 
                Time.deltaTime * (((Speed)World.world["Speed"]).speed)[idxSpeed];
        }
    }
}
