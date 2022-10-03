using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AchievementsManager : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] Transform content;
   [SerializeField] Sprite lockSprite;
    [SerializeField] GameObject prefab;
    void Start()
    {
        for(int i=1;i<=14;i++)
        {
           var objTmp =Instantiate(prefab, content);
            if (GameManager.instance.powerSprite[i].isUnlock == false)
            {
                objTmp.GetComponent<Image>().sprite = lockSprite;
                objTmp.transform.GetChild(0).gameObject.SetActive(false); //tat object
            }
            else objTmp.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.powerSprite[i].shadeSprite;
            objTmp.transform.GetChild(0).GetComponent<Image>().SetNativeSize();  
            objTmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.ToString();
        }
       // Destroy(tmp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
