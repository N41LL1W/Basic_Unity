using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    //Criando Singtom, instance
    public static PlayerStatsController Instance;

    public int xpMultiply = 1;
    public float xpFirstLevel = 100;
    public float difficultFactor = 1.5f;

    private static float xpNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Application.LoadLevel("GamePlay");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddXp(100);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public static void AddXp(float xpAdd)
    {
        float newXp = (GetCurrentXp() + xpAdd) * PlayerStatsController.Instance.xpMultiply;
        while (newXp >= GetNextXP())
        {
            newXp -= GetNextXP();
            AddLevel();
        }
        PlayerPrefs.SetFloat("currentXp", newXp);
    }

    public static float GetCurrentXp()
    {
        return PlayerPrefs.GetFloat("currentXp");
    }

    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("currentLevel");
    }

    public static void AddLevel()
    {
        int newLevel = GetCurrentLevel()+1;
        PlayerPrefs.SetInt("currentLevel", newLevel);
    }

    public static float GetNextXP()
    {
        return PlayerStatsController.Instance.xpFirstLevel * (GetCurrentLevel() + 1) * PlayerStatsController.Instance.difficultFactor;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 150, 50), "Currente XP = " + GetCurrentXp());
        GUI.Label(new Rect(0, 20, 150, 50), "Currente Level = " + GetCurrentLevel());
        GUI.Label(new Rect(0, 40, 150, 50), "Currente Next XP = " + GetNextXP());
    }
}
