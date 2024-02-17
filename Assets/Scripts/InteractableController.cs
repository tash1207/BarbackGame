using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    // public GameObject gameObject;
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
        // We set the gameObject on the beer so it can be destroyed.
        if (gameObject != null && gameObject.tag == "Removable")
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
        // We don't set the gameObject on the drainCover/table so it isn't destroyed.
        // TODO: Change interactable behavior based on the object rather than the gameObject field.
        else
        {
            if (controller.ClearGlassware())
            {
                controller.PlaySound(interactedClip);
            }            
        }
    }
}
