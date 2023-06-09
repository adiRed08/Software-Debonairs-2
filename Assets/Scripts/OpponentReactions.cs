using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpponentReactions : MonoBehaviour
{

    //public GameObject move1;
    public TMP_Text dmgAnimation;
    public Move[] moves;
    public BattleDialogue battleDialogue;
    public HealthBarMechanics enemyHealthBar;
    public HealthBarMechanics playerHealthBar;
    public int damage;
    public AudioQueue audio;
    public Sprite hp25;
    public Sprite hp50;
    public Sprite hp75;
    public Sprite hp100;
    public Sprite fhp25;
    public Sprite fhp50;
    public Sprite fhp75;
    public Sprite fhp100;
    public Image image;
    private GAMEMYDATA saveHolder;

    /*Reactions are only for the benefit of the demo and might be added later on if we decide
    to pursue this project further*/

    void Start()
    {
        try
        {
            //Try to find GAMEDATA
            GameObject _myGameObject = GameObject.Find("GAMEMYDATA");
            saveHolder = (GAMEMYDATA)_myGameObject.GetComponent(typeof(GAMEMYDATA));
            bool _genderMale = saveHolder.mySave.male;
            Debug.Log(_genderMale);
            if (_genderMale == false)
            {
                battleDialogue.opponent.image = fhp100;
                hp25 = fhp25;
                hp50 = fhp50;
                hp75 = fhp75;
                hp100 = fhp100;
            }
            Debug.Log("Success");

    }
        catch
        {

        }
        this.image.sprite = hp100;
    }
    public void promptDisplay(string prompt)
    {
        battleDialogue.toDisplay.text = prompt;
    }

    public void react_to_move(Move playerMove, string opponentMove)
    { 
        //Debug.Log(opponentMove);
        //Debug.Log(playerMove.moveName == "I'm here to do the work");
        switch (playerMove.moveName)
        {
            case "I'm here to do the work":
                //Super effective moves
                if (opponentMove == "It's interesting how much effort you put into everything. I guess some people just really try hard.")
                {
                    set_toDisplay("Ngrrgh, we'll just see about that.");
                    damage = 30;
                    audio.playAudioClip();
                }
                else if (opponentMove == "Oh, you got into this school? Must have been a lucky day for you.")
                {
                    set_toDisplay("...You won't make it, just wait and see.");
                    damage = 30;
                    audio.playAudioClip();
                }
                //Semi-effective
                else if (opponentMove == "I can't imagine being as unpopular as you")
                {
                    set_toDisplay("And what about your social life?");
                    damage = 20;
                }
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*")
                {
                    set_toDisplay("Wow, that must be a joke right?");
                    damage = 20;
                }
                //Not very effective
                else
                {
                    set_toDisplay("That's the best you can come up with?");
                    damage = 10;
                }
                break;

            case "Does that matter?":
                //super effective
                if (opponentMove == "I've got so many expensive things kid, I don't think you can afford even one of them.")
                {
                    set_toDisplay("S-shut up. You just wish you had these things.");
                    damage = 30;
                    audio.playAudioClip();
                }
                else if (opponentMove == "I can't imagine being as unpopular as you")
                {
                    set_toDisplay("Ngrgh, why you little b-!");
                    damage = 30;
                    audio.playAudioClip();
                }
                //Semi effective
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*")
                {
                    set_toDisplay("*in a sarcastic tone* HAHAHAHA good one!");
                    damage = 20;

                }
                else if (opponentMove == "I'm gonna beat you up so bad, no one's gonna recognize you.")
                {
                    set_toDisplay("Heck, I think I might enjoy this even more.");
                    damage = 20;
                }
                else if (opponentMove == "*Laughs at you*")
                {
                    set_toDisplay("Hahahahahaa forgive me, talking with you is just too funny.");
                    damage = 20;
                }
                else if (opponentMove == "*Stares at you threateningly*")
                {
                    set_toDisplay("You're really asking for it now, I swear.");
                    damage = 20;
                }
                else
                {
                    set_toDisplay("Tsk Tsk, I guess the important stuff doesn't really matter to you huh.");
                    damage = 10;
                }
                break;

            case "That won't affect me":
                //Super effective moves
                if (opponentMove == "I won't let you out of my sight" || opponentMove == "I'm gonna beat you up so bad, no one's gonna recognize you.")
                {
                    set_toDisplay("Grrr... why you little!");
                    damage = 30;
                    audio.playAudioClip();
                }
                else if (opponentMove == "*Stares at you threateningly*" || opponentMove == "*Laughs at you*")
                {
                    set_toDisplay("You mock me with that statement of yours!");
                    damage = 30;
                    audio.playAudioClip();
                }
                //Semi-effective
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*" || opponentMove == "Oh, you got into this school? Must have been a lucky day for you.")
                {
                    set_toDisplay("I'm pretty sure that affected you.");
                    damage = 20;
                }
                //Not very effective
                else
                {
                    set_toDisplay("That's the best you can come up with?");
                    damage = 10;
                }
                break;

            case "Ignore":
                //Super effective moves 5 1
                if (opponentMove == "I can't imagine being as unpopular as you" || opponentMove == "I've got so many expensive things kid, I don't think you can afford even one of them."
                    || opponentMove == "It's interesting how much effort you put into everything. I guess some people just really try hard.")
                {
                    set_toDisplay("H-hey! Look at me when I'm talking to you, you idiot!");
                    damage = 30;
                    audio.playAudioClip();
                }
                //Semi-effective 0 7 
                else if (opponentMove == "Hey, you wanna hear a joke, buddy? You! *laughs*" || opponentMove == "Oh, you got into this school? Must have been a lucky day for you."
                    || opponentMove == "*Stares at you threateningly*")
                {
                    set_toDisplay("You're really getting on my nerves, kid");
                    damage = 20;
                }
                //Not very effective
                else if (opponentMove == "*Laughs at you*")
                {
                    set_toDisplay("*Continues to laugh as you say nothing.");
                    damage = 10;
                }
                else
                {
                    set_toDisplay("I guess you're just that used to not doing anything that you can't even defend yourself. *laughs at you*"); 
                    damage = 10;
                }
                break;
        }
    }

    public void minusHealth(HealthBarMechanics healthBar)
    {
        if (healthBar.health.value - damage >= 0)
        {
            healthBar.health.value -= damage;
            Debug.Log(healthBar.health.value);
        }
        else
        {
            healthBar.health.value = 0;
        }
        healthBar.valueText.text = healthBar.health.value.ToString() + "/" + healthBar.health.maxValue.ToString();
        changeFace();
    }

    //public void move1damage()
    //{
    //    move1.SetActive(true);
    //    GameObject dmgPopup = move1.transform.GetChild(0).gameObject;
    //    TMP_Text dmgText = dmgPopup.GetComponent<TMP_Text>();
    //    dmgText.text = damage.ToString();
    //    StartCoroutine(DisableGameObject(move1));
        
    //}

    public void MoveDamage(GameObject moveObject){
        try
        {
            moveObject.SetActive(true);
            GameObject dmgPopup = moveObject.transform.GetChild(0).gameObject;
            TMP_Text dmgText = dmgPopup.GetComponent<TMP_Text>();
            dmgText.text = '-' + damage.ToString();
            StartCoroutine(DisableGameObject(moveObject));
        }
        catch { }
    }

    private IEnumerator DisableGameObject(GameObject move)
    {
        yield return new WaitForSeconds(2f);
        move.SetActive(false);
    }

    public void set_toDisplay(string response)
    {
        battleDialogue.toDisplay.text = response;
    }
    public void changeFace()
    {
        Debug.Log(enemyHealthBar.health.value);
        if (enemyHealthBar.health.value <= 75 && enemyHealthBar.health.value > 50){
            battleDialogue.opponent.image = hp75;
            image.sprite = hp75;
        }
        else if (enemyHealthBar.health.value <= 50 && enemyHealthBar.health.value > 25)
        {
            battleDialogue.opponent.image = hp50;
            image.sprite = hp50;
        }
        else if (enemyHealthBar.health.value <= 25)
        {
            battleDialogue.opponent.image = hp25;
            image.sprite = hp25;
        }
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
