using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : IComponent
{
    public List<EntityComponent> entities = new List<EntityComponent>();
    public List<float> size = new List<float>();
}
