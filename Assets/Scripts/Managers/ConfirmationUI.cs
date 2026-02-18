using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationUI : MonoBehaviour
{
    public static ConfirmationUI instance;

    public GameObject confirmationMenu;
    public TextMeshProUGUI title;
    public TextMeshProUGUI message;
    public Button confirm;
    public Button cancel;
    public CanvasGroup darkenBackground;

    private Action onConfirmAction;
    private Action onCancelAction;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        confirmationMenu.SetActive(false);
    }

    public void OpenConfirmation(string titleText, string messageText, Action onConfirm, Action onCancel = null)
    {
        confirmationMenu.SetActive(true);
        darkenBackground.alpha = 1;
        darkenBackground.blocksRaycasts = true;

        title.text = titleText;
        message.text = messageText;

        onConfirmAction = onConfirm;
        onCancelAction = onCancel;

        confirm.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();

        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
    }

    private void Confirm()
    {
        onConfirmAction?.Invoke();
        Close();
    }

    private void Cancel()
    {
        onCancelAction?.Invoke();
        Close();
    }

    private void Close()
    {
        confirmationMenu.SetActive(false);
        darkenBackground.alpha = 0;
        darkenBackground.blocksRaycasts = false;
    }
}
