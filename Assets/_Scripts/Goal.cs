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
        SavePlayerLevel(level);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Goal.goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_Color", Color.green);

            SavePlayerLevel(level + 1);
        }
    }

    int LoadPlayerLevel()
    {
         return PlayerPrefs.GetInt("playerMaxLevel");
    }

    void SavePlayerLevel(int newScore)
    {
        if (LoadPlayerLevel() < newScore) 
        {
            // update high score, old score is less than new score (player beat record)
            PlayerPrefs.SetInt("playerMaxLevel", newScore);
            PlayerPrefs.Save();
        }
    }
}