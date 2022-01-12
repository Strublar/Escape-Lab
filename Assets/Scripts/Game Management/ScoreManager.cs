using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager m;

    [SerializeField] private float startTime,endTime;

    [SerializeField] private int currentLevel;
    [SerializeField] private float score;
    [SerializeField] private bool resetScore;

    public void Awake()
    {
        m = this;
    }

    

    public void Update()
    {
        if (resetScore)
        {
            ResetScores();
            resetScore = false;
            
        }
            

    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public void EndTimer()
    {
        endTime = Time.time;
        score = endTime - startTime;
        SaveScore();
    }

    public void SaveScore()
    {
        Debug.Log("Saving Scores");
        //timer
        if(PlayerPrefs.HasKey("score"))
        {
            if(PlayerPrefs.GetFloat("score") < score)
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

            if (PlayerPrefs.GetInt("currentLevel") < level + 1)
            {
                PlayerPrefs.SetInt("currentLevel", level + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("currentLevel", level + 1);
        }
    }

    public void SaveCurrentTimer()
    {
        PlayerPrefs.SetFloat("timer", Time.time-startTime);
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
        for(int level = 0; level<3;level++)
        {
            if (PlayerPrefs.GetFloat("score" + level) < score)
            {
                Debug.Log("New Record");
                PlayerPrefs.SetFloat("score" + level, score);
            }
        }
        PlayerPrefs.SetInt("currentLevel", 0);

        Debug.Log("Scores reset");
    }

}
