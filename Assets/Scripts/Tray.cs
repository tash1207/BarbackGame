using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Tray Interacted");
        if (gameObject.tag == "Removable")
        {
            if (playerController.ChangeTrays(1))
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
