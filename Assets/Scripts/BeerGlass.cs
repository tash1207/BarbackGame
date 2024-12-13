using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGlass : Interactable
{
    public AudioClip brokenGlassClip;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Beer Glass Interacted");
        if (gameObject.tag == "Removable")
        {
            int changeInGlassware = playerController.ChangeGlassware(1);
            if (changeInGlassware > 0)
            {
                playerController.PlaySound(interactedClip);
                Destroy(gameObject);
            }
            else if (changeInGlassware < 0)
            {
                playerController.PlaySound(brokenGlassClip);
                Destroy(gameObject);
            }
            else
            {
                playerController.PlaySound(negativeClip);
            }
        }
    }
}
