using UnityEngine;

public class EntityInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        UIManager.instance.OpenDialogue();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
