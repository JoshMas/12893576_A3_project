using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Behaviour : MonoBehaviour
{

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        GetComponent<Block_Behaviour>().IAmTheBoss();
    }

}
