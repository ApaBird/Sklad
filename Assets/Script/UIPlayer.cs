using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
        this.transform.position = player.transform.position;
    }
}
