using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float timer = 0.0f;
    private float interval = 1.1f;
    private float counter = 0.0f;

    [SerializeField]
    private Tetromino_Behaviour activeBlock;

    [SerializeField]
    private Tetromino_Behaviour nextBlock;

    [SerializeField]
    private List<GameObject> blockList = new List<GameObject>();

    private SpeedManager speedManager;

    [SerializeField]
    private AudioSource lineClearAudio = null;
    [SerializeField]
    private AudioSource gameOverAudio = null;

    // Start is called before the first frame update
    void Start()
    {
        speedManager = gameObject.GetComponent<SpeedManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // This moves the active tetromino down over time
        timer += Time.deltaTime;
        if (timer > counter)
        {
            counter += interval;
            StepDownActiveBlock();
        }

        if (activeBlock.CheckPosition() == 0)
        {
            int count = 0;
            for(float i = 9.5f; i >= -9.5f; --i)
            {
                if (DeleteAtHeight(i))
                {
                    ++count;
                    StartCoroutine(MoveBoardDown(i));
                }
            }
            if(count > 0)
            {
                lineClearAudio.Play();
            }
            interval = speedManager.UpdateScore(count);
            SwapActiveBlock();
        }
    }

    public void StepDownActiveBlock()
    {
        activeBlock.MoveDown();
        
    }

    public void RotateActiveBlock()
    {
        activeBlock.Rotate();
    }

    public void MoveActiveBlock(float dist)
    {
        activeBlock.Move(new Vector3(dist, 0.0f));
    }

    public void SwapActiveBlock()
    {
        nextBlock.Move(new Vector3(-11, 3));
        Tetromino_Behaviour temp = activeBlock;
        activeBlock = nextBlock;
        activeBlock.transform.position = new Vector3(0, 9);
        nextBlock = SelectNextBlock(nextBlock);
        temp.gameObject.GetComponent<DestroyIfEmpty>().enabled = true;
        temp.enabled = false;
    }

    private Tetromino_Behaviour SelectNextBlock(Tetromino_Behaviour previousBlock)
    {
        GameObject newBlock;
        newBlock = blockList[Random.Range(0, blockList.Count)];
        while(newBlock.name == previousBlock.gameObject.name)
        {
            newBlock = blockList[Random.Range(0, blockList.Count)];
        }
        GameObject chosenBlock = Instantiate(newBlock, new Vector3(11, -3), Quaternion.identity);
        return chosenBlock.GetComponent<Tetromino_Behaviour>();
    }

    private bool DeleteAtHeight(float height)
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        List<GameObject> blocksToDestroy = new List<GameObject>();
        foreach(GameObject block in blocks)
        {
            if(Mathf.Abs(height - block.transform.position.y) < 0.1 && block.transform.position.x < 6)
            {
                blocksToDestroy.Add(block);
            }
        }
        //Debug.Log("Row: " + (height + 11.5) + "| Blocks: " + blocksToDestroy.Count);
        if (blocksToDestroy.Count >= 10)
        {
            foreach (GameObject block in blocksToDestroy)
            {
                if (block.GetComponent<Block_Behaviour>().IsTheBoss())
                {
                    GameWin();
                }
                else
                {
                    block.GetComponent<Block_Behaviour>().SetClearTrigger();
                }
                //block.transform.Translate(new Vector3(0, -100));
                //Destroy(block);
            }
            return true;
        }
        return false;
    }

    IEnumerator MoveBoardDown(float height)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in allBlocks)
        {
            if(block.transform.position.y > height && block.transform.position.x < 6)
            {
                block.transform.Translate(Vector3.down);
            }
        }
    }

    public void GameOver()
    {
        enabled = false;
        gameOverAudio.Play();
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in allBlocks)
        {
            block.GetComponent<Block_Behaviour>().SetGameOverTrigger();
        }
        speedManager.GameOver("Game Over\nPress Esc to Exit");
    }

    public void GameWin()
    {
        lineClearAudio.Play();
        speedManager.UpdateScore(20);
        enabled = false;
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in allBlocks)
        {
            block.GetComponent<Block_Behaviour>().SetClearTrigger();
        }
        speedManager.GameOver("You Win!\nPress Esc to Exit");
    }
}
