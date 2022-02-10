using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : IComponent
{
    public List<EntityComponent> entities = new List<EntityComponent> ();
    public uint firstCopyId;
    public float rewindTimeStamp;
    public uint cooldownShowNextSecond = 3;
    public bool moveRewindCopies = false;
}
