using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStation : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Bus Station Interacted");
        if (playerController.ClearGlassware() | playerController.ClearTrays())
        {
            playerController.PlaySound(interactedClip);
        }
    }
}
