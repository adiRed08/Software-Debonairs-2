using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleDialogue : MonoBehaviour
{
    public string enemyName;
    private string dialogue;

    public TMP_Text toDisplay;

   void Start()
    {
        dialogue = enemyName + " has started a confrontation.";
        toDisplay.text = dialogue;
    }

    public void optionDialogue(Move move)
    {
        //dialogue = move.dialogue;
        toDisplay.text = move.dialogue;
    }
}
