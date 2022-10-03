using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public static UI_Controller instance;
    [SerializeField] Image soundButton, musicButton;
    public Sprite musicOn, musicOff, soundOn, soundOff;
    private void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        if (PlayerPrefs.GetInt("allowMusic") == 0)
        {
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
        else musicButton.GetComponent<Image>().sprite = musicOff;
        if (PlayerPrefs.GetInt("allowSound") == 0)
        {
            soundButton.GetComponent<Image>().sprite = soundOn;
        }
        else soundButton.GetComponent<Image>().sprite = soundOff;
    }
    public void resetAchievement()
    {
        for(int i=2;i<=14;i++)
        {
            GameManager.instance.powerSprite[i].isUnlock = false;
        }

    }
    public void pauseGame()
    {
        Time.timeScale = 0;
    }
    public void playAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GamePlay");
    }
    public void changeTimeScale()
    {
        Time.timeScale = 1 - Time.timeScale;
    }
    public void stopTime()
    {
        Time.timeScale = 0;
    }
    public void continueTime()
    {
        Time.timeScale = 1;
    }
    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void openFacebook()
    {
        Application.OpenURL("https://www.facebook.com/100010587756741");
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSound()
    {
        AudioManager.Instance.ToggleSound();
    }
    public void clickSound()
    {
        AudioManager.Instance.Play("Click");
    }
    public void notificationSound()
    {
        AudioManager.Instance.Play("Notification");
    }
    public void newGame()
    {
        GameManager.instance.newGame();
    }
}
