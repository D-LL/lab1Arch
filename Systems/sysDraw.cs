using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sysDraw : ISystem
{
    public string Name { get; }
    public sysDraw()
    {
        Name = "sysDraw";
    }
    public void UpdateSystem()
    {
        //position and drawable
        List<EntityComponent>[] req = { (((Position)World.world["Position"]).entities), (((Drawable)World.world["Drawable"]).entities) };
        List<EntityComponent> entities = World.getIntersect(req);
        if (((LeftSide) World.world["LeftSide"]).frame % 4 != 0)
        {
            List<EntityComponent>[] reqL = { entities, ((LeftSide) World.world["LeftSide"]).entities };
            entities = World.getIntersect(reqL);
        }

        foreach (EntityComponent e in entities)
        {
            int idxPos = (((Position)World.world["Position"]).entities).IndexOf(e);
            ECSManager.Instance.UpdateShapePosition(e.id, (((Position)World.world["Position"]).position)[idxPos]);
        }
        //size
        List<EntityComponent>[] req2 = { (((Size)World.world["Size"]).entities), (((Drawable)World.world["Drawable"]).entities) };
        entities = World.getIntersect(req2);

        foreach (EntityComponent e in entities)
        {
            int idxSize = (((Size)World.world["Size"]).entities).IndexOf(e);
            ECSManager.Instance.UpdateShapeSize(e.id, (((Size)World.world["Size"]).size)[idxSize]);
        }
        //color
        List<EntityComponent>[] req3 = { (((ColorComp)World.world["ColorComp"]).entities), (((Drawable)World.world["Drawable"]).entities) };
        entities = World.getIntersect(req3);

        foreach (EntityComponent e in entities)
        {
            int idxCol = (((ColorComp)World.world["ColorComp"]).entities).IndexOf(e);
            ECSManager.Instance.UpdateShapeColor(e.id, (((ColorComp)World.world["ColorComp"]).color)[idxCol]);
        }
    }
}
