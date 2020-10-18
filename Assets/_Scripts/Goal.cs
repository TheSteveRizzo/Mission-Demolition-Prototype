using System;
using UnityEngine;
using System.Collections;
public class Goal : MonoBehaviour
{
    private int level;
    static public bool goalMet = false;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("playerMax`"))
        {
            LoadPlayerLevel();
        }
        SavePlayerLevel();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Goal.goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_Color", Color.green);

            SavePlayerLevel();
            
        }
    }

    int LoadPlayerLevel()
    {
         return PlayerPrefs.GetInt("playerMaxLevel");
    }

    void SavePlayerLevel()
    {
        int savedLevel = LoadPlayerLevel();
        int nextLevel = (level + 1);
        
        if (savedLevel < nextLevel) 
        {
            // update high score
            PlayerPrefs.SetInt("playerMaxLevel", level+1);
            PlayerPrefs.Save();
        }
    }
}