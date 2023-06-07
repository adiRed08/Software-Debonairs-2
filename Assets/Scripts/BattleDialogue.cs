using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleDialogue : MonoBehaviour
{
    public Item[] possibleItems;
    public Opponent opponent;
    public Move[] moves;
    private string dialogue;
    public OpponentReactions reactions;
    public bool isGameDialogue;
    public bool isEndDialogue;
    public bool isOpponentDialogue;
    private static int lastPrompt;
    private int prompt;
    public Sprite femaleStart;

    public TMP_Text toDisplay;

   void Start()
    {
        foreach (Item item in possibleItems)
        {
            if(item.stackable == false)
            {
                Debug.Log("Item Name: " + item.name);
                Debug.Log("Is Equipped: " + item.isEquipped);
            }
        }

        if (isGameDialogue)
        {
            opponent.image = femaleStart;
            lastPrompt = -1;
            foreach (Move move in moves)
            {
                move.timesCalled = 0;
            }
            dialogue = opponent.name + " has started a confrontation.";
            toDisplay.text = dialogue;
        }
        //else if (isEndDialogue)
        //{
        //    this.gameObject.SetActive(true);
        //}
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
        toDisplay.text = opponent.name + " took " + reactions.damage + " damage";
    }

    public void Update()
    {
        if (isEndDialogue && reactions.healthBar.health.value == 0) {
            toDisplay.text = opponent.name + " has been defeated.";
        }
        else if (isOpponentDialogue && reactions.healthBar.health.value == 0)
        {
            toDisplay.text = opponent.endRemark;
        }
        else if (isGameDialogue && reactions.healthBar.health.value == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}