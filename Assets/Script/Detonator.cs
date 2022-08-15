using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private ParticleSystem boom;

    public void Boom()
    {
        Instantiate(boom, this.transform);
    }
}
