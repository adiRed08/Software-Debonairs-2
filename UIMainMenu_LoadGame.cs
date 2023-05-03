using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu_LoadGame : MonoBehaviour
{
    
    [SerializeField] Button _loadGame;
   
    void Start()
    {
        _loadGame.onClick.AddListener(LoadGame);
    }   

    private void LoadGame()
    {
        ScenesManager.Instance.LoadLoadScene();
    }
}
