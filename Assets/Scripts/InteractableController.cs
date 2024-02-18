using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public AudioClip interactedClip;
    public AudioClip negativeClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject bot)
    {
        BotController controller = bot.GetComponent<BotController>();
        Debug.Log("Interacted");
        if (gameObject.tag == "Removable")
        {
            // TODO: Check if beer or dog poop.
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
        else
        {
            if (controller.ClearGlassware())
            {
                controller.PlaySound(interactedClip);
            }            
        }
    }
}
