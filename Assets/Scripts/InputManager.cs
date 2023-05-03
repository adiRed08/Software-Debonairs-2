using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private KeyBindings keybindings;
    //this is just to make sure that the InputManager is this
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public KeyCode GetKeyForAction(KeybindingActions keybindingAction)
    {
        foreach (KeyBindings.KeybindingCheck keybindingCheck in keybindings.keybindingChecks)
        {
            if(keybindingCheck.keybindingAction == keybindingAction){
                return keybindingCheck.keycode;
            }
        }

        return KeyCode.None;
    }

    public bool GetKey(KeybindingActions key)
    {
        foreach (KeyBindings.KeybindingCheck keybindingCheck in keybindings.keybindingChecks)
        {
            if(keybindingCheck.keybindingAction == key){
                return Input.GetKey(keybindingCheck.keycode);
            }
        }

        return false;
    }
}
