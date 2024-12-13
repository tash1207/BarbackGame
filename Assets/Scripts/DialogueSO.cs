using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue Text", menuName = "New Dialogue Text")]
public class DialogueSO : ScriptableObject {

    [TextArea(2, 6)]
    [SerializeField] string dialogueText;
    [SerializeField] string dialogueSpeaker;
    [SerializeField] Sprite dialogueSpeakerSprite;
    [SerializeField] string buttonText = "CONTINUE";

    public string GetDialogueText()
    {
        return dialogueText;
    }

    public string GetSpeakerName()
    {
        return dialogueSpeaker;
    }

    public Sprite GetSpeakerSprite()
    {
        return dialogueSpeakerSprite;
    }

    public string GetButtonText()
    {
        return buttonText;
    }
}
