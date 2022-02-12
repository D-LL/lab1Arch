using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sysColor : ISystem
{
    public string Name { get; }
    public sysColor()
    {
        Name = "sysColor";
    }
    public void UpdateSystem()
    {
        //find entities with position and speed
        List<EntityComponent>[] req = { (((Position)World.world["Position"]).entities), (((ColorComp)World.world["ColorComp"]).entities) };
        List<EntityComponent> entities = World.getIntersect(req);
        if (((LeftSide) World.world["LeftSide"]).frame % 4 != 0)
        {
            List<EntityComponent>[] reqL = { entities, ((LeftSide) World.world["LeftSide"]).entities };
            entities = World.getIntersect(reqL);
        }

        //red by default, overwrite in other cases
        foreach (EntityComponent e in entities)
        {
            int idxCol = (((ColorComp)World.world["ColorComp"]).entities).IndexOf(e);
            (((ColorComp)World.world["ColorComp"]).color)[idxCol] = UnityEngine.Color.red;
        }
        //if dynamic make blue
        List<EntityComponent>[] dynreq = { entities, (((Speed)World.world["Speed"]).entities) };
        List<EntityComponent> dynEntities = World.getIntersect(dynreq);
        foreach (EntityComponent e in dynEntities)
        {
            int idxCol = (((ColorComp)World.world["ColorComp"]).entities).IndexOf(e);
            (((ColorComp)World.world["ColorComp"]).color)[idxCol] = UnityEngine.Color.blue;
        }
        //if Collidable make green
        List<EntityComponent>[] dynColreq = { dynEntities, (((Collidable)World.world["Collidable"]).entities) };
        List<EntityComponent> dynColEntities = World.getIntersect(dynColreq);
        foreach (EntityComponent e in dynColEntities)
        {
            int idxCol = (((ColorComp)World.world["ColorComp"]).entities).IndexOf(e);
            (((ColorComp)World.world["ColorComp"]).color)[idxCol] = UnityEngine.Color.green;
        }
    }
}
