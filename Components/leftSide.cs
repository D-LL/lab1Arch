using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : IComponent
{
    public List<EntityComponent> entities = new List<EntityComponent>();
    public uint frame;
    public float deltaTimeRight;
}

