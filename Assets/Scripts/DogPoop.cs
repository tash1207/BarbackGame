using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPoop : Interactable
{
    public override void Interact(GameObject bot)
    {
        base.Interact(bot);
        BotController controller = bot.GetComponent<BotController>();
        Debug.Log("Dog Poop Interacted");
        if (gameObject.tag == "Removable")
        {
            if (controller.ChangePoop(1))
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
