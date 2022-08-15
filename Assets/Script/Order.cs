using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObj;
    [SerializeField][Range(1, 3)] private int maxLenghtOrder;
    [SerializeField] private GameObject score;
    [SerializeField] private float timeComplite;
    [SerializeField] private GameObject[] orderTable = new GameObject[4];
    private GameObject[] order = new GameObject[4];
    private GameObject[] inZone = new GameObject[4];
    private GameObject[] preview = new GameObject[4];
    private int lenghtOrder = 0;
 
    private void Start()
    {
        newOrder();
    }

    private void PreparePreview(GameObject[] preOrder)
    {
        for (int i = 0; i < lenghtOrder; i++)
        {
            preview[i] =  Instantiate(order[i].GetComponent<Boxs>().GetPreviewImg(), orderTable[i].transform.position, Quaternion.identity);
        }
    }

    private void checkOrder()
    {
        int count = 0;
        for (int i = 0; i < lenghtOrder; i++)
        {
            if (preview[i].GetComponent<Outline>().enabled)
                count++;
        }
        Debug.Log(count);

        if (count == lenghtOrder)
        {
            score.GetComponent<Score>().UpScore(lenghtOrder * 100);
            newOrder();
        }

    }

    private void newOrder()
    {
        //Стераем старый заказа
        for (int i = 0; i < lenghtOrder; i++)
        {
            if (preview[i])
                Destroy(preview[i]);
            preview[i] = null;
            order[i] = null;
            if (inZone[i])
                Destroy(inZone[i]);
            inZone[i] = null;
        }

        //Определение размера следующего заказа
        lenghtOrder = Random.Range(1, maxLenghtOrder);

        //Его создание и создания превью
        for (int i = 0; i < lenghtOrder; i++)
        {
            order[i] = spawnObj[Random.Range(0, spawnObj.Length)];
        }
        PreparePreview(order);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Boxs>())
           for (int i = 0; i < lenghtOrder; i++)
           {
                if (inZone[i] == null && order[i].GetComponent<Boxs>().GetTypeBox() == other.GetComponent<Boxs>().GetTypeBox() && !preview[i].GetComponent<Outline>().enabled)
                {
                    inZone[i] = other.gameObject;
                    preview[i].GetComponent<Outline>().enabled = true;
                    break;
                }
           }

        int count = 0;
        for (int i = 0; i < lenghtOrder; i++)
        {
            if (preview[i].GetComponent<Outline>().enabled)
                count++;
        }
        Debug.Log(count);
        if (count == lenghtOrder)
        {
            Debug.Log("!");
            Invoke("checkOrder", timeComplite);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Boxs>())
        {
            Debug.Log("Box");
            for (int i = 0; i < lenghtOrder; i++)
            {
                if (inZone[i] == other.gameObject)
                {
                    Debug.Log("Box is out");
                    inZone[i] = null;
                    preview[i].GetComponent<Outline>().enabled = false;
                    break;
                }
            }
        }
    }

}
