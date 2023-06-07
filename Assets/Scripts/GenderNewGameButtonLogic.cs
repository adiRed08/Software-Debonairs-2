using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenderNewGameButtonLogic : MonoBehaviour
{
    public GameObject FemaleButtonImg;
    public GameObject MaleButtonImg;
    public GameObject TheSaveMan;
    public GameObject SAVEPERSISTENCY;
    public Image prof;
    public Image rival;
    public Image senpai;
    public Image friend;
    public TMP_InputField inputName;
    public bool MALE;
    SaveFile mmm;
    GAMEMYDATA xxx;
    Image x, y;
    public Button button;
    public bool pess;



    private void Start()
    {
        x = FemaleButtonImg.GetComponent<Image>();
        y = MaleButtonImg.GetComponent<Image>();

        Color grayColor = Color.grey;
        x.color = grayColor;
        y.color = grayColor;
        pess = false;
        mmm = TheSaveMan.GetComponent<SaveFile>();
        xxx = SAVEPERSISTENCY.GetComponent<GAMEMYDATA>();
        button.interactable = false;
    }

    public void clickFemale()
    {
        pess = true;
        x.color = Color.white;
        y.color = Color.gray;
        MALE = false;
        mmm.isMale = false;
        xxx.mySave.male = false;
        if(inputName.text != "")
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;

        }
    }

    public void refreshstring()
    {
        if(pess && inputName.text != "")
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;

        }
    }

    public void clickMale()
    {
        pess = true;

        y.color = Color.white;
        x.color = Color.gray;
        MALE = true;
        mmm.isMale = true;
        xxx.mySave.male = true;
        if (inputName.text != "")
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;

        }

    }

    private Image returnImage(GameObject characterObj)
    {
        return characterObj.GetComponent<Image>();
    }
}
