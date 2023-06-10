using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BackButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] Button _backButton;

    void Start()
    {
        _backButton.onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        // GameObject myGameObject = GameObject.Find("GAMEMYDATA");
        // Destroy(myGameObject);
        ScenesManager.Instance.LoadMainMenu();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _text.color = Color.yellow;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        _text.color = Color.black;
    }
}
