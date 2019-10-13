using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfEmpty : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEmpty();
    }

    private void CheckIfEmpty()
    {
        if (gameObject.GetComponentsInChildren<Transform>().Length == 1)
        {
            Destroy(gameObject);
        }
    }
}
