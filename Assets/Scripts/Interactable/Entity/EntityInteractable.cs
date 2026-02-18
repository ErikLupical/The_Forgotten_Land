using System.Collections.Generic;
using UnityEngine;

public class EntityInteractable : MonoBehaviour, IInteractable
{
    public string entityName;
    public int ID;
    public string type;
    public string faction;
    public int priority;
    public Dictionary<int, int> relationship;

    public void Interact()
    {
        DialogueUI.instance.OpenDialogue();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
