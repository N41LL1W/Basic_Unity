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
    public List<BasicInfoChar> baseInfoChars;
    
    [System.Serializable]
    public class BasicInfoChar
    {
        public CharacterBase.BasicStats baseInfo;
        public TypeCharacter typeChar;
    }

    private static float xpNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //Application.LoadLevel("GamePlay");

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
    
    // Função para adicionar XP
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
    
    // Função que guarda o XP Atual
    public static float GetCurrentXp()
    {
        return PlayerPrefs.GetFloat("currentXp");
    }
    
    // Função que guarda o Level Atual
    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("currentLevel");
    }

    // Função que adiciona o Level
    public static void AddLevel()
    {
        int newLevel = GetCurrentLevel()+1;
        PlayerPrefs.SetInt("currentLevel", newLevel);
    }

    // Função que pega quanto XP falta para o proximo Level
    public static float GetNextXP()
    {
        return PlayerStatsController.Instance.xpFirstLevel * (GetCurrentLevel() + 1) * PlayerStatsController.Instance.difficultFactor;
    }

    public static TypeCharacter GetTypeCharacter()
    {
        int typeId = PlayerPrefs.GetInt("TypeCharacter");

        if (typeId == 0)
        {
            return TypeCharacter.Warrior;
        }
        else if (typeId == 1)
        {
            return TypeCharacter.Wizard;
        }
        else if (typeId == 2)
        {
            return TypeCharacter.Archer;
        }
        return TypeCharacter.Warrior;
    }

    public static void SetTypeCharacter(TypeCharacter newType)
    {
        PlayerPrefs.SetInt("TypeCharacter", (int) newType);
    }

    public CharacterBase.BasicStats GetBasicStats(TypeCharacter type)
    {
        foreach (BasicInfoChar info in baseInfoChars)
        {
            if (info.typeChar == type)
            {
                return info.baseInfo;
            }
        }
        return baseInfoChars[0].baseInfo;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 150, 50), "Currente XP = " + GetCurrentXp());
        GUI.Label(new Rect(0, 20, 150, 50), "Currente Level = " + GetCurrentLevel());
        GUI.Label(new Rect(0, 40, 150, 50), "Currente Next XP = " + GetNextXP());
    }
}
