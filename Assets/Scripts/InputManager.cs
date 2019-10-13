using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("r"))
        {
            gameManager.RotateActiveBlock();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameManager.MoveActiveBlock(-1.0f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameManager.MoveActiveBlock(1.0f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameManager.StepDownActiveBlock();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
