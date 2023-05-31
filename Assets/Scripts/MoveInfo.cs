using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveInfo : MonoBehaviour
{
    public Move move;
    public BattleDialogue battleDialogue;
    private TMP_Text moveName;
    //private string dialogue;

    void Start()
    {
        moveName = this.GetComponentInChildren<TMP_Text>();
        moveName.text = move.moveName;
        moveName.fontSize = 30;
    }

    public void callPlayerDialogue()
    {
        battleDialogue.optionDialogue(move);
    }

    public void callOpponentDialogue()
    {
        battleDialogue.responseDialogue(move);
    }
}
