using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino_Behaviour : MonoBehaviour
{
    //The position the tetromino was in on the row above
    private Vector3 previousPosition;
    //The vector used to move the tetromino when a collision occurs
    private Vector3 borderCollision;
    //The scripts attached to the blocks that make up this tetromino
    private List<Block_Behaviour> scripts;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = new Vector3(0, 10, 0);
        borderCollision = new Vector3(0, 0, 0);
        scripts = new List<Block_Behaviour>();
        Block_Behaviour[] list = GetComponentsInChildren<Block_Behaviour>();
        foreach (Block_Behaviour item in list)
        {
            scripts.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RestrictTransform();
        //If it has collided with anything, move in the border collision direction by one
        if (borderCollision.magnitude > 0)
        {
            transform.Translate(borderCollision.normalized, Space.World);
            borderCollision.Set(0, 0, 0);
        }
    }

    // Keeps the x and y position of the tetromino on integer values
    private void RestrictTransform()
    {
        if (transform.position.x % 1 != 0)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        }
    }

    //Rotates the tetromino 90 degrees counterclockwise
    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, -90), Space.Self);
        foreach (Block_Behaviour block in scripts)
        {
            block.Rotate();
        }
    }

    //Moves the tetromino 1 space down, also moving the previousPosition
    public void MoveDown()
    {
        previousPosition = transform.position;
        transform.Translate(new Vector3(0.0f, -1.0f), Space.World);
    }

    //Moves the tetromino 1 space in the specified direction
    public void Move(Vector3 direction)
    {
        transform.Translate(direction, Space.World);
    }

    //Returns the distance between the current position and the previous position
    public float CheckPosition()
    {
        return Vector3.Distance(transform.position, previousPosition);
    }

    //Adds the direction to its border collision vector
    public void DetectBorderCollision(Vector3 direction)
    {
        borderCollision += direction;
    }
}
