using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sysLeftSide : ISystem
{
    public string Name { get; }
    public sysLeftSide()
    {
        Name = "sysLeftSide";
    }
    public void UpdateSystem()
    {
        List<EntityComponent> entities = ((Position)World.world["Position"]).entities;
        foreach (EntityComponent e in entities)
        {
            int idxPos = (((Position)World.world["Position"]).entities).IndexOf(e);
            if ((((Position)World.world["Position"]).position)[idxPos][0] < 0)
            {
                if (!((((LeftSide) World.world["LeftSide"]).entities).Contains(e)))
                {
                    (((LeftSide) World.world["LeftSide"]).entities).Add(e);
                }
            }
            else
            {
                if ((((LeftSide) World.world["LeftSide"]).entities).Contains(e))
                {
                    (((LeftSide) World.world["LeftSide"]).entities).Remove(e);
                }
            }
        }
        if (((LeftSide) World.world["LeftSide"]).frame % 4 == 0)
        {
            ((LeftSide) World.world["LeftSide"]).deltaTimeRight = 0;
        }
        ((LeftSide) World.world["LeftSide"]).frame =  unchecked(((LeftSide) World.world["LeftSide"]).frame + 1);//let frame come back to 0 on overflow;
        ((LeftSide) World.world["LeftSide"]).deltaTimeRight += Time.deltaTime;
    }
}