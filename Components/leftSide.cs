using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : IComponent
{
    public List<EntityComponent> entities = new List<EntityComponent>();
    public int frame;
    public float deltaTimeRight;
}

