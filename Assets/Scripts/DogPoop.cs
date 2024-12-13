using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPoop : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Dog Poop Interacted");
        if (gameObject.tag == "Removable")
        {
            if (playerController.ChangePoop(1))
            {
                playerController.PlaySound(interactedClip);
                Destroy(gameObject);
            }
            else
            {
                playerController.PlaySound(negativeClip);
            }
        }
    }
}
