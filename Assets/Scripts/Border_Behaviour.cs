using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_Behaviour : MonoBehaviour
{

    public Vector3 movement;
    [SerializeField]
    private bool isTop = false;

    //Detects when a tetromino is placed too high up, ending the game
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTop && !collision.GetComponentInParent<Tetromino_Behaviour>().enabled)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().GameOver();
        }
    }

}
