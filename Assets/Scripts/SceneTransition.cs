using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject obj1, obj2;
    void Start()
    {
        obj1.transform.DOMoveX(-3.61f,0.01f, false).OnComplete(() =>
            { obj1.transform.DOMoveX(-7, 0.5f, false).SetEase(Ease.InSine); }
        );
        obj2.transform.DOMoveX(3.61f, 0.01f, false).OnComplete(() =>
        { obj2.transform.DOMoveX(7, 0.5f, false).SetEase(Ease.InSine); }
        );
    }

}
