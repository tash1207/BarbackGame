using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Trash Can Interacted");
        if (playerController.ClearPoop())
        {
            playerController.PlaySound(interactedClip);
        }
    }
}
