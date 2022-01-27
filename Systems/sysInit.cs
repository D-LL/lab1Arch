using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysInits : ISystem
{
    public string Name { get; }
    public void UpdateSystem()
    {
        if (World.world.Count == 0)
        {
            World.world.Add("Collidable", new Collidable());
            World.world.Add("ColorComp", new ColorComp());
            World.world.Add("Dynamic", new Dynamic());
            World.world.Add("Frames", new Frames());
            World.world.Add("Position", new Position());
            World.world.Add("Size", new Size());
            World.world.Add("Speed", new Speed());
            World.world.Add("LeftSide", new LeftSide());

            uint entityIndex = 0;
            foreach(Config.ShapeConfig e in ECSManager.Instance.Config.allShapesToSpawn)
            {
                EntityComponent entity = new EntityComponent();
                entity.id = entityIndex;
                ((Collidable)World.world["Collidable"]).entities.Add(entity);
                ((ColorComp)World.world["ColorComp"]).entities.Add(entity);
                ((ColorComp)World.world["ColorComp"]).color.Add(UnityEngine.Color.red);
                ((Dynamic)World.world["Dynamic"]).entities.Add(entity);
                ((Frames)World.world["Frames"]).entities.Add(entity);
                ((Position)World.world["Position"]).entities.Add(entity);
                ((Position)World.world["Position"]).position.Add(e.initialPos);
                ((Size)World.world["Size"]).entities.Add(entity);
                ((Size)World.world["Size"]).size.Add(e.size);
                ((Speed)World.world["Speed"]).entities.Add(entity);
                ((Speed)World.world["Speed"]).speed.Add(e.initialSpeed);
                ((LeftSide)World.world["LeftSide"]).entities.Add(entity);
            }
        }
    }
}
