using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStation : Interactable
{
    public override void Interact(GameObject bot)
    {
        base.Interact(bot);
        BotController controller = bot.GetComponent<BotController>();
        Debug.Log("Bus Station Interacted");
        if (controller.ClearGlassware() | controller.ClearTrays())
        {
            controller.PlaySound(interactedClip);
        }
    }
}
