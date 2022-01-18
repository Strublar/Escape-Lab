using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager m;

    [SerializeField] private float startTime,endTime;

    [SerializeField] private int currentLevel;
    [SerializeField] private float score;
    [SerializeField] private bool resetScore, showScore;
    [SerializeField] private float savedTimer;
    [SerializeField] private TextMeshPro scoreText, highScoreText;
    [SerializeField] private List<SpriteRenderer> stars;
    public void Awake()
    {
        m = this;
    }

    

    public void Update()
    {
        if (resetScore)
        {
            ResetScores();
            PlayerPrefs.SetFloat("score", 0f);
            resetScore = false;
            
        }
        if(showScore)
        {
            ShowScore();
            showScore = false;
        }

    }

    public void StartTimer()
    {
        savedTimer = GetTimer();
        startTime = Time.time;
        Debug.Log("Starting Timer");
    }

    public void EndTimer()
    {
        endTime = Time.time;
        score = endTime - startTime+savedTimer;
        SaveScore();
        ShowScore();
        Debug.Log("Ending Timer");
    }

    public void ShowScore()
    {
        scoreText.text = "Score : " + score.ToString("0.00")+"s";
        highScoreText.text = "Best : " + GetHighScore().ToString("0.00")+"s";
        foreach (SpriteRenderer sprite in stars)
            sprite.color = Color.black;

        if (score <= 600)
            stars[0].color = Color.white;
        if (score <= 360)
            stars[1].color = Color.white;
        if (score <= 180)
            stars[2].color = Color.white;

    }

    public void SaveScore()
    {
        Debug.Log("Saving Scores");
        //timer
        if(PlayerPrefs.HasKey("score"))
        {
            if(PlayerPrefs.GetFloat("score") > score)
            {
                Debug.Log("New Record");
                PlayerPrefs.SetFloat("score", score);
            }
        }
        else
        {
            Debug.Log("New Record");
            PlayerPrefs.SetFloat("score", score);
        }

        
    }

    public void SaveCurrentLevel(int level)
    {
        //Levels done
        if (PlayerPrefs.HasKey("currentLevel"))
        {
            Debug.Log("CHecking current level : " + PlayerPrefs.GetInt("currentLevel") + " vs " + (level + 1));
            if (PlayerPrefs.GetInt("currentLevel") < level + 1)
            {
                PlayerPrefs.SetInt("currentLevel", level + 1);
                Debug.Log("Saved level " + (level + 1));
            }
        }
        else
        {
            PlayerPrefs.SetInt("currentLevel", level + 1);
        }
    }

    public void SaveCurrentTimer()
    {
        Debug.Log("Saving timer");
        PlayerPrefs.SetFloat("timer", Time.time-startTime+savedTimer);
    }

    public float GetTimer()
    {
        if (PlayerPrefs.HasKey("timer"))
            return PlayerPrefs.GetFloat("timer");
        return 0f;
    }

    public float GetHighScore()
    {
        if (PlayerPrefs.HasKey("score"))
            return PlayerPrefs.GetFloat("score");
        return 0f;
    }

    public int GetCurrentLevel()
    {
        if (PlayerPrefs.HasKey("currentLevel"))
            return PlayerPrefs.GetInt("currentLevel");
        return 0;
    }

    public void ResetScores()
    {

        PlayerPrefs.SetInt("currentLevel", 0);
        PlayerPrefs.SetFloat("timer", 0f);
        Debug.Log("Scores reset");
    }

    public void StartNewRun()
    {
        savedTimer = 0f;
        ResetScores();
        GameManager.gm.LoadMenu();
    }
}
