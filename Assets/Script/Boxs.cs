using UnityEngine;

public class Boxs : MonoBehaviour, InteractiveObject
{
    [SerializeField] string type;
    [SerializeField] GameObject previewImg;
    private bool onHand = false;
    private bool available = false;

    public string GetTypeBox() => type;

    public void OnHandSwith()
    {
        if (onHand)
            onHand = false;
        else
            onHand = true;
    }

    public bool GetOnHand() => onHand;

    public void Outline(bool light) => this.GetComponent<Outline>().enabled = light;

    public string Interaction() => "PickUp";

    public bool Available() => available;

    public void AvailableSet(bool condition) => available = condition;

    public GameObject GetPreviewImg() => previewImg;

    public void Activate(GameObject user = null)
    {
    }
}
