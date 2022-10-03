using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GridManager instance;
    int width, height;
    [SerializeField] GameObject block;
    private void Awake()
    {
        instance = this;
        width = 5;
        height = 5;
        for (int x = width; x >= 1; x--)
            for (int y = height; y >= 1; y--)
            {
                var spawnedBlock = Instantiate(block, new Vector3(x, y), Quaternion.identity, this.transform);
                spawnedBlock.name = $"Block {x} {y}";
                spawnedBlock.GetComponent<Block>().Init(x, y);
            }
        transform.localPosition = new Vector3(-6.13f, -7.29f);
    }
}
