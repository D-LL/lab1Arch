using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : IComponent
{
    public List<EntityComponent> entities = new List<EntityComponent>();
    public List<Vector2> position = new List<Vector2>();
}
