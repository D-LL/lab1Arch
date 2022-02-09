using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysInits : ISystem
{
    public string Name { get; }
    public SysInits()
    {
        Name = "SysInits";
    }
    public void UpdateSystem()
    {
        if (World.world.Count == 0)
        {
            World.world.Add("Collidable", new Collidable());
            World.world.Add("ColorComp", new ColorComp());
            World.world.Add("Frames", new Frames());
            World.world.Add("Position", new Position());
            World.world.Add("Drawable", new Drawable());
            World.world.Add("Size", new Size());
            World.world.Add("Speed", new Speed());
            World.world.Add("LeftSide", new LeftSide());

            uint entityIndex = 0;
            //entity zero is frame manager
            EntityComponent ex = new EntityComponent();
            ex.id = entityIndex;
            ((Frames)World.world["Frames"]).entities.Add(ex);
            entityIndex += 1;
            foreach(Config.ShapeConfig e in ECSManager.Instance.Config.allShapesToSpawn)
            {
                EntityComponent entity = new EntityComponent();
                entity.id = entityIndex;
                ((Collidable)World.world["Collidable"]).entities.Add(entity);
                ((ColorComp)World.world["ColorComp"]).entities.Add(entity);
                ((ColorComp)World.world["ColorComp"]).color.Add(UnityEngine.Color.red);
                if ((entity.id - 1) % 4 != 0)
                {
                    //dynamic circle
                    ((Speed)World.world["Speed"]).entities.Add(entity);
                    ((Speed)World.world["Speed"]).speed.Add(e.initialSpeed);
                }
                ((Position)World.world["Position"]).entities.Add(entity);
                ((Position)World.world["Position"]).position.Add(e.initialPos);
                ((Size)World.world["Size"]).entities.Add(entity);
                ((Size)World.world["Size"]).size.Add(e.size);
                ((Size)World.world["Size"]).originalSize.Add(e.size);
                ((Drawable)World.world["Drawable"]).entities.Add(entity);
                if (e.initialPos.x < 0)
                {
                    ((LeftSide)World.world["LeftSide"]).entities.Add(entity);
                }
                ECSManager.Instance.CreateShape(entityIndex, e);
                entityIndex += 1;
            }
        }
    }
}
