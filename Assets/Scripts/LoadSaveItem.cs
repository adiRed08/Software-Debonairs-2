using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static PlayerDB;
using System;
using static GameSaveLoader;
using TMPro;
using static SaveFile;

public class LoadSaveItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public GameObject moreOptions;
    [SerializeField]
    public TMP_InputField editTextBox;

    public delegate void clickSaveItem(string msg);
    public static event clickSaveItem clickSaveItemAnnounce;

    public TextMeshProUGUI ChapterNo;
    public TextMeshProUGUI SceneNo;



    private void Start()
    {
        //TextMeshProUGUI[] texts = this.GetComponentsInChildren<TextMeshProUGUI>();
        //editTextBox.text = texts[1].text;
        try
        {
            ChapterNo.text = getChapterID(long.Parse(name)).ToString();
            SceneNo.text = getSceneID(long.Parse(name)).ToString();
        }
        catch
        {

        }

    }


    public void hideMenu(string xx)
    {
        if(this.name != xx)
        {
            if (moreOptions.activeInHierarchy == true)
            {
                Image image = GetComponents<Image>()[0];
                image.color = HexToColor("8C8C8C");
                moreOptions.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        LoadSaveItem.clickSaveItemAnnounce += hideMenu;
    }

    private void OnDestroy()
    {
        LoadSaveItem.clickSaveItemAnnounce -= hideMenu;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (moreOptions.activeInHierarchy == true)
        {
            clickSaveItemAnnounce("0");
        }
        else
        {
            Image image = GetComponents<Image>()[0];
            image.color = HexToColor("FFFFFF");
            moreOptions.SetActive(true);
            try
            {
                clickSaveItemAnnounce(this.name);
            }
           catch
            {
                clickSaveItemAnnounce("0");
            }
        }
    }

    public void DeleteSave()
    {
        deleteUser(Int64.Parse(this.name));
        Delete(this.name);
        Destroy(this.GetComponentInParent<Transform>().Find(this.name));
        foreach(Transform child in transform.parent)
        {
            if(child.name == this.name)
            {
                Debug.Log(child.name + " and " + this.name);
                Destroy(child.gameObject);
            }
        }
    }

    public void EditSave(string name)
    {
        if(name != "")
        {
            editName(long.Parse(this.name), name);
            this.GetComponentsInChildren<TextMeshProUGUI>()[1].text = name;
        }
    }

    private Color HexToColor(string hex)
    {
        Color color = Color.white;
        if (!string.IsNullOrEmpty(hex))
        {
            if (hex[0] == '#')
            {
                hex = hex.Substring(1);
            }
            if (hex.Length == 6)
            {
                byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                color = new Color32(r, g, b, 255);
            }
            else
            {
                Debug.LogWarning("Invalid hexadecimal color value: " + hex);
            }
        }
        return color;
    }
}
