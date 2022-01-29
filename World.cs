using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

static public class World
{
    public static Dictionary<string, IComponent> world = new Dictionary<string, IComponent>();
    public static List<EntityComponent> getIntersect(List<EntityComponent>[] components)
    {
        List<EntityComponent> res = new List<EntityComponent>();
        if (components.Length == 0)
        {
            return res;
        }
        foreach (EntityComponent e in components[0])
        {
            bool present = true;
            for (int i=1;i<components.Length;i++)
            {
                if (components[i].Contains(e) == false)
                {
                    present = false;
                }
            }
            if (present)
            {
                res.Add(e);
            }
        }
        return res;
    }
}
