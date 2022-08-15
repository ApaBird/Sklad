using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHere : MonoBehaviour
{
    private bool trigger = false;
    private GameObject here = null;

    public bool isHere() => trigger;
    public GameObject GetHere() => here;

    private void OnTriggerEnter(Collider other) 
    {
        here = other.gameObject;
        trigger = true; 
    }

    private void OnTriggerExit(Collider other)
    {
        here = null;
        trigger = false;
    }
}
