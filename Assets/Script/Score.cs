using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text timer;
    [SerializeField] private GameObject[] boom;
    private Text score;
    private int scoreInt = 0;
    void Start()
    {
        score = this.GetComponent<Text>();
    }

    private void UpdateScore()
    {
        score.text = scoreInt.ToString();
        if (scoreInt >= 1000)
        {
            timer.GetComponent<TimerScore>().TimerStop();
            foreach (GameObject bomb in boom)
                bomb.GetComponent<Detonator>().Boom();
        }
    }

    public void UpScore(int bonus)
    {
        scoreInt += bonus;
        UpdateScore();
    }

    public void SetScore(int scoreSet)
    {
        scoreInt = scoreSet;
        UpdateScore();
    }
}
