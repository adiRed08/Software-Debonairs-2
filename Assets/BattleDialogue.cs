using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleDialogue : MonoBehaviour
{
    public Opponent opponent;
    public Move[] moves;
    private string dialogue;
    public OpponentReactions reactions;
    public bool isGameDialogue;
    private static int lastPrompt;
    private int prompt;

    public TMP_Text toDisplay;

   void Start()
    {
        if (isGameDialogue)
        {
            lastPrompt = -1;
            foreach (Move move in moves)
            {
                move.timesCalled = 0;
            }
            dialogue = opponent.name + " has started a confrontation.";
            toDisplay.text = dialogue;
        }
    }

    public void randomPrompt()
    {
        while (true)
        {
            prompt = Random.Range(0, opponent.statements.Length);
            if (prompt != lastPrompt)
            {
                reactions.promptDisplay(opponent.statements[prompt]);
                lastPrompt = prompt;
                //Debug.Log(lastPrompt);
                break;

            }
        }
    }

    public void optionDialogue(Move move)
    {
        //dialogue = move.dialogue
        string[] dialogueList = { move.dialogue1, move.dialogue2 };

        toDisplay.text = dialogueList[Random.Range(0, 2)];
    }

    public void responseDialogue(Move move)
    {
        //Debug.Log("This");
        //Debug.Log(lastPrompt);
        reactions.react_to_move(move, opponent.statements[lastPrompt]);
        move.timesCalled++;
    }

    public void ClickButtonText()
    {
        toDisplay.text = "Click to continue";
    }
}
