using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public Transform enemy;
    public Transform player;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, player);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemy);
        enemyUnit = enemyGO.GetComponent<Unit>();
        dialogueText.text = enemyUnit.unitName + " has challenged you to a battle.";

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);
        Debug.Log("yes");
    }
}
