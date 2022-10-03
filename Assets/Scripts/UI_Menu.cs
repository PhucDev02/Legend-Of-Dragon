using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UI_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject playButton,continueButton, gameTitle;
    [SerializeField] GameObject confirmPanel;
    void Start()
    {
        playButton.transform.DOScale(1.2f, 1).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            playButton.transform.DOScale(1, 1).SetEase(Ease.InOutSine);
        }).SetLoops(-1, LoopType.Yoyo); 
        continueButton.transform.DOScale(1.1f, 1).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            continueButton.transform.DOScale(1, 1).SetEase(Ease.InOutSine);
        }).SetLoops(-1, LoopType.Yoyo);
        gameTitle.transform.DOScale(1.2f, 0.01f).OnComplete(() =>
        {
            gameTitle.transform.DOScale(1, 0.8f).SetEase(Ease.OutBounce);
        });
    }
    public void newGame()
    {
        GameManager.instance.newGame();
    }
    public void pressPlayButton()
    {
        if(PlayerPrefs.GetInt("lastScore")==0)
        {
            GameManager.instance.newGame();
            SceneManager.LoadScene("GamePlay");
        }
        else
        {
            confirmPanel.SetActive(true);
        }
    }

}
