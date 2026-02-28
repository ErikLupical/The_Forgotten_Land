using UnityEngine;

public class ArenaInteractable : MonoBehaviour, IInteractable
{
    public string entityName;
    public int ID;
    public string type;
    public string faction;
    public int priority;

    public void Interact()
    {
        ConfirmationUI.instance.OpenConfirmation(
            "Enter the Arena?",
            "You are about to enter the arena.",
            () =>
            {
                // TODO: Enter arena
                Debug.Log("Confirmed!");
            }
            );
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
