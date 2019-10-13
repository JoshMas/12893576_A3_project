using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfEmpty : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        CheckIfEmpty();
    }

    //Destroys this object if there are no children (blocks) attached
    private void CheckIfEmpty()
    {
        if (gameObject.GetComponentsInChildren<Transform>().Length == 1)
        {
            Destroy(gameObject);
        }
    }
}
