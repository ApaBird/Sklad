using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractiveObject 
{
    public void Outline(bool light);

    public string Interaction();

    public bool Available();

    public void AvailableSet(bool condition);

    public void Activate(GameObject user = null);
}
