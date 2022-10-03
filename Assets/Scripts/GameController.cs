using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Block[][] blocks;
    public int[][] index;
    // Start is called before the first frame update
    [SerializeField] GameObject tileHolder;
    public int maxDistance, countMaxDistance, distanceFall;
    Vector2Int combinePosition;
    int countBlockSelected;
    public int score;
    private void Awake()
    {
        countMaxDistance = maxDistance = 0;
        instance = this;
        blocks = new Block[6][];
        for (int i = 0; i < 6; i++) blocks[i] = new Block[6];
        index = new int[6][];
        for (int i = 0; i < 6; i++) index[i] = new int[11];
    }
    private void Start()
    {
        int id = 0;
        GameManager.instance.updateAchievement(3);
        GameManager.instance.loadGame();
        score = PlayerPrefs.GetInt("lastScore");
        for (int x = 5; x >= 1; x--)
        {
            for (int y = 5; y >= 1; y--)
            {
                blocks[x][y] = tileHolder.transform.GetChild(id++).gameObject.GetComponent<Block>();
                //index[x][y] = blocks[x][y].powerIndex;
                blocks[x][y].setPowerIndexAndSprite(index[x][y]);
            }
        }
        //for (int x = 1; x <= 5; x++)
        //    for (int y = 6; y <= 10; y++)
        //    {
        //        index[x][y] = randomIndex();
        //    }
        executeBestPowerIndex();
        UI_GamePlay.instance.updateScore(score);
        GameManager.instance.saveGame();
    }
    private void Update()
    {

        if (Block.s_isCombining)
        {
            for (int x = 1; x <= 5; x++)
                for (int y = 1; y <= 5; y++)
                {
                    if (blocks[x][y].isCombining && blocks[x][y].distanceTrace == maxDistance)
                        blocks[x][y].moveToTrace();
                }
        }
        for (int x = 1; x <= 5; x++)
            for (int y = 1; y <= 5; y++)
            {
                if (blocks[x][y].isFalling)
                    blocks[x][y].fallDown();
            }
    }
    public void updateIndex()
    {
        //for (int x = 1; x <= 5; x++)
        //    for (int y = 1; y <= 5; y++)
        //    {
        //        index[x][y] = blocks[x][y].powerIndex;
        //    }
        for (int x = 1; x <= 5; x++)
            for (int y = 6; y <= 10; y++)
            {
                index[x][y] = randomIndex();
            }
        GameManager.instance.saveGame();
    }
    public void bfs(int x, int y)
    {
        AudioManager.Instance.Play("SelectBlock");
        countBlockSelected = 0;
        bool[][] check = new bool[6][];
        for (int i = 0; i < 6; i++) check[i] = new bool[6];
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        Queue<Vector2Int> res = new Queue<Vector2Int>();
        q.Enqueue(new Vector2Int(x, y));
        while (q.Count > 0)
        {
            Vector2Int tmp = q.Dequeue();
            res.Enqueue(tmp);
            //01 10 0-1 -10
            check[tmp.x][tmp.y] = true;
            if (tmp.y <= 4 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x][tmp.y + 1].powerIndex && !check[tmp.x][tmp.y + 1])
                q.Enqueue(new Vector2Int(tmp.x, tmp.y + 1));
            if (tmp.x <= 4 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x + 1][tmp.y].powerIndex && !check[tmp.x + 1][tmp.y])
                q.Enqueue(new Vector2Int(tmp.x + 1, tmp.y));
            if (tmp.y >= 2 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x][tmp.y - 1].powerIndex && !check[tmp.x][tmp.y - 1])
                q.Enqueue(new Vector2Int(tmp.x, tmp.y - 1));
            if (tmp.x >= 2 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x - 1][tmp.y].powerIndex && !check[tmp.x - 1][tmp.y])
                q.Enqueue(new Vector2Int(tmp.x - 1, tmp.y));
        }
        if (res.Count > 1)
        {
            countBlockSelected = res.Count;
            // Debug.Log(res.Count);
            Block.s_isChoosing = true;
            while (res.Count > 0)
            {
                Vector2Int tmp = res.Dequeue();
                blocks[tmp.x][tmp.y].isChoosing = true;
            }
        }
    }
    private void trace(int x, int y)
    {
        bool[][] check = new bool[6][];
        for (int i = 0; i < 6; i++) check[i] = new bool[6];
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(new Vector2Int(x, y));
        while (q.Count > 0)
        {
            Vector2Int tmp = q.Dequeue();
            //01 10 0-1 -10
            check[tmp.x][tmp.y] = true;
            if (tmp.y <= 4 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x][tmp.y + 1].powerIndex && !check[tmp.x][tmp.y + 1])
            {
                q.Enqueue(new Vector2Int(tmp.x, tmp.y + 1));
                blocks[tmp.x][tmp.y + 1].tracePosition = blocks[tmp.x][tmp.y].transform.position;
                blocks[tmp.x][tmp.y + 1].distanceTrace = blocks[tmp.x][tmp.y].distanceTrace + 1;
            }
            if (tmp.x <= 4 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x + 1][tmp.y].powerIndex && !check[tmp.x + 1][tmp.y])
            {
                q.Enqueue(new Vector2Int(tmp.x + 1, tmp.y));
                blocks[tmp.x + 1][tmp.y].tracePosition = blocks[tmp.x][tmp.y].transform.position;
                blocks[tmp.x + 1][tmp.y].distanceTrace = blocks[tmp.x][tmp.y].distanceTrace + 1;
            }
            if (tmp.y >= 2 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x][tmp.y - 1].powerIndex && !check[tmp.x][tmp.y - 1])
            {
                q.Enqueue(new Vector2Int(tmp.x, tmp.y - 1));
                blocks[tmp.x][tmp.y - 1].tracePosition = blocks[tmp.x][tmp.y].transform.position;
                blocks[tmp.x][tmp.y - 1].distanceTrace = blocks[tmp.x][tmp.y].distanceTrace + 1;
            }
            if (tmp.x >= 2 && blocks[tmp.x][tmp.y].powerIndex == blocks[tmp.x - 1][tmp.y].powerIndex && !check[tmp.x - 1][tmp.y])
            {
                q.Enqueue(new Vector2Int(tmp.x - 1, tmp.y));
                blocks[tmp.x - 1][tmp.y].tracePosition = blocks[tmp.x][tmp.y].transform.position;
                blocks[tmp.x - 1][tmp.y].distanceTrace = blocks[tmp.x][tmp.y].distanceTrace + 1;
            }
        }
    }
    public void combine(int x, int y)
    {
        AudioManager.Instance.Play("Combine");
        score += countBlockSelected * blocks[x][y].powerIndex;
        distanceFall = 0;
        combinePosition = new Vector2Int(x, y);
        trace(x, y);
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 5; j++)
            {
                updateMaxDistance(blocks[i][j].distanceTrace);
                if (i == x && j == y)
                    continue;
                if (blocks[i][j].isChoosing == true)
                {
                    blocks[i][j].setPowerIndex(0);
                    blocks[i][j].isCombining = true;
                    // index[i][j] = 0;
                }
            }
        }
        index[x][y] = blocks[x][y].powerIndex + 1;
        blocks[x][y].setPowerIndex(blocks[x][y].powerIndex + 1);
        for (int i = combinePosition.y; i >= 1; i--)
        {
            if (blocks[combinePosition.x][i].powerIndex == 0) distanceFall++;
        }
        CountMaxDistance();
        Block.s_isCombining = true;
    }
    public void fallDown()
    {
        for (int y = 1; y <= 5; y++)
        {
            for (int x = 1; x <= 5; x++)
            {
                int count = 0;
                if (blocks[x][y].powerIndex == 0)
                {
                    //update index
                    for (int i = y; i <= 5; i++)
                        if (blocks[x][i].powerIndex == 0) count++;
                        else break;
                    for (int cnt = 0; cnt < 10 - count - y; cnt++)
                    {
                        index[x][y + cnt] = index[x][y + cnt + count];
                    }                    //
                }
            }
        }
        for (int x = 1; x <= 5; x++)
        {
            for (int y = 1; y <= 5; y++)
            {
                if (blocks[x][y].powerIndex == 0)
                {
                    for (int k = y + 1; k <= 5; k++)
                    {
                        if (blocks[x][k].powerIndex > 0)
                        {
                            blocks[x][y].setPowerIndexAndSprite(index[x][y]);
                            blocks[x][k].setPowerIndex(0);
                            blocks[x][y].setStepFall(k - y);
                            break;
                        }
                    }
                    if (blocks[x][y].powerIndex == 0)
                    {
                        int tmp = 6 - y;
                        for (int i = y; i <= 5; i++)
                        {
                            blocks[x][i].setPowerIndexAndSprite(index[x][i]);
                            blocks[x][i].setStepFall(tmp);
                        }
                        break;
                    }
                }
            }
        }
        updateIndex();
        TimeCounter.instance.reset();
    }
    public void resetStatus()
    {
        for (int x = 5; x >= 1; x--)
        {
            for (int y = 5; y >= 1; y--)
            {
                blocks[x][y].isChoosing = false;
            }
        }
    }
    public void updateMaxDistance(int x)
    {
        maxDistance = Mathf.Max(maxDistance, x);
    }
    public void decreaseMaxDistance()
    {
        maxDistance--;
        if (maxDistance == 0)
        {
            Block.s_isChoosing = false;
            Block.s_isCombining = false;
            blocks[combinePosition.x][combinePosition.y].setSprite();
            blocks[combinePosition.x][combinePosition.y - distanceFall].powerUpAnim(distanceFall);
            UI_GamePlay.instance.updateScore(score);
            executeBestPowerIndex();
            fallDown();
            checkGameOver();
        }
        else
            CountMaxDistance();
    }
    void CountMaxDistance()
    {
        for (int x = 1; x <= 5; x++)
            for (int y = 1; y <= 5; y++)
                if (blocks[x][y].isCombining && blocks[x][y].distanceTrace == maxDistance)
                    countMaxDistance++;
    }
    void executeBestPowerIndex()
    {
        int max = 0;
        for (int x = 1; x <= 5; x++)
        {
            for (int y = 1; y <= 5; y++)
            {
                max = Mathf.Max(index[x][y], max);
            }
        }
        UI_GamePlay.instance.updateBestPower(max);
        GameManager.instance.updateAchievement(max);
    }
    void checkGameOver()
    {
        bool flag = true;
        for (int x = 1; x <= 5; x++)
            for (int y = 1; y <= 5; y++)
                if (index[x][y] == index[x - 1][y] || index[x][y] == index[x][y - 1])
                {
                    flag = false;
                }
        if (flag == true)
        {
            StartCoroutine(waitGameOver(0.7f)); //wait fall down
        }
    }
    IEnumerator waitGameOver(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UI_GamePlay.instance.gameOver();
    }
    int randomIndex()
    {
        //return Random.Range(7, 9);
        return Random.Range(1, Mathf.Max(4, UI_GamePlay.instance.bestPower * 2 / 3));
    }
}
