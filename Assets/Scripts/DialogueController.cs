using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] GameObject dialogueDisplay;
    [SerializeField] TextMeshProUGUI dialogueTextUI;
    [SerializeField] TextMeshProUGUI dialogueSpeakerUI;
    [SerializeField] Image dialogueSpeakerImage;
    [SerializeField] TextMeshProUGUI dialogButtonText;

    [SerializeField] GameObject pauseButton;

    [Header("Dialogue Interactions")]
    [SerializeField] DialogueInteractionSO introDialogueInteraction;

    DialogueInteractionSO currentDialogueInteraction;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void DisplayIntroDialogInteraction()
    {
        currentDialogueInteraction = introDialogueInteraction;
        DisplayCurrentDialogue();
    }

    void DisplayCurrentDialogue()
    {
        DialogueSO dialogueSO = currentDialogueInteraction.GetCurrentDialogueSO();
        dialogueTextUI.text = dialogueSO.GetDialogueText();
        dialogueSpeakerUI.text = dialogueSO.GetSpeakerName();
        dialogueSpeakerImage.sprite = dialogueSO.GetSpeakerSprite();
        dialogButtonText.text = dialogueSO.GetButtonText();

        dialogueDisplay.SetActive(true);
        pauseButton.SetActive(false);
    }

    // TODO: Consider adding a skip dialogue button so users don't have to go through the
    // whole intro every time they start a new game.

    public void AdvanceDialogue()
    {
        if (currentDialogueInteraction.AdvanceDialogue())
        {
            DisplayCurrentDialogue();
        }
        else
        {
            dialogueDisplay.SetActive(false);
            pauseButton.SetActive(true);

            // TODO: Right now we assume we want to start the game when the dialogue is done
            // because our only dialogue is the intro dialogue. Allow an action to be passed
            // in so we can control this behavior.
            gameManager.StartGame();
        }
    }
}
