﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Set in Inspector")] 
    public Text uitLevel;
    public Text uitShots;
    public Text uitHighScore;
    public Text uitButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")] 
    public static int level;
    public int levelMax;
    public int shotsTaken;
    
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    
    public string showing = "Show Slingshot";
    
    void Start()
    {
        S = this;

        level = 1;
        levelMax = castles.Length;

        StartLevel();
    }
    
    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }
        
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject pTemp in gos) {
            Destroy(pTemp);
        }
        
        castle = Instantiate<GameObject>(castles[(level-1)]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        
        SwitchView("Show Both");

        ProjectileLine.S.Clear();
        
        Goal.goalMet = false;
        
        UpdateGUI();

        mode = GameMode.playing;

    }

    public static int GetLevel()
    {
        return level;
    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
        uitHighScore.text = "Highest Score: " + level;

    }

    void Update()
    {

        UpdateGUI();
        
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            
            mode = GameMode.levelEnd;
            
            SwitchView("Show Both");
            
            Invoke("NextLevel", 2f);

        }
    }
    
    void NextLevel()
    {
        level++;
        if (level > levelMax)
        {
            level = 1;
        }
        StartLevel();
    }
    
    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }

        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}