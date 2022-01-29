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
        //position
        List<EntityComponent> entities = ((Position)World.world["Position"]).entities;

        foreach (EntityComponent e in entities)
        {
            int idxPos = (((Position)World.world["Position"]).entities).IndexOf(e);
            ECSManager.Instance.UpdateShapePosition(e.id, (((Position)World.world["Position"]).position)[idxPos]);
        }
        //size
        entities = ((Size)World.world["Size"]).entities;

        foreach (EntityComponent e in entities)
        {
            int idxSize = (((Size)World.world["Size"]).entities).IndexOf(e);
            ECSManager.Instance.UpdateShapeSize(e.id, (((Size)World.world["Size"]).size)[idxSize]);
        }
        //color
        entities = ((ColorComp)World.world["ColorComp"]).entities;

        foreach (EntityComponent e in entities)
        {
            int idxCol = (((ColorComp)World.world["ColorComp"]).entities).IndexOf(e);
            ECSManager.Instance.UpdateShapeColor(e.id, (((ColorComp)World.world["ColorComp"]).color)[idxCol]);
        }
    }
}
