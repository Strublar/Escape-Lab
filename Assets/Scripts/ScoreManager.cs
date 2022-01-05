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

    public void StartTimer(int level)
    {
        currentLevel = level;
        startTime = Time.time;
    }

    public void EndTimer(int level)
    {
        endTime = Time.time;
        score = endTime - startTime;
        SaveScore(level);
    }

    public void SaveScore(int level)
    {
        Debug.Log("Saving Scores");
        //timer
        if(PlayerPrefs.HasKey("score"+level))
        {
            if(PlayerPrefs.GetFloat("score" + level) < score)
            {
                Debug.Log("New Record");
                PlayerPrefs.SetFloat("score" + level, score);
            }
        }
        else
        {
            Debug.Log("New Record");
            PlayerPrefs.SetFloat("score" + level, score);
        }

        //Levels done
        if(PlayerPrefs.HasKey("currentLevel"))
        {

            if (PlayerPrefs.GetInt("currentLevel") < level+1)
            {
                PlayerPrefs.SetInt("currentLevel", level+1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("currentLevel", level+1);
        }
    }

    public float GetHighScore(int level)
    {
        if (PlayerPrefs.HasKey("score" + level))
            return PlayerPrefs.GetFloat("score" + level);
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
