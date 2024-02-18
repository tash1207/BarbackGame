using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Interactable
{
    public override void Interact(GameObject bot)
    {
        base.Interact(bot);
        BotController controller = bot.GetComponent<BotController>();
        Debug.Log("Trash Can Interacted");
        if (controller.ClearPoop())
        {
            controller.PlaySound(interactedClip);
        }
    }
}
