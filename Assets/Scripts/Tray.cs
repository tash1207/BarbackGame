using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : Interactable
{
    public override void Interact(GameObject bot)
    {
        base.Interact(bot);
        BotController controller = bot.GetComponent<BotController>();
        Debug.Log("Tray Interacted");
        if (gameObject.tag == "Removable")
        {
            if (controller.ChangeTrays(1))
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
