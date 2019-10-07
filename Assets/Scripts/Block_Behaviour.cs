using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Behaviour : MonoBehaviour
{
    private Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponentsInParent<Transform>()[1];
        Debug.Log(parentTransform.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rotate()
    {
        transform.Rotate(0, 0, -90, Space.Self);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        parentTransform.Translate(collision.gameObject.GetComponent<Border_Behaviour>().movement, Space.World);
        Debug.Log(collision.gameObject.name);
    }
}
