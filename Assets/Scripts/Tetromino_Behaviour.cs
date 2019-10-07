using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino_Behaviour : MonoBehaviour
{
    private Vector3 previousPosition;
    private Vector3 borderCollision;
    private bool isActive = false;
    private List<Block_Behaviour> scripts;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = new Vector3(0, 0, 0);
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
        if (borderCollision.magnitude > 0)
        {
            Move(Vector3.Normalize(borderCollision));
            borderCollision.Set(0, 0, 0);
        }
    }

    /*
    public void StepDown()
    {
        previousPosition = transform.position;
        transform.Translate(new Vector3(0.0f, -1.0f), Space.World);
    }
    */

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, 90), Space.Self);  
        foreach (Block_Behaviour block in scripts)
        {
            block.Rotate();
        }
    }

    public void Move(Vector3 direction)
    {
        previousPosition = transform.position;
        transform.Translate(direction, Space.World);
    }

    public void MoveBack()
    {
        transform.position = previousPosition;
    }

    public void DetectBorderCollision(Vector3 direction)
    {
        borderCollision += direction;
    }
    
}
