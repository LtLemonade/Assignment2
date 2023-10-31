using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueTree dialogueTree;
    public Button thisButton;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        thisButton.interactable = false;
        thisButton.gameObject.SetActive(false);
    }

    public void TriggerDialogueTree()
    {
        FindObjectOfType<DialogueTreeManager>().StartDialogueTree(dialogueTree);
        thisButton.interactable = false;
    }
}
