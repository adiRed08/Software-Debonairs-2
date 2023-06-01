using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentReactions : MonoBehaviour
{
    public Move[] moves;
    public BattleDialogue battleDialogue;
    public HealthBarMechanics healthBar;
    public int damage;

    /*Reactions are only for the benefit of the demo and might be added later on if we decide
    to pursue this project further*/
    public void promptDisplay(string prompt)
    {
        battleDialogue.toDisplay.text = prompt;
    }

    public void react_to_move(Move playerMove, string opponentMove)
    {
        damage = 0;
        //Debug.Log(opponentMove);
        //Debug.Log(playerMove.moveName == "I'm here to do the work");
        switch (playerMove.moveName)
        {
            case "I'm here to do the work":
                //Super effective moves
                if (opponentMove == "It's interesting how much effort you put into everything. I guess some people just really try hard.")
                {
                    set_toDisplay("Ngrrgh, we'll just see about that.");
                    damage = 15;
                }
                else if (opponentMove == "Oh, you got into this school? Must have been a lucky day for you.")
                {
                    set_toDisplay("...You won't make it, just wait and see.");
                    damage = 15;
                }
                //Semi-effective
                else if (opponentMove == "I can't imagine being as unpopular as you")
                {
                    set_toDisplay("And what about your social life?");
                    damage = 10;
                }
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*")
                {
                    set_toDisplay("Wow, that must be a joke right?");
                    damage = 10;
                }
                //Not very effective
                else
                {
                    set_toDisplay("That's the best you can come up with?");
                    damage = 5;
                }
                break;

            case "Does that matter?":
                //super effective
                if (opponentMove == "I've got so many expensive things kid, I don't think you can afford even one of them.")
                {
                    set_toDisplay("S-shut up. You just wish you had these things.");
                    damage = 15;
                }
                else if (opponentMove == "I can't imagine being as unpopular as you")
                {
                    set_toDisplay("Ngrgh, why you little b-!");
                    damage = 15;
                }
                //Semi effective
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*")
                {
                    set_toDisplay("*in a sarcastic tone* HAHAHAHA good one!");
                    damage = 10;
                }
                else if (opponentMove == "I'm gonna beat you up so bad, no one's gonna recognize you.")
                {
                    set_toDisplay("Heck, I think I might enjoy this even more.");
                    damage = 10;
                }
                else if (opponentMove == "*Laughs at you*")
                {
                    set_toDisplay("Hahahahahaa forgive me, talking with you is just too funny.");
                    damage = 10;
                }
                else if (opponentMove == "*Stares at you threateningly*")
                {
                    set_toDisplay("You're really asking for it now, I swear.");
                    damage = 10;
                }
                else
                {
                    set_toDisplay("Tsk Tsk, I guess the important stuff doesn't really matter to you huh.");
                    damage = 5;
                }
                break;

            case "That won't affect me":
                //Super effective moves
                if (opponentMove == "I won't let you out of my sight" || opponentMove == "I'm gonna beat you up so bad, no one's gonna recognize you.")
                {
                    set_toDisplay("Grrr... why you little!");
                    damage = 15;
                }
                else if (opponentMove == "*Stares at you threateningly*" || opponentMove == "*Laughs at you*")
                {
                    set_toDisplay("You mock me with that statement of yours!");
                    damage = 15;
                }
                //Semi-effective
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*" || opponentMove == "Oh, you got into this school? Must have been a lucky day for you.")
                {
                    set_toDisplay("I'm pretty sure that affected you.");
                    damage = 10;
                }
                //Not very effective
                else
                {
                    set_toDisplay("That's the best you can come up with?");
                    damage = 5;
                }
                break;

            case "Ignore":
                //Super effective moves 5 1
                if (opponentMove == "I can't imagine being as unpopular as you" || opponentMove == "I've got so many expensive things kid, I don't think you can afford even one of them."
                    || opponentMove == "It's interesting how much effort you put into everything. I guess some people just really try hard.")
                {
                    set_toDisplay("H-hey! Look at me when I'm talking to you, you idiot!");
                    damage = 15;
                }
                //Semi-effective 0 7 
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*" || opponentMove == "Oh, you got into this school? Must have been a lucky day for you."
                    || opponentMove == "*Stares at you threateningly*")
                {
                    set_toDisplay("You're really getting on my nerves, kid");
                    damage = 10;
                }
                //Not very effective
                else if (opponentMove == "*Laughs at you*")
                {
                    set_toDisplay("*Continues to laugh as you say nothing.");
                    damage = 5;
                }
                else
                {
                    set_toDisplay("I guess you're just that used to not doing anything that you can't even defend yourself. *laughs at you*"); 
                    damage = 5;
                }
                break;
        }
    }

    public void minusHealth()
    {
        if (healthBar.health.value - damage >= 0)
        {
            healthBar.health.value -= damage;
        }
        else
        {
            healthBar.health.value = 0;
        }    
        healthBar.valueText.text = healthBar.health.value.ToString() + "/" + healthBar.health.maxValue.ToString();
    }

    public void set_toDisplay(string response)
    {
        battleDialogue.toDisplay.text = response;
    }
}
        //        if (playerMove.moveName == "I'm here to do the work")
        //{
        //    //Super effective moves
        //    if (opponentMove == "It's interesting how much effort you put into everything. I guess some people just really try hard.")
        //    {
        //        battleDialogue.toDisplay.text = "Ngrrgh, we'll just see about that.";
        //    }
        //    else if (opponentMove == "Oh, you got into this school? Must have been a lucky day for you.")
        //    {
        //        battleDialogue.toDisplay.text = "...You won't make it, just wait and see.";
        //    }
        //    //Semi-effective
        //    else if (opponentMove == "I can't imagine being as unpopular as you")
        //    {
        //        battleDialogue.toDisplay.text = "And what about your social life?";
        //    }
        //    else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*")
        //    {
        //        battleDialogue.toDisplay.text = "Wow, that must be a joke right?";
        //    }
        //    //Not very effective
        //    else
        //    {
        //        battleDialogue.toDisplay.text = "That's the best you can come up with?";
        //    }
        //}

          /* "say \"GG go next\"":
                if (currMove.timesCalled == 0) 
                {
                    battleDialogue.toDisplay.text = "What do you mean GG go next? There won't be a next time for you, my friend.";
                }
                else
                {
                    battleDialogue.toDisplay.text = "Didn't you hear what I just said? Are you deaf or something!?";
                }
                break;

                            case "Ignore":
                                if (currMove.timesCalled == 0)
                {
                    battleDialogue.toDisplay.text = "What? You think you can just ignore me and not get away with it?";
                    //This is supposed to make you lose health against the guy
                }
                else
                {
                    battleDialogue.toDisplay.text = "You just never learn apparently.";
                }
                break;

                            case "Suggest to talk things out":
                                if (currMove.timesCalled == 0)
                {
                    battleDialogue.toDisplay.text = "T-talk things out!? I don't think this can even be sorted out in the first place.";
                    //This is supposed to make you lose health against the guy
                }
                else
                {
                    battleDialogue.toDisplay.text = "You just never learn apparently.";
                }
                break;*/
