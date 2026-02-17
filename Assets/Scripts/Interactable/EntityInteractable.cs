using UnityEngine;

public class EntityInteractable : MonoBehaviour, IInteractable
{
    public string entityName;
    public int ID;
    public string type;
    public string faction;
    public int priority;
    public int relationship;

    public void Interact()
    {
        UIManager.instance.OpenDialogue();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
