using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row_Behaviour : MonoBehaviour
{
    float blockCount;

    // Start is called before the first frame update
    void Start()
    {
        blockCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        blockCount++;        
    }

    public bool IsRowFull()
    {
        return blockCount >= 10;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        blockCount--;
    }
}
