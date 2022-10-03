using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Scale : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] float scale,time,disableScale;
    public void Awake()
    {
        transform.DOScale(scale, time).SetEase(Ease.InSine);
        transform.DOScale(disableScale, time).SetEase(Ease.InSine);
    }
    public void Start()
    {
        transform.DOScale(scale, time).SetEase(Ease.InSine);
    }
    public void OnEnable()
    {
        transform.DOScale(scale, time).SetEase(Ease.InSine);
    }
    public void OnDisable()
    {
        transform.DOScale(disableScale, time).SetEase(Ease.InSine);
    }
}
