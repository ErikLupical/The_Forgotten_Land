using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference interact;
    public InputActionReference normal;

    public bool inCombat;
    public EntityBehavior entityBehavior;

    private List<IInteractable> nearbyInteractables = new List<IInteractable>();

    private void OnEnable()
    {
        interact.action.started += OnInteractCallback;
        normal.action.started += OnAttackCallback;
    }

    private void OnDisable()
    {
        interact.action.started -= OnInteractCallback;
        normal.action.started -= OnAttackCallback;
    }

    private void OnInteractCallback(InputAction.CallbackContext context)
    {
        OnInteract();
    }
    private void OnAttackCallback(InputAction.CallbackContext context)
    {
        OnAttack();
    }

    public void OnInteract()
    {
        if (nearbyInteractables.Count == 0) return;

        IInteractable closest = null;
        float minDistance = Mathf.Infinity;

        if (!inCombat)
        {
            foreach (IInteractable interactable in nearbyInteractables)
            {
                float distance = Vector2.Distance(
                    transform.position,
                    interactable.GetTransform().position
                );

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = interactable;
                }
            }

            closest?.Interact();
        }
    }

    public void OnAttack()
    {
        if (entityBehavior.CanAttack() && inCombat)
        {
            StartCoroutine(entityBehavior.Attack());
        }
    }

    void FixedUpdate()
    {
        inCombat = EnemyManager.instance != null && EnemyManager.instance.InCombat;

        if (entityBehavior.actionState == EntityBehavior.ActionState.Idle) Move();
        else if (entityBehavior.actionState == EntityBehavior.ActionState.Attacking)
            entityBehavior.rb.linearVelocity = Vector2.zero;
    }

    private void Move()
    {
        Vector2 direction = move.action.ReadValue<Vector2>();

        if (direction.x > 0 && transform.localScale.x < 0 ||
            direction.x < 0 && transform.localScale.x > 0)
        {
            entityBehavior.Flip();
        }

        entityBehavior.UpdateAnimation(Mathf.Abs(direction.x), Mathf.Abs(direction.y));

        if (GetComponent<EntityHealth>().isAlive)
            entityBehavior.rb.linearVelocity = new Vector2(direction.x, direction.y) * entityBehavior.speed;
        else
            entityBehavior.rb.linearVelocity = new Vector2(0, 0);
    }

    public void AddInteractable(IInteractable interactable)
    {
        if (!nearbyInteractables.Contains(interactable))
            nearbyInteractables.Add(interactable);
    }

    public void RemoveInteractable(IInteractable interactable)
    {
        nearbyInteractables.Remove(interactable);
    }
}
