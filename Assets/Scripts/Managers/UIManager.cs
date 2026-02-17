using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Player Stats")]
    public GameObject playerStats;

    [Header("Dialogue Box")]
    public GameObject dialogueBox;
    public TextMeshProUGUI SayDoButtonText;

    [Header("Confirmation Menu")]
    public GameObject Confirmation;

    [Header("Darken Background")]
    public CanvasGroup DarkenBackground;

    [Header("References")]
    private EntityBehavior playerBehavior;

    private void Awake()
    {
        instance = this;
        playerBehavior = (EntityBehavior) GameObject.Find("Player").GetComponent("EntityBehavior");
        playerStats.SetActive(EnemyManager.instance.InCombat);
    }

    public void OpenDialogue()
    {
        playerStats.SetActive(false);
        dialogueBox.SetActive(true);

        playerBehavior.actionState = EntityBehavior.ActionState.Interacting;
        playerBehavior.rb.linearVelocity = Vector2.zero;
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        playerStats.SetActive(true);

        playerBehavior.actionState = EntityBehavior.ActionState.Idle;
    }

    public void ToggleSayDo()
    {
        if (SayDoButtonText.text == "Say") SayDoButtonText.text = "Do";
        else if (SayDoButtonText.text == "Do") SayDoButtonText.text = "Say";
    }
}
