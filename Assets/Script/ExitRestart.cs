using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRestart : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject score;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exit.active)
                exit.active = false;
            else
                exit.active = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
