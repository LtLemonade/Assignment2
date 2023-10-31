using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameManager GameManager;

    public Queue<string> sentences;

    public GameObject elementalDialogues;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Button firstButton;
    public Button finalDialogue;

    public Animator animator;

    public  Image mc;
    public  Image currentSpeaker;
    public Image[] allSpeakers;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        string name = dialogue.name;
        nameText.text = name;

        foreach (Image go in allSpeakers)
        {
            if (go.name == name)
            {
                currentSpeaker = go;
                break;
            }
        }

        mc.gameObject.SetActive(true);
        currentSpeaker.gameObject.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
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
        sentences.Clear();
        if (!firstButton.IsInteractable())
        {
            elementalDialogues.SetActive(true);
        }
        else
        {
            firstButton.gameObject.SetActive(true);
        }
        if (elementalDialogues.gameObject.activeSelf && GameManager.CheckDoneTrees())
        {
            elementalDialogues.SetActive(false);
            finalDialogue.gameObject.SetActive(true);
        }
        if (!finalDialogue.IsInteractable())
        {
            SceneManager.LoadScene("R2");
        }
        animator.SetBool("IsOpen", false);
        mc.gameObject.SetActive(false);
        currentSpeaker.gameObject.SetActive(false);
    }
}
