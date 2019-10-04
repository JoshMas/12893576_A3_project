using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino_Behaviour : MonoBehaviour
{

    private bool isActive = false;
    private List<Block_Behaviour> scripts;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }

    public void StepDown()
    {
        transform.Translate(new Vector3(0.0f, -0.64f), Space.World);
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, 90), Space.Self);  
        foreach (Block_Behaviour block in scripts)
        {
            block.Rotate();
        }
    }

    public void Move(float dist)
    {
        transform.Translate(new Vector3(dist, 0, 0));
    }
}
