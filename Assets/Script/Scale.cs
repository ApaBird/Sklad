using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    [SerializeField] private Image scale;
    private float value;

    private void UpdateScale()
    {
        scale.rectTransform.localScale = new Vector3(value, scale.rectTransform.localScale.y, scale.rectTransform.localScale.z);
    }

    public void SetPrecent(float precent)
    {
        if (precent > 1)
            value = 1;
        else if (precent < 0)
            value = 0;
        else
            value = precent;
        UpdateScale();
    }

    public void Enebale(bool command)
    {
        this.GetComponent<Image>().enabled = command;
        scale.enabled = command;
    }
}
