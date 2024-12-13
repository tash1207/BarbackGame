using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Interaction", menuName = "New Dialogue Interaction")]
public class DialogueInteractionSO : ScriptableObject {
    [SerializeField] List<DialogueSO> dialogueTexts;
    int currentDialogueTextIndex = 0;

    public DialogueSO GetCurrentDialogueSO()
    {
        return dialogueTexts[currentDialogueTextIndex];
    }
    
    public bool AdvanceDialogue()
    {
        if (currentDialogueTextIndex < dialogueTexts.Count - 1)
        {
            currentDialogueTextIndex++;
            return true;
        }
        else
        {
            currentDialogueTextIndex = 0;
            return false;
        }
    }

}
