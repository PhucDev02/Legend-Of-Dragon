using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public PowerObject[] powerSprite;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void executeBestScore(int x)
    {
        if (x > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", x);
            UI_GamePlay.instance.bestScoreText.text = "BEST " + PlayerPrefs.GetInt("BestScore").ToString();
            UI_GamePlay.instance.updateHighScore();
        }
    }
    public void updateAchievement(int x)
    {
        for (int i = 1; i <= x; i++)
        {
            powerSprite[i].isUnlock = true;
        }
    }
    public void saveGame()
    {
        string s = "0";
        for (int x = 1; x <= 5; x++)
        {
            for (int y = 1; y <= 10; y++)
            {
                s += GameController.instance.index[x][y].ToString("X");
            }
        }
        PlayerPrefs.SetString("lastGame", s);
        PlayerPrefs.SetInt("lastScore", GameController.instance.score);
        Debug.Log(PlayerPrefs.GetInt("lastScore"));
        Debug.Log(PlayerPrefs.GetString("lastGame"));
    }
    public void loadGame()
    {
        string s = PlayerPrefs.GetString("lastGame");
        Debug.Log(PlayerPrefs.GetString("lastGame"));
        int index = 1;
        for (int x = 1; x <= 5; x++)
        {
            for (int y = 1; y <= 10; y++)
            {
                GameController.instance.index[x][y] = HexToInt(s[index++].ToString());
            }
        }
        GameController.instance.score = PlayerPrefs.GetInt("lastScore");
    }
    public void newGame()
    {
        string s = "0";
        for (int x = 1; x <= 5; x++)
        {
            for (int y = 1; y <= 10; y++)
            {
                s += Random.Range(1, 4).ToString();
            }
        }
        PlayerPrefs.SetString("lastGame", s);
        PlayerPrefs.SetInt("lastScore", 0);
        Debug.Log("new game");
    }
    int HexToInt(string s)
    {
        switch (s)
        {
            case "1": return 1; 
            case "2": return 2; 
            case "3": return 3; 
            case "4": return 4; 
            case "5": return 5; 
            case "6": return 6; 
            case "7": return 7; 
            case "8": return 8; 
            case "9": return 9; 
            case "A": return 10;
            case "B": return 11;
            case "C": return 12;
            case "D": return 13;
            default: return 14;
        }
    }
}
