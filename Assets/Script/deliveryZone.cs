using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deliveryZone : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObj;
    private Collider myCollider;

    private void Start() => myCollider = this.GetComponent<Collider>();

    public void Activate()
    {
        for (int i=0; i<spawnObj.Length; i++)
        {
            float x = Random.Range(-myCollider.bounds.size.x/2, myCollider.bounds.size.x/2);
            float z = Random.Range(-myCollider.bounds.size.z/2, myCollider.bounds.size.z/2);
            Instantiate(spawnObj[i], new Vector3(x + this.transform.position.x, 2f, z + this.transform.position.z), Quaternion.identity);
        }
    }
}
