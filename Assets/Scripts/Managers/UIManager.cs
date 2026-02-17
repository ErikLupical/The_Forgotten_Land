using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject playerStats;
    public GameObject dialogueBox;
    private EntityBehavior playerBehavior;

    private void Awake()
    {
        instance = this;
        playerBehavior = (EntityBehavior) GameObject.Find("Player").GetComponent("EntityBehavior");
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
}
