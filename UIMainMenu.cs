using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    
    [SerializeField] Button _newGame;
   
    void Start()
    {
        _newGame.onClick.AddListener(NewGame);
    }

    private void NewGame()
    {
        ScenesManager.Instance.LoadNewGame();
    }    
}
