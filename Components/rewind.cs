using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : IComponent
{
    public List<EntityComponent> entities = new List<EntityComponent> ();
    public uint firstCopyId;
    public float rewindTimeStamp;
    public bool moveRewindCopies = false;
}
