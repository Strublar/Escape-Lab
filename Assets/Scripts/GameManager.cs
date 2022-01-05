using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public List<MenuLevel> menuLevels;
    public GameObject player;
    public TextMeshPro menuText;
    public int currentLevel = 0;

    public void Awake()
    {
        gm = this;
    }

    void Start()
    {
        LoadMenu();
    }

    public void LoadMenu()
    {
        currentLevel = ScoreManager.m.GetCurrentLevel();

        foreach(MenuLevel level in menuLevels)
        {
            level.Lock();
        }

        if (currentLevel >= 1)
        {
            for (int i = 0; i < currentLevel; i++)
            {
                menuLevels[i].Unlock();
            }
            menuText.text = "Welcome to Escape Lab\n" +
                "Find your way out of this mad scientist's lair by finding the keys to the doors !\n"+
                "Doors unlocked : " + currentLevel;
        }
        else //no levels unlocked
        {
            menuText.text = "Welcome to Escape Lab\n" +
                "Find your way out of this mad scientist's lair by finding the keys to the doors !";
        }
        
    }

    public void LoadLevel(MenuLevel level)
    {
        player.transform.position = level.waypoint.position;
    }
}
