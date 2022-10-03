using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject counter;
    public static TimeCounter instance;
    float timeCooldown;
    bool isPause;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        isPause = false;
        reset();
    }
  public  void Pause()
    {
        isPause = true;
    }
    void Continue()
    {
        isPause = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPause == false)
            timeCooldown -= Time.deltaTime;
        counter.transform.localScale = new Vector3(timeCooldown / 5f, 1, 1);
        if (timeCooldown <= 0&&isPause==false)
        {
            isPause = true;
            UI_GamePlay.instance.timeUp();
        }
    }
   public void reset()
    {
        isPause = false;
        timeCooldown = 5f;
    }
}
