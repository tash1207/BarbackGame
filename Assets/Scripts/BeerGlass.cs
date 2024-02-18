using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGlass : Interactable
{
    public override void Interact(GameObject bot)
    {
        base.Interact(bot);
        BotController controller = bot.GetComponent<BotController>();
        Debug.Log("Beer Glass Interacted");
        if (gameObject.tag == "Removable")
        {
            if (controller.ChangeGlassware(1))
            {
                controller.PlaySound(interactedClip);
                Destroy(gameObject);
            }
            else
            {
                controller.PlaySound(negativeClip);
            }
        }
    }
}
