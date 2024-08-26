using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Threading;

public class InputFieldManager : SINGLETON<InputFieldManager>
{
    [SerializeField] private GameObject inputFieldPanel;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI backText;
    [SerializeField] private PlayerAction player;
    [SerializeField] private PlayerMovement playerMovement;

    private bool checking = false;
    private string text;
    private PlayerActionEnum playerType;
    private EnemyEnum enemyType;
    private float timer;

    private bool is_typing = false;
    public bool Is_typing { get { return is_typing; } set { is_typing = value; } }

    private void Awake()
    {
        inputFieldPanel.SetActive(false);
        uiCanvas.SetActive(true);
    }

    private void Update()
    {
        inputField.placeholder.enabled = true;
    }

    public void Input(string _text, PlayerActionEnum _playerType, EnemyEnum _enemyType, float _timer)
    {
        checking = false;
        inputField.text = string.Empty;
        inputField.characterLimit = _text.Length;
        inputFieldPanel.SetActive(true);
        uiCanvas.SetActive(false);

        text = _text;
        playerType = _playerType;
        enemyType = _enemyType;
        timer = GameManager.Instance.TimeNormalize(_timer);

        backText.text = text;    

        if (inputField.isFocused == false)      
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        Invoke("Check", timer);    

        is_typing = true;
    }

    public void Effect()     
    {
        inputField.transform.DOScale(1.1f, GameManager.Instance.TimeNormalize(0.25f))
            .OnComplete(() => { inputField.transform.DOScale(1f, GameManager.Instance.TimeNormalize(0.25f)); });
    }

    public void Check() 
    {
        if (checking == false)
        {
            if (inputField.text == text)
            {
                player.action(playerType);
                EnemyManager.Instance.EnemyDamage(enemyType);
            }
            else
            {
                EnemyManager.Instance.EnemyAttack(enemyType); 
            }

            checking = true;
            CameraAction.Instance.Shake();
        }

        GameManager.Instance.TimeReset();
        is_typing = false;
        inputField.text = string.Empty;
        inputFieldPanel.SetActive(false);
        uiCanvas.SetActive(true);

        if (playerMovement != null)
        {
            playerMovement.Is_typing = false;
        }
    }
}
