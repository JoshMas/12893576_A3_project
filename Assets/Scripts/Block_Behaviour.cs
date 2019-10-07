using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Behaviour : MonoBehaviour
{
    private Tetromino_Behaviour parentScript;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<Tetromino_Behaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rotate()
    {
        transform.Rotate(0, 0, -90, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        parentScript.Move(collision.gameObject.GetComponent<Border_Behaviour>().movement);
    }
}
