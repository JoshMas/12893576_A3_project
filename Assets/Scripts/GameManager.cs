﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float timer = 0.0f;
    [SerializeField]
    private float interval = 1.0f;
    private float counter = 0.0f;

    [SerializeField]
    private GameObject activeBlock;

    [SerializeField]
    private GameObject nextBlock;

    [SerializeField]
    private List<GameObject> blockList;

    private GameObject[] rowList;

    // Start is called before the first frame update
    void Start()
    {
        rowList = GameObject.FindGameObjectsWithTag("Row");
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
        if (activeBlock.GetComponent<Tetromino_Behaviour>().CheckPosition() == 0)
        {
            SwapActiveBlock();
            foreach (GameObject row in rowList)
            {
                Row_Behaviour script = row.GetComponent<Row_Behaviour>();
                if (script.IsRowFull())
                {
                    DeleteAtHeight(row.transform.position.y);
                    script.ClearRow();
                    MoveBoardDown(row.transform.position.y);
                }
            }
        }
    }

    public void StepDownActiveBlock()
    {
        activeBlock.GetComponent<Tetromino_Behaviour>().MoveDown();
        
    }

    public void RotateActiveBlock()
    {
        activeBlock.GetComponent<Tetromino_Behaviour>().Rotate();
    }

    public void MoveActiveBlock(float dist)
    {
        activeBlock.GetComponent<Tetromino_Behaviour>().Move(new Vector3(dist, 0.0f));
    }

    public void SwapActiveBlock()
    {
        nextBlock.GetComponent<Tetromino_Behaviour>().Move(new Vector3(-11, 3));
        GameObject temp = activeBlock;
        activeBlock = nextBlock;
        activeBlock.transform.position = new Vector3(0, 9);
        nextBlock = SelectNextBlock(nextBlock);
        temp.GetComponent<Tetromino_Behaviour>().enabled = false;
    }

    private GameObject SelectNextBlock(GameObject previousBlock)
    {

        GameObject newBlock;
        do
        {
            newBlock = blockList[Random.Range(0, blockList.Count)];
        } while (newBlock.Equals(previousBlock));
        GameObject chosenBlock = Instantiate(newBlock, new Vector3(11, -3), Quaternion.identity);
        return chosenBlock;
    }

    private void DeleteAtHeight(float height)
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        List<GameObject> blocksToDestroy = new List<GameObject>();
        foreach(GameObject block in blocks)
        {
            if(block.transform.position.y == height)
            {
                blocksToDestroy.Add(block);
            }
        }

        foreach(GameObject block in blocksToDestroy)
        {
            Destroy(block);
        }
    }

    public void MoveBoardDown(float height)
    {
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in allBlocks)
        {
            if(block.transform.position.y > height)
            {
                block.transform.Translate(Vector3.down);
            }
        }
    }
}
