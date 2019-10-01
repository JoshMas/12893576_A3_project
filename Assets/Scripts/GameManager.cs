using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // This moves the active tetromino down over time
        timer += Time.deltaTime;
        if (timer > counter)
        {
            counter += interval;
            activeBlock.GetComponent<Tetromino_Behaviour>().StepDown();
        }
    }

    public void RotateActiveBlock()
    {
        activeBlock.GetComponent<Tetromino_Behaviour>().Rotate();
    }


}
