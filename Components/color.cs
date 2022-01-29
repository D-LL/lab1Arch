using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorComp : IComponent
{
    public List<EntityComponent> entities = new List<EntityComponent>();
    public List<Color> color = new List<Color>();
}
