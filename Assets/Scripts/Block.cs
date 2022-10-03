using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int x, y;
    [SerializeField] Sprite darkSprite, lightSprite;
    [SerializeField] SpriteRenderer sprite, eggRenderer;
    public int powerIndex; //1-14
    [SerializeField] GameObject powerObject;
    Vector3 startPosition;
    public Vector2 tracePosition;
    [SerializeField] public int distanceTrace;
    public bool isChoosing, isCombining, isFalling;
    public int stepFall;
    public static bool s_isChoosing, s_isCombining;
    [SerializeField] PowerObjectController eggController;
    private void Awake()
    {
        isFalling = false;
        distanceTrace = 0;
        s_isChoosing = false;
        s_isCombining = false;
        isCombining = false;
    }
    public void Start()
    {
        startPosition = transform.position;
    }
    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
        powerIndex = Random.Range(1, 4);
        sprite.sortingOrder = 6 - y;
        eggRenderer.sortingOrder = 6 - y;
        setPowerIndexAndSprite(powerIndex);
        if ((x + y) % 2 == 0) sprite.sprite = lightSprite;
        else sprite.sprite = darkSprite;
    }
    public void setSprite()
    {
        isChoosing = false;
        powerObject.GetComponentInChildren<SpriteRenderer>().sprite = GameManager.instance.powerSprite[powerIndex].sprite;
    }
    public void setPowerIndex(int power)
    {
        powerIndex = power;
        eggController.powerIndex = power;
    }
    public void setPowerIndexAndSprite(int power)
    {
        powerIndex = power;
        eggController.powerIndex = power;
        setSprite();
    }
    // Update is called once per frame
    void Update()
    {
        //if (powerObject.transform.position.y < 1.5f)
        //    eggRenderer.enabled = true;
        //else eggRenderer.enabled = false;
        if (isChoosing == true)
        {
            greenUp();
            moveUp();
        }
        else
        {
            greenDown();
            moveDown();
        }
    }
    private void OnMouseDown()
    {
        if (Time.timeScale == 1)
            if (s_isChoosing == true && isChoosing == false)
            {
                GameController.instance.resetStatus();
                s_isChoosing = false;
            }
            else
            if (isChoosing == false && s_isCombining == false)
            {
                GameController.instance.bfs(x, y);
            }
            else if (isChoosing == true && s_isCombining == false)
            {
                GameController.instance.combine(x, y);
            }
    }
    #region move and green
    private void moveUp()
    {
        transform.position += new Vector3(0, 0.5f, 0) * Time.deltaTime;
        if (transform.position.y > startPosition.y + 0.08)
            transform.position = startPosition + new Vector3(0, 0.08f, 0);
    }
    private void moveDown()
    {
        transform.position += new Vector3(0, -0.5f, 0) * Time.deltaTime;
        if (transform.position.y < startPosition.y)
            transform.position = startPosition;
    }
    private void greenUp()
    {
        sprite.color = Color.green;
        //sprite.color -= new Color(speedColor, 0, speedColor,0)*Time.deltaTime; 
        // if(sprite.color.r<=0) sprite.color = new Color(0, 255, 0,255);
    }
    private void greenDown()
    {
        sprite.color = Color.white;
        //sprite.color += new Color(speedColor, 0, speedColor,0) * Time.deltaTime;
        //if (sprite.color.r >= 255) sprite.color = new Color(255, 255,255, 255);
    }
    #endregion
    public void moveToTrace()
    {
        float speed = 15;
        float x = powerObject.transform.position.x, y = powerObject.transform.position.y;
        powerObject.transform.position += new Vector3(tracePosition.x - x, tracePosition.y - y).normalized * speed * Time.deltaTime;
        if (Mathf.Abs(x - tracePosition.x) < 0.1f && Mathf.Abs(y - tracePosition.y) < 0.1f)
        {
            powerObject.transform.position = new Vector3(100, 100);
            isCombining = false;
            distanceTrace = 0;
            GameController.instance.countMaxDistance--;
            if (GameController.instance.countMaxDistance == 0)
                GameController.instance.decreaseMaxDistance();
        }
        else
            isChoosing = false;
    }
    public void fallDown()
    {
        float speed = 5f * stepFall;
        // float speed = 5 * stepFall;
        // y 2.425
        powerObject.transform.position += new Vector3(0, -1) * speed * Time.deltaTime;
        if (powerObject.transform.localPosition.y < 0)
        {
            powerObject.transform.localPosition = new Vector3(0, 0, 0);
            isFalling = false;
            stepFall = 0;
            //s_isFalling = false;
        }
    }
    public void setStepFall(int step)
    {
        if (step > 0)
            isFalling = true;
        powerObject.transform.localPosition = new Vector3(0, step * 2.425f, 0);
        stepFall = step;
    }
    public void powerUpAnim(int distance)
    {
        powerObject.GetComponentInChildren<PowerObjectController>().powerUp(distance);
    }
}
