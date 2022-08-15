using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, InteractiveObject
{
    [SerializeField] private GameObject obj;
    private bool available;

    public bool Available() => available;

    public void AvailableSet(bool condition) => available = condition;

    public string Interaction() => "Activate";

    public void Outline(bool light) => this.GetComponent<Outline>().enabled = light;

    public void Activate(GameObject user = null) => obj.GetComponent<deliveryZone>().Activate();
}
