using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PowerObject", menuName = "PowerObject")]
public class PowerObject : ScriptableObject
{
    public int powerIndex;
    public Sprite sprite,shadeSprite;
    public bool isUnlock;
}
