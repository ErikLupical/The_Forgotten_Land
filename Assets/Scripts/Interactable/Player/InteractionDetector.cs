using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            player.AddInteractable(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            player.RemoveInteractable(interactable);
        }
    }
}
