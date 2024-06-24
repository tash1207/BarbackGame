using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGlass : Interactable
{
    public AudioClip brokenGlassClip;

    public override void Interact(GameObject bot)
    {
        base.Interact(bot);
        BotController controller = bot.GetComponent<BotController>();
        Debug.Log("Beer Glass Interacted");
        if (gameObject.tag == "Removable")
        {
            int changeInGlassware = controller.ChangeGlassware(1);
            if (changeInGlassware > 0)
            {
                controller.PlaySound(interactedClip);
                Destroy(gameObject);
            }
            else if (changeInGlassware < 0)
            {
                controller.PlaySound(brokenGlassClip);
                Destroy(gameObject);
            }
            else
            {
                controller.PlaySound(negativeClip);
            }
        }
    }
}
