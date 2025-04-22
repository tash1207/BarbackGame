using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerGlass : Interactable
{
    [Header("References")]
    [SerializeField] AudioClip brokenGlassClip;
    [SerializeField] SpriteRenderer beerFill;

    [Header("Settings")]
    [SerializeField] float beerDepletionRate = 10f;
    [SerializeField] float beerAmountDeemedEmpty = 0.35f;

    void Update()
    {
        if (beerFill != null && beerFill.size.y > 0)
        {
            beerFill.size -= new Vector2(0f, beerDepletionRate / 100 * Time.deltaTime);
            beerFill.size = new Vector2(beerFill.size.x, Mathf.Clamp(beerFill.size.y, 0, 1));
        }
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Beer Glass Interacted");
        if (gameObject.tag == "Removable")
        {
            if (beerFill.size.y > beerAmountDeemedEmpty)
            {
                AlertControl.instance.ShowAlert("That beer isn't empty yet.", 2f);
                return;
            }

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
