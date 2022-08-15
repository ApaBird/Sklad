using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.GetComponent<InteractiveObject>().AvailableSet(true);
        }
        catch { };
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            other.GetComponent<InteractiveObject>().AvailableSet(false);
        }
        catch { };
    }
}
