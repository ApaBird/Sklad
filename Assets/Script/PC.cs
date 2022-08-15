using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC : MonoBehaviour
{
    private Rigidbody my_rb;
    private GameObject inHand;
    [SerializeField] private Canvas UIPlayer;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public LayerMask LayerMask;
    [SerializeField] private float maxPowerThrow;
    [SerializeField] private float maxTimeThrow;
    [SerializeField] private float powerPlayer;
    private float startThrow = -1;
    private float endThrow = 0;
    private GameObject target = null;
    [SerializeField] private GameObject hand;

    void goTO(float directAngle)
    {
        float angle = Mathf.Abs(my_rb.rotation.eulerAngles.y % 360);
        if (my_rb.rotation.eulerAngles.y % 360 != directAngle)
            if (angle > (directAngle%360) - 22 && angle < (directAngle%360) + 22)
                my_rb.transform.eulerAngles = new Vector3(my_rb.rotation.eulerAngles.x, directAngle, my_rb.rotation.eulerAngles.z);
            else if (angle < directAngle)
                if (directAngle - angle < 360 - (directAngle - angle))
                    my_rb.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
                else
                    my_rb.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
            else
                if (angle - directAngle > 360 - (angle - directAngle))
                    my_rb.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
                else
                    my_rb.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void Drop(float FThrow)
    {
        inHand.GetComponent<Rigidbody>().freezeRotation = false;
        inHand.GetComponent<Rigidbody>().useGravity = true;
        inHand.transform.parent = null;
        Vector3 power = new Vector3(0f, FThrow, FThrow);
        inHand.GetComponent<Rigidbody>().isKinematic = false;
        inHand.GetComponent<Rigidbody>().AddRelativeForce(power, ForceMode.VelocityChange);
        inHand = null;
    }

    void Start()
    {
        my_rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            goTO(45);
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            goTO(315);
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            goTO(135);
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            goTO(225);
        else if (Input.GetKey(KeyCode.W))
            goTO(360);
        else if (Input.GetKey(KeyCode.A))
            goTO(270);
        else if (Input.GetKey(KeyCode.D))
            goTO(90);
        else if (Input.GetKey(KeyCode.S))
            goTO(180);

        float _speed;
        if (inHand != null)
            _speed = speed - inHand.GetComponent<Rigidbody>().mass;
        else
            _speed = speed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
            my_rb.AddRelativeForce(0f, 0f, _speed, ForceMode.Acceleration);

    }

    void Update()
    {
        //Взаимодействие с выбраным объектом
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            target = GetComponent<PlayerRayCast>().GetTarget();
            if (target)
            {
                string typeInteraction = target.GetComponent<InteractiveObject>().Interaction();
                // Что то можно поднять
                if (typeInteraction == "PickUp")
                {
                    if (inHand != null)
                    {
                        inHand.GetComponent<Rigidbody>().freezeRotation = false;
                        inHand.transform.parent = null;
                        inHand = null;
                    }
                    if (powerPlayer >= target.GetComponent<Rigidbody>().mass)
                    {
                        target.transform.parent = hand.transform;
                        target.transform.position = hand.transform.position;
                        target.transform.rotation = hand.transform.rotation;
                        inHand = target;
                        target = null;
                        inHand.GetComponent<Rigidbody>().isKinematic = true;
                        inHand.GetComponent<Rigidbody>().freezeRotation = true;
                        inHand.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
                else if (typeInteraction == "Activate")
                { // Что то можно активировать
                    //Переписать на абстрактный класс интерактивные объекты и все что с ними связано
                    if (target.GetComponent<Button>())
                        target.GetComponent<Button>().Activate(null);
                    else if (target.GetComponent<Rack>())
                    {
                        target.GetComponent<InteractiveObject>().Activate(inHand);
                        Drop(0);
                    }
                }
            }
        }

        //Данные для шкалы силы броска
        if (startThrow != -1)
        {
            endThrow = Time.time - startThrow;
            UIPlayer.transform.Find("Scale").GetComponent<Scale>().SetPrecent(endThrow);
        }

        if (endThrow > 0.075f)
            UIPlayer.transform.Find("Scale").GetComponent<Scale>().Enebale(true);

        //Время удержания кнопки
        if (Input.GetKeyDown(KeyCode.Mouse1))
            startThrow = Time.time;

        //Бросить то что в руках
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (inHand != null)
            {
                endThrow = Time.time - startThrow;
                if (endThrow > 0.2f)
                    if (endThrow > maxTimeThrow)
                        Drop(maxPowerThrow);
                    else
                        Drop(maxPowerThrow * (endThrow / maxTimeThrow));
                else
                    Drop(0);
            }
            startThrow = -1;
            endThrow = 0;
            UIPlayer.transform.Find("Scale").GetComponent<Scale>().SetPrecent(0);
            UIPlayer.transform.Find("Scale").GetComponent<Scale>().Enebale(false);
        }

        if (inHand != null)
            inHand.transform.position = hand.transform.position;

    }
}
