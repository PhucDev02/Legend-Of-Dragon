using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject boundSprite;

    void Start()
    {
        Camera.main.orthographicSize = boundSprite.GetComponent<SpriteRenderer>().bounds.size.x * Screen.height / Screen.width * 0.5f;
        float x = boundSprite.GetComponent<SpriteRenderer>().bounds.size.y / boundSprite.GetComponent<SpriteRenderer>().bounds.size.x;
        boundSprite.transform.localScale = new Vector3(1, (float)Screen.height / Screen.width / x);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
