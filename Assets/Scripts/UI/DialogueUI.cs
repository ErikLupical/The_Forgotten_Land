using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance;

    [Header("Dialogue Box")]
    public CanvasGroup playerAvatar;
    public CanvasGroup healthBar;

    [Header("Dialogue Box")]
    public GameObject dialogueBox;
    private EntityBehavior playerBehavior;

    [Header("Output")]
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI outputText;

    [Header("Input")]
    public TextMeshProUGUI SayDoButtonText;

    private void Awake()
    {
        instance = this;
        playerBehavior = (EntityBehavior)GameObject.Find("Player").GetComponent("EntityBehavior");
    }

    public void OpenDialogue()
    {
        dialogueBox.SetActive(true);

        playerBehavior.actionState = EntityBehavior.ActionState.Interacting;
        playerBehavior.rb.linearVelocity = Vector2.zero;
        playerBehavior.UpdateAnimation(0, 0);

        playerAvatar.alpha = 0f;
        playerAvatar.interactable = false;
        playerAvatar.blocksRaycasts = false;

        healthBar.alpha = 0f;
        healthBar.interactable = false;
        healthBar.blocksRaycasts = false;
    }

    public void CloseDialogue()
    {
        ConfirmationUI.instance.OpenConfirmation(
            "Close Dialogue?",
            "Are you sure you want close dialogue? The current event will not be recorded in history.",
            () =>
            {
                Debug.Log("Confirmed!");

                dialogueBox.SetActive(false);
                playerBehavior.actionState = EntityBehavior.ActionState.Idle;

                playerAvatar.alpha = 1f;
                playerAvatar.interactable = true;
                playerAvatar.blocksRaycasts = true;

                healthBar.alpha = 1f;
                healthBar.interactable = true;
                healthBar.blocksRaycasts = true;
            }
        );
    }

    // TODO
    public void Send()
    {

    }

    public void ToggleSayDo()
    {
        if (SayDoButtonText.text == "Say") SayDoButtonText.text = "Do";
        else if (SayDoButtonText.text == "Do") SayDoButtonText.text = "Say";
    }
}
