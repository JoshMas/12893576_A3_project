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

    //Rotates the block 90 degrees clockwise
    public void Rotate()
    {
        transform.Rotate(0, 0, 90, Space.Self);
    }

    //Changes whether the SpriteRenderer is enabled or not
    public void SetRenderer(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
    }

    //Detects collisions with other blocks and the game border, sending a vector back to the parent in order to move it back out
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Border_Behaviour>() != null)
        {
            parentScript.DetectBorderCollision(collision.gameObject.GetComponent<Border_Behaviour>().movement);
        } else
        {
            Vector3 distance = transform.parent.transform.position - collision.gameObject.transform.position;
            if (parentScript.CheckPosition() <= 1)
            {
                parentScript.DetectBorderCollision(Vector3.up);
            }
            else
            {
                parentScript.DetectBorderCollision(Vector3.Normalize(new Vector3(distance.x, 0.0f)));
            }
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
