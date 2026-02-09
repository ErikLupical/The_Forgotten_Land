using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;

    public Rigidbody2D rb;
    private Vector2 direction;
    public InputActionReference move;
    public InputActionReference interact;
    public InputActionReference normal;

    public Animator anim;
    public int facingX = 1;

    public bool inCombat;
    public ActionState actionState;
    public EntityCombat entityCombat;

    public enum ActionState
    {
        Idle,
        Attacking,
        Interrupted,
    }

    private void OnEnable()
    {
        interact.action.started += OnInteractCallback;
        normal.action.started += OnAttackCallback;
    }

    private void OnDisable()
    {
        interact.action.started += OnInteractCallback;
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
        // Interaction logic here
        Debug.Log("Interacted");
        return;
    }

    public void OnAttack()
    {
        if (CanAttack())
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        if (actionState == ActionState.Idle) Move();
        else if (actionState == ActionState.Attacking)
            rb.linearVelocity = Vector2.zero;
    }

    private bool CanAttack()
    {
        return actionState != ActionState.Attacking &&
               actionState != ActionState.Interrupted &&
               inCombat && entityCombat.timer <= 0;
    }

    public IEnumerator Attack()
    {
        actionState = ActionState.Attacking;
        anim.SetBool("attacking", true);

        // Wait one frame so the animator switches states
        yield return null;

        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length - 0.01f;
        yield return new WaitForSeconds(animationLength);

        actionState = ActionState.Idle;
        anim.SetBool("attacking", false);
        yield break;
    }
    public void OnAttackFinished()
    {
        actionState = ActionState.Idle;
        anim.SetBool("attacking", false);
    }

    private void Move()
    {
        direction = move.action.ReadValue<Vector2>();

        if (direction.x > 0 && transform.localScale.x < 0 ||
            direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(direction.x));
        anim.SetFloat("vertical", Mathf.Abs(direction.y));

        if (GetComponent<EntityHealth>().isAlive)
            rb.linearVelocity = new Vector2(direction.x, direction.y) * speed;
        else
            rb.linearVelocity = new Vector2(0, 0);
    }

    private void Flip()
    {
        facingX *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
