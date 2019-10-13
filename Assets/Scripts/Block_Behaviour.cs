using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Behaviour : MonoBehaviour
{
    private Tetromino_Behaviour parentScript;
    private Animator animator;
    private bool isTheBoss = false;

    [SerializeField]
    private AudioClip[] impactClips;
    private AudioSource impactAudio;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<Tetromino_Behaviour>();
        animator = GetComponent<Animator>();
        impactAudio = gameObject.GetComponent<AudioSource>();
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

    public bool IsTheBoss()
    {
        return isTheBoss;
    }

    public void IAmTheBoss()
    {
        isTheBoss = true;
    }

    //Detects collisions with other blocks and the game border, sending a vector back to the parent in order to move it back out
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayImpactNoise();
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

    public void SetClearTrigger()
    {
        animator.SetTrigger("IsCleared");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    public void SetGameOverTrigger()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        animator.SetTrigger("GameOver");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void PlayImpactNoise()
    {
        if (!impactAudio.isPlaying)
        {
            impactAudio.clip = impactClips[Random.Range(0, impactClips.Length)];
            impactAudio.Play();
        }
    }
}
