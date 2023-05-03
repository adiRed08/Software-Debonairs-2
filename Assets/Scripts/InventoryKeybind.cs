using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryKeybind : MonoBehaviour
{

    // private InputManager inputManager;
    public GameObject inventory;
    public bool toggle;    

    // Start is called before the first frame update
    void Start()
    {
        // inputManager = InputManager.instance;
        toggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            inventory.SetActive(!toggle);
            toggle = !toggle;
        }

        
    }
}
