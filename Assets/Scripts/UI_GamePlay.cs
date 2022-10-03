using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UI_GamePlay : MonoBehaviour
{
    public static UI_GamePlay instance;
    [SerializeField] Image bestPowerImg;
    [SerializeField] TextMeshProUGUI bestPowerText, scoreText;
    public TextMeshProUGUI bestScoreText;
    [SerializeField] Sprite[] bestPowerSprite;
    [SerializeField] Vector2Int[] bestPowerSpriteSize;
    [SerializeField] public int bestPower;
    [SerializeField] GameObject gameOverPanel, scoreBoard;
    [SerializeField] GameObject noMoreMoveImage,timeUpImage;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        updateScore(0);
        bestScoreText.text = "BEST " + PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void updateBestPower(int x)
    {
        if (x != bestPower)
        {
            bestPower = x;
            bestPowerText.text = x.ToString();
            changeBestPowerImg();
        }
    }
    public void changeBestPowerImg()
    {
        bestPowerImg.sprite = bestPowerSprite[bestPower];
        bestPowerImg.rectTransform.sizeDelta = bestPowerSpriteSize[bestPower];
        bestPowerImg.transform.DOScale(1.5f, 0.2f).OnComplete(() =>
        {
            bestPowerImg.transform.DOScale(1, 0.2f).SetEase(Ease.OutCubic);
        });

    }
    public void updateScore(int x)
    {
        scoreText.text = x.ToString();
        GameManager.instance.executeBestScore(x);
        scoreText.transform.DOScale(1.2f, 0.1f).OnComplete(() =>
         {
             scoreText.transform.DOScale(1, 0.1f).SetEase(Ease.OutCubic);
         });
    }
    public void gameOver()
    {
        noMoreMoveImage.transform.DOMoveX(0, 1.5f, false).SetEase(Ease.OutBounce).SetUpdate(true);
        Time.timeScale = 0;
        StartCoroutine(waitGameOver(1.6f));
    }
    public void timeUp()
    {
        timeUpImage.transform.DOMoveX(0, 1.5f, false).SetEase(Ease.OutBounce).SetUpdate(true);
        
        Time.timeScale = 0;
        StartCoroutine(waitGameOver(1.5f));
    }
    IEnumerator waitGameOver(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        scoreBoard.SetActive(false);
        gameOverPanel.SetActive(true);
        noMoreMoveImage.SetActive(false);
        AudioManager.Instance.Play("GameOver");
        GameManager.instance.newGame();
    }
    public void updateHighScore()
    {
        bestScoreText.transform.DOScale(1.5f, 0.2f).OnComplete(() =>
        {
            bestScoreText.transform.DOScale(1, 0.2f).SetEase(Ease.InCubic);
        });
    }
}
