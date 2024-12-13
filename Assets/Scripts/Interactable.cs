using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AudioClip interactedClip;
    public AudioClip negativeClip;

    protected PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted");
    }
}
