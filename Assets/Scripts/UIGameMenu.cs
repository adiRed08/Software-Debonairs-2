using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : MonoBehaviour
{
    [SerializeField] private Button _mainMenu;
   
    void Start()
    {
        _mainMenu.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        ScenesManager.Instance.LoadMainMenu();
    } 
}
