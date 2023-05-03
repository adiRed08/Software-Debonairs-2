using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static DialogueItem;

public class DialogueLoader : MonoBehaviour
{
    private List<string> DiaOpt;
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private GameObject prefab;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ChoicesOn");
    }

    public void SetChoices(List<string> options)
    {
        DiaOpt = options;
    }

    public void print(string name)
    {
        Debug.Log(name);
    }

    private void OnEnable()
    {
        dialogueSub += print;
    }

    private void OnDisable()
    {
        dialogueSub -= print;
    }

    public void RefreshChoices()
    {
        foreach (Transform child in scrollViewContent)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < DiaOpt.Count; i++)
        {
            GameObject gameObject = Instantiate(prefab, scrollViewContent);
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = DiaOpt[i];
            gameObject.name = i.ToString();

        }
    }



}
