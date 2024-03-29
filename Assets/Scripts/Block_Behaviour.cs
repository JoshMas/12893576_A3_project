﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Behaviour : MonoBehaviour
{
    private Tetromino_Behaviour parentScript;
    private Animator animator;
    private bool isTheBoss = false;

    [SerializeField]
    private AudioClip[] impactClips = new AudioClip[2];
    private AudioSource impactAudio;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<Tetromino_Behaviour>();
        animator = GetComponent<Animator>();
        impactAudio = gameObject.GetComponent<AudioSource>();
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

    //Returns true if the block is the boss
    public bool IsTheBoss()
    {
        return isTheBoss;
    }

    //Turns the block into the boss
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

    //Does the exact same thing as the OnCollisionEnter function
    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    //Plays the line clear animation, then destroys this object
    public void SetClearTrigger()
    {
        animator.SetTrigger("IsCleared");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    //Plays the game over animation, then destroys this object
    public void SetGameOverTrigger()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        animator.SetTrigger("GameOver");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    //Plays a random impact noise
    private void PlayImpactNoise()
    {
        if (!impactAudio.isPlaying)
        {
            impactAudio.clip = impactClips[Random.Range(0, impactClips.Length)];
            impactAudio.Play();
        }
    }
}
