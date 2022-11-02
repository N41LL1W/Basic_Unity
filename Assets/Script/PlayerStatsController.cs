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

    private float xpNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Application.LoadLevel("GamePlay");

        xpNextLevel = xpFirstLevel * (GetCurrentLevel() + 1) * difficultFactor;
        
        Debug.Log(xpNextLevel);
        
        PlayerPrefs.DeleteAll();
        AddXp(10);
        Debug.Log(GetCurrentXp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddXp(float xpAdd)
    {
        float newXp = (GetCurrentXp() + xpAdd) * PlayerStatsController.Instance.xpMultiply;
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
}
