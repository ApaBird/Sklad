using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack : MonoBehaviour, InteractiveObject
{
    [SerializeField] private GameObject[] places;
    private bool available;
    private bool isFull = false;

    public bool Available() => available;

    public void AvailableSet(bool condition) 
    { 
        available = condition;
        AvailableObject(condition);
    }

    public string Interaction() => "Activate";

    public void Outline(bool light) => this.GetComponent<Outline>().enabled = light;

    public void Activate(GameObject user = null)
    {
        bool fl = false;
        if (user != null && user.GetComponent<Boxs>() && !isFull)
        {
            for(int i = 0; i < places.Length; i++)
                if (!places[i].GetComponent<IsHere>().isHere())
                {
                    user.transform.position = places[i].transform.position;
                    fl = true;
                    break;
                }

            if (!fl)
                isFull = true;
        }
    }

    private void AvailableObject(bool condition)
    {
        foreach(GameObject place in places)
            if (place.GetComponent<IsHere>().isHere())
                place.GetComponent<IsHere>().GetHere().GetComponent<Boxs>().AvailableSet(condition);
    }
}
