using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager m;
    [SerializeField] private float startTime,endTime;

    [SerializeField] private float score;


    public void Awake()
    {
        m = this;
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public void EndTimer()
    {
        endTime = Time.time;
        score = endTime - startTime;
    }

    public void SaveScore()
    {
        if(PlayerPrefs.HasKey("score"))
        {
            if(PlayerPrefs.GetFloat("score")>= score)
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

    public float GetHighScore()
    {
        if (PlayerPrefs.HasKey("score"))
            return PlayerPrefs.GetFloat("score");
        return 0f;
    }
}
