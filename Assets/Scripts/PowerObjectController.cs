using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PowerObjectController : MonoBehaviour
{
    [SerializeField] Animator animator, powerUpAnimator, shadeAnim;
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer[] spriteRender;
    [SerializeField] GameObject eggBreak, dragonBreak, dragon10, dragon11, dragon12, dragon13, dragon14;
    public int powerIndex;
  [SerializeField]  float cooldown,cooldownSetActive;
   [SerializeField] bool isJustIdle;
    [SerializeField] GameObject shade;
    void Start()
    {
        appear();
        cooldown = Random.Range(1f, 15f);
        cooldownSetActive = 1.5f;
        isJustIdle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isJustIdle==true)
        {
            cooldownSetActive -= Time.deltaTime;
            if(cooldownSetActive<0)
            {
                isJustIdle = false;
                cooldownSetActive = 1.5f;
                setActiveAnim();
            }
        }
        if (powerIndex == 1 || powerIndex >= 8) shade.SetActive(false);
        else shade.SetActive(true);
        //if (transform.position.y <= 1.8f)
        //{
        //    foreach (var tmp in spriteRender)
        //    {
        //        tmp.enabled = true;
        //    }
        //    //spriteRender.enabled = true;
        //}
        //else
        //    foreach (var tmp in spriteRender)
        //    {
        //        tmp.enabled = false;
        //    }
        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            executeIdleAnim();
            cooldown = Random.Range(10f, 15f);
        }
    }
    public void powerUp(int distance)
    {
        int tmpX = transform.parent.parent.GetComponent<Block>().x;
        int tmpY = transform.parent.parent.GetComponent<Block>().y;
        powerIndex = GameController.instance.blocks[tmpX][tmpY + distance].powerIndex;
        cooldown = Random.Range(10f, 15f);
        executeIdleAnim();
        executePowerUpAnim();
    }
    void appear()
    {
        shadeAnim.Play("Appear");
        animator.Play("Appear");
    }
    void jump()
    {
        animator.Play("Jump");
        shadeAnim.Play("Jump");
    }
    void fly()
    {
        shadeAnim.Play("Fly");
        animator.Play("Fly");
    }
    void dragon10Anim()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        dragon10.SetActive(true);
        isJustIdle = true;
    }
    void dragon11Anim()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dragon11.SetActive(true);
        isJustIdle = true;
    }
    void dragon12Anim()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dragon12.SetActive(true);
        isJustIdle = true;
    }
    void dragon13Anim()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dragon13.SetActive(true);
        isJustIdle = true;
    }
    void dragon14Anim()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dragon14.SetActive(true);
        isJustIdle = true;
    }
    void eggBreakAnim()
    {
        AudioManager.Instance.Play("EggBreak");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        eggBreak.SetActive(true);
        isJustIdle = true;
    }
    void dragonBreakAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dragonBreak.SetActive(true);
        isJustIdle = true;
    }
    void executeIdleAnim()
    {
        switch (powerIndex)
        {
            case 1:
                break;
            case 2:
                jump();
                break;
            case 3:
                jump();
                break;
            case 4:
                jump();
                break;
            case 5:
                jump();
                break;
            case 6:
                fly();
                break;
            case 7:
                fly();
                break;
            case 8:
                eggBreakAnim(); break;
            case 9:
                dragonBreakAnim(); break;
            case 10:
                dragon10Anim(); break;
            case 11:
                dragon11Anim(); break;
            case 12:
                dragon12Anim(); break;
            case 13:
                dragon13Anim(); break;
            case 14:
                dragon14Anim();
                break;
        }
    }
    void executePowerUpAnim()
    {
        switch (powerIndex)
        {
            case 1:
                break;
            case 2:
                powerUpAnimator.Play("PowerUp");
                break;
            case 3:
                powerUpAnimator.Play("PowerUp");
                break;
            case 4:
                powerUpAnimator.Play("PowerUp");
                break;
            case 5:
                powerUpAnimator.Play("PowerUp");
                break;
            case 6:
                blink();
                break;
            case 7:
                blink();
                break;
            case 8: blink();
                break;
            case 9:
                powerUpAnimator.Play("PowerUp");
                break;
            default:
                powerUpAnimator.Play("PowerUp");
                break;
        }
    }
    public void blink()
    {
        powerUpAnimator.Play("PowerUpBlink");
        AudioManager.Instance.Play("Blink");
    }
    IEnumerator wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;;
        dragonBreak.SetActive(false);
        eggBreak.SetActive(false);
        dragon10.SetActive(false);
        dragon11.SetActive(false);
        dragon12.SetActive(false);
        dragon13.SetActive(false);
        dragon14.SetActive(false);
        Debug.Log("adu");
    }
    void setActiveAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        dragonBreak.SetActive(false);
        eggBreak.SetActive(false);
        dragon10.SetActive(false);
        dragon11.SetActive(false);
        dragon12.SetActive(false);
        dragon13.SetActive(false);
        dragon14.SetActive(false);
    }
}
