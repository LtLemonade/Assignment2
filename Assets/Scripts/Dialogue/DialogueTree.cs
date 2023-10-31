using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueResponse
{
    public int nexBranchID;
    [TextArea(1, 3)]
    public string responseDialogue;
}
[System.Serializable]
public class DialogueSection
{
    public DialogueResponse[] responses;
    [TextArea(1, 10)]
    public string dialogue;
}
[System.Serializable]
public class DialogueBranch
{
    public string branchName;
    public int branchID;
    public float value;
    public DialogueSection[] sections;
    public bool endOnFinal;
}

public class DialogueTree : MonoBehaviour
{
    public DialogueBranch[] branches;
    public string NPCName;
}
