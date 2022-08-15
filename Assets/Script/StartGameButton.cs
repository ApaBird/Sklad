using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private int game;
    public void StartGame() => SceneManager.LoadScene(game);
}
