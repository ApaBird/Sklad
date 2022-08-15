using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject target = null;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Выбор интерактивного объекта
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            var interactiveObject = hit.collider.gameObject.GetComponent<InteractiveObject>();
            if (interactiveObject != null)
            {
                if (hit.collider.GetComponent<InteractiveObject>().Available())
                {
                    if (target != null && target != hit.collider.gameObject)
                    {
                        target.GetComponent<InteractiveObject>().Outline(false);
                        target = hit.collider.gameObject;
                        target.GetComponent<InteractiveObject>().Outline(true);
                    }
                    else if (target == null)
                    {
                        target = hit.collider.gameObject;
                        target.GetComponent<InteractiveObject>().Outline(true);
                    }
                }
            }
        }
        else if (target != null)
        {
            target.GetComponent<InteractiveObject>().Outline(false);
            target = null;
        }
    }

    public GameObject GetTarget() => target;

}
