using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Keeps track of how long the game has been going for
    private float timer = 0.0f;
    //Controls how often the active tetromino moves down automatically 
    private float interval = 1.1f;
    //Used as part of the timing system
    private float counter = 0.0f;

    //The block being moved by the player
    [SerializeField]
    private Tetromino_Behaviour activeBlock;

    //The block next in line
    [SerializeField]
    private Tetromino_Behaviour nextBlock;

    //The list of blocks that the game can give to the player
    [SerializeField]
    private List<GameObject> blockList = new List<GameObject>();

    //Keeps track of the score and speed of the game
    private SpeedManager speedManager;

    //Audio that plays when lines are cleared, or when the game ends
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

        //Checks if the active block can't move any further
        if (activeBlock.CheckPosition() == 0)
        {
            //If so, it then checks if any lines can be cleared for each line
            int count = 0;
            for(float i = 9.5f; i >= -9.5f; --i)
            {
                //If so, it clears that row, moving the blocks above that row one down
                if (DeleteAtHeight(i))
                {
                    ++count;
                    StartCoroutine(MoveBoardDown(i));
                }
            }
            //If any lines were cleared, it plays the line clear sound effect
            if(count > 0)
            {
                lineClearAudio.Play();
            }
            //It then updates the score and game speed
            interval = speedManager.UpdateScore(count);
            //And swaps the next block in, choosing a random block to take its place
            SwapActiveBlock();
        }
    }

    //Moves the active block down one
    public void StepDownActiveBlock()
    {
        activeBlock.MoveDown();
        
    }

    //Rotates the active block
    public void RotateActiveBlock()
    {
        activeBlock.Rotate();
    }

    //Moves the active block either left or right
    public void MoveActiveBlock(float dist)
    {
        activeBlock.Move(new Vector3(dist, 0.0f));
    }

    //This method handles the block swapping whenever a piece is dropped
    public void SwapActiveBlock()
    {
        //It sets the next block as the active block
        Tetromino_Behaviour temp = activeBlock;
        activeBlock = nextBlock;
        //Then moves it into position
        activeBlock.transform.position = new Vector3(0, 9);
        //The next block is chosen from the block list
        nextBlock = SelectNextBlock(nextBlock);
        //Then enables the 'Destroy if Empty' script on the old block, while disabling its main script
        temp.gameObject.GetComponent<DestroyIfEmpty>().enabled = true;
        temp.enabled = false;
    }

    //This method handles selecting the next block
    private Tetromino_Behaviour SelectNextBlock(Tetromino_Behaviour previousBlock)
    {
        //It chooses a random tetromino from the list of available ones
        GameObject newBlock;
        newBlock = blockList[Random.Range(0, blockList.Count)];
        //Then it instantiates that block in the correct location
        GameObject chosenBlock = Instantiate(newBlock, new Vector3(11, -3), Quaternion.identity);
        //And returns the script attached to it
        return chosenBlock.GetComponent<Tetromino_Behaviour>();
    }

    //This method clears a line if there are 10 blocks in it
    private bool DeleteAtHeight(float height)
    {
        //First, it puts every block at that height into a list
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        List<GameObject> blocksToDestroy = new List<GameObject>();
        foreach(GameObject block in blocks)
        {
            if(Mathf.Abs(height - block.transform.position.y) < 0.1 && block.transform.position.x < 6)
            {
                blocksToDestroy.Add(block);
            }
        }
        //Then, if that list has 10 blocks in it, it gets cleared, and the method returns true
        if (blocksToDestroy.Count >= 10)
        {
            foreach (GameObject block in blocksToDestroy)
            {
                //Also, if one of the blocks was the 'boss' the player wins
                if (block.GetComponent<Block_Behaviour>().IsTheBoss())
                {
                    GameWin();
                }
                else
                {
                    block.GetComponent<Block_Behaviour>().SetClearTrigger();
                }
            }
            return true;
        }
        return false;
    }

    //This Coroutine moves all blocks above a certain height down by one
    //It does this to allow the clear animation to play out fully
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

    //This method is called when the game ends in a loss for the player
    public void GameOver()
    {
        enabled = false;
        gameOverAudio.Play();
        //All blocks on screen play their game over animation
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in allBlocks)
        {
            block.GetComponent<Block_Behaviour>().SetGameOverTrigger();
        }
        //The player is prompted to exit the game
        speedManager.GameOver("Game Over\nPress Esc to Exit");
    }

    //This method is called when the player beats the boss
    public void GameWin()
    {
        lineClearAudio.Play();
        //The player is given a large score increase
        speedManager.UpdateScore(20);
        enabled = false;
        //All blocks on screen play their line clear animation
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in allBlocks)
        {
            block.GetComponent<Block_Behaviour>().SetClearTrigger();
        }
        //The player is prompted to exit the game
        speedManager.GameOver("You Win!\nPress Esc to Exit");
    }
}
