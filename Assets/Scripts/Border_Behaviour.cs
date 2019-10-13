using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_Behaviour : MonoBehaviour
{

    public Vector3 movement;
    [SerializeField]
    private bool isTop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTop && !collision.GetComponentInParent<Tetromino_Behaviour>().enabled)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().GameOver();
        }
    }

}
