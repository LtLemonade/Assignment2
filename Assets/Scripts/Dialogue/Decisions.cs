using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]
public class Decisions : ScriptableObject
{
    [SerializeField]
    public List<int> playerDecisions = new List<int>();
    [SerializeField]
    public float winningPropbaility = 0.5f;

    private void OnDisable()
    {
        playerDecisions.Clear();
    }
}
