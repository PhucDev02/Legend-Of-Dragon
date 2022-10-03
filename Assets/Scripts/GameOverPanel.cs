using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Image[] stars;
    [SerializeField] Sprite starSprite;
    [SerializeField] Image bestPower;
    [SerializeField] Transform initPoint;
    [SerializeField] TextMeshProUGUI yourScore, bestScore;
    [SerializeField] GameObject lightImg, board;
    [SerializeField] GameObject gameTitle;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        bestPower.sprite = GameManager.instance.powerSprite[UI_GamePlay.instance.bestPower].sprite;
        bestPower.SetNativeSize();
        yourScore.text = GameController.instance.score.ToString();
        bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        int t, tmp = UI_GamePlay.instance.bestPower;
        if (tmp < 6)
            t = 1;
        else if (tmp < 11) t = 2;
        else t = 3;
        for (int i = 1; i <= t; i++)
            stars[i - 1].sprite = starSprite;
        board.transform.DOScale(1, 0.2f).SetEase(Ease.InSine).SetUpdate(true);
        gameTitle.transform.DOMoveY(5, 0.01f).SetUpdate(true).OnComplete(() => { gameTitle.transform.DOMoveY(3.5f, 0.2f).SetEase(Ease.InSine).SetUpdate(true); });
        for (int i = 0; i < 3; i++)
        {
            stars[i].transform.DOScale(1, 1f).SetEase(Ease.OutCubic).SetUpdate(true);
        }
        lightImg.transform.DORotate(new Vector3(0, 0, 90), 15).SetUpdate(true).SetLoops(10);
    }

}
