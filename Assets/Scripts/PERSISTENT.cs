using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PERSISTENT : MonoBehaviour
{
    public static PERSISTENT instance;

    // This method is called before the first frame update
    private void Start()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set this as the instance and mark it to not be destroyed on scene change
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }
}
