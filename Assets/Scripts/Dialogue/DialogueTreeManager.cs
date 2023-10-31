using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueTreeManager : MonoBehaviour
{
    private Queue<string> sentences;

    public GameObject elementalButtons;

    public TMP_Text winningPer;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public TMP_Text[] buttonTexts;

    public Button[] buttons;
    private Button contButton;

    public Image[] allSpeakers;

    public Animator animator;

    private int currentBranchId = 0;

    public DialogueManager dm;

    public Decisions decisions;

    // Start is called before the first frame update
    void Start()
    {
        decisions.winningPropbaility = 0.5f;
        decisions.playerDecisions.Clear();
        sentences = new Queue<string>();
        contButton = GameObject.Find("ContinueButton").GetComponent<Button>();
    }

    public void StartDialogueTree(DialogueTree dialogue)
    {
        currentBranchId = 0;
        elementalButtons.SetActive(false);
        animator.SetBool("IsOpen", true);
        string name = dialogue.NPCName;
        nameText.text = name;

        foreach (Image go in allSpeakers)
        {
            if (go.name == name)
            {
                dm.currentSpeaker = go;
                break;
            }
        }

        dm.mc.gameObject.SetActive(true);
        dm.currentSpeaker.gameObject.SetActive(true);

        contButton.gameObject.SetActive(false);
        DialogueLoop(dialogue);
    }

    public void DialogueLoop(DialogueTree dialogue)
    {
        decisions.playerDecisions.Add(currentBranchId);
        decisions.winningPropbaility += Array.Find(dialogue.branches, item => item.branchID == currentBranchId).value;
        winningPer.text = "Chances of Winning: " + (decisions.winningPropbaility * 100.0f) + "%";
        foreach (DialogueSection section in Array.Find(dialogue.branches, item => item.branchID == currentBranchId).sections) 
        {
            //Debug.Log(currentBranchId);
            dm.sentences.Enqueue(section.dialogue);
            DisplayNextSentence();
            if (section.responses.Length > 0)
            {
                for (int i = 0; i < section.responses.Length; i++)
                {
                    Debug.Log(i);
                    buttons[i].gameObject.SetActive(true);
                    buttonTexts[i].text = section.responses[i].responseDialogue;
                    int temp = i;
                    buttons[i].onClick.RemoveAllListeners();
                    buttons[i].onClick.AddListener(() => SetBranchID(section.responses[temp].nexBranchID, dialogue)) ;
                }
            }
            if (section.responses.Length == 0 && Array.Find(dialogue.branches, item => item.branchID == currentBranchId).endOnFinal)
            {
                contButton.gameObject.SetActive(true);
            }
        }      
    }

    public void SetBranchID(int ID,  DialogueTree dialogue)
    {
        currentBranchId = ID;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        DialogueLoop(dialogue);
    }

    public void DisplayNextSentence()
    {
        if (dm.sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dm.sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        elementalButtons.SetActive(true);
        animator.SetBool("IsOpen", false);

        dm.mc.gameObject.SetActive(false);
        dm.currentSpeaker.gameObject.SetActive(false);
    }   

}
