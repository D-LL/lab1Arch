using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class sysRewind : ISystem
{
    public string Name { get; }
    public sysRewind()
    {
        Name = "sysRewind";
    }
    public void UpdateSystem()
    {
        float rewindTimeStamp = ((Rewind) World.world["Rewind"]).rewindTimeStamp;
        float coolDownTimeLeft = (rewindTimeStamp - Time.time);
        //Used to print on console
        uint cooldownTimer = ((Rewind)World.world["Rewind"]).cooldownShowNextSecond;
        
        if(coolDownTimeLeft <= 0.0f)
        {
            uint firstCopyId = ((Rewind) World.world["Rewind"]).firstCopyId;
            ((Rewind) World.world["Rewind"]).moveRewindCopies = true;
            
            //Print when rewind is available
            if(cooldownTimer == 0)
            {
                Debug.Log("Rewind Ready!");
                ((Rewind)World.world["Rewind"]).cooldownShowNextSecond = 3;
            }

            //If player press space bar
            if (Input.GetKeyDown("space"))
            {
                ((Rewind) World.world["Rewind"]).moveRewindCopies = false;
                List<EntityComponent> entities = ((Position)World.world["Position"]).entities;
                for(int index = 0; index < entities.Count; index++)
                {
                    //Destroy current circles
                    if (entities[index].id < firstCopyId)
                    {
                        DeleteEntity(entities[index]);
                        index--;
                    }
                    else
                    {
                        //Show the hidden copies
                        ActivateCopy(entities[index], firstCopyId);
                        firstCopyId++;
                    }
                }
                ((Rewind) World.world["Rewind"]).firstCopyId = firstCopyId;
                //Create new rewind copies
                CreateNewCopies(firstCopyId);
                ((Rewind) World.world["Rewind"]).rewindTimeStamp = Time.time + 3.0f;
            }
        }
        else
        {
            //Print rewind cooldown
            if (coolDownTimeLeft <= cooldownTimer)
            {
                if (cooldownTimer > 0)
                {
                    Debug.Log("Rewind cooldown: " + cooldownTimer);
                    ((Rewind)World.world["Rewind"]).cooldownShowNextSecond--;
                }
            }
        }
    }
    
    private void DeleteEntity(EntityComponent entity)
    {
        ECSManager.Instance.DestroyShape(entity.id);
        ((Collidable)World.world["Collidable"]).entities.Remove(entity);
        int index = ((ColorComp)World.world["ColorComp"]).entities.IndexOf(entity);
        ((ColorComp)World.world["ColorComp"]).entities.RemoveAt(index);
        ((ColorComp)World.world["ColorComp"]).color.RemoveAt(index);
        
        if (((Speed)World.world["Speed"]).entities.Contains(entity))
        {
            index = ((Speed)World.world["Speed"]).entities.IndexOf(entity);
            ((Speed)World.world["Speed"]).entities.RemoveAt(index);
            ((Speed)World.world["Speed"]).speed.RemoveAt(index);
        }
        
        index = ((Position)World.world["Position"]).entities.IndexOf(entity);
        ((Position)World.world["Position"]).entities.RemoveAt(index);
        ((Position)World.world["Position"]).position.RemoveAt(index);
        index = ((Size)World.world["Size"]).entities.IndexOf(entity);
        ((Size)World.world["Size"]).entities.RemoveAt(index);
        ((Size)World.world["Size"]).size.RemoveAt(index);
        ((Size)World.world["Size"]).originalSize.RemoveAt(index);
        index = ((Drawable)World.world["Drawable"]).entities.IndexOf(entity);
        ((Drawable)World.world["Drawable"]).entities.RemoveAt(index);
        
        if (((LeftSide)World.world["LeftSide"]).entities.Contains(entity))
        {
            index = ((LeftSide)World.world["LeftSide"]).entities.IndexOf(entity);
            ((LeftSide)World.world["LeftSide"]).entities.RemoveAt(index);
        }
    }

    private void ActivateCopy(EntityComponent entity, uint entityId)
    {
        Config.ShapeConfig config = new Config.ShapeConfig();
        
        int positionIndex = ((Position)World.world["Position"]).entities.IndexOf(entity);
        int sizeIndex = ((Size)World.world["Size"]).entities.IndexOf(entity);
        int speedIndex = ((Speed)World.world["Speed"]).entities.IndexOf(entity);
        UnityEngine.Vector2 speed = new UnityEngine.Vector2(0.0f, 0.0f);
        if (speedIndex > -1)
        {
            speed = ((Speed)World.world["Speed"]).speed[speedIndex];
        }

        config.initialPos = ((Position)World.world["Position"]).position[positionIndex];
        config.size = ((Size)World.world["Size"]).size[sizeIndex];
        config.initialSpeed = speed;

        ECSManager.Instance.CreateShape(entityId,config);
        ((Drawable)World.world["Drawable"]).entities.Add(entity);
        ((Rewind)World.world["Rewind"]).entities.Remove(entity);
    }

    private void CreateNewCopies(uint copyId)
    {
        List<EntityComponent> originalEntities = ((Position)World.world["Position"]).entities;
        int originalEntityIndex = 0;
        foreach(Config.ShapeConfig e in ECSManager.Instance.Config.allShapesToSpawn)
        {
            EntityComponent entity = new EntityComponent();
            entity.id = copyId;
            
            
            ((Rewind) World.world["Rewind"]).entities.Add(entity);
            ((Collidable)World.world["Collidable"]).entities.Add(entity);
            ((ColorComp)World.world["ColorComp"]).entities.Add(entity);
            ((ColorComp)World.world["ColorComp"]).color.Add(UnityEngine.Color.red);
            ((Position)World.world["Position"]).entities.Add(entity);
            ((Position)World.world["Position"]).position.Add(((Position)World.world["Position"]).position[originalEntityIndex]);
            
            ((Size)World.world["Size"]).entities.Add(entity);
            ((Size)World.world["Size"]).size.Add(((Size)World.world["Size"]).size[originalEntityIndex]);
            ((Size)World.world["Size"]).originalSize.Add(((Size)World.world["Size"]).originalSize[originalEntityIndex]);
            
            if (((Speed)World.world["Speed"]).entities.Contains(originalEntities[originalEntityIndex]))
            {
                //dynamic circle
                ((Speed)World.world["Speed"]).entities.Add(entity);
                int index = ((Speed)World.world["Speed"]).entities.IndexOf(originalEntities[originalEntityIndex]);
                ((Speed)World.world["Speed"]).speed.Add(((Speed)World.world["Speed"]).speed[index]);
            }
            
            if (((Position)World.world["Position"]).position[originalEntityIndex].x < 0)
            {
                ((LeftSide)World.world["LeftSide"]).entities.Add(entity);
            }
            copyId += 1;
            originalEntityIndex += 1;
        }
    }
}