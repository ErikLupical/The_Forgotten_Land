using System.Collections;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public Animator anim;
    public int facingX = 1;

    public ActionState actionState;
    public EntityCombat entityCombat;
    public enum ActionState
    {
        Idle,
        Attacking,
        Interrupted,
        Interacting,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        entityCombat = GetComponent<EntityCombat>();
    }

    void Start()
    {
        actionState = ActionState.Idle;
    }

    public bool CanAttack()
    {
        return actionState != ActionState.Attacking &&
               actionState != ActionState.Interrupted &&
               entityCombat != null && entityCombat.timer <= 0;
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

    public void Flip()
    {
        facingX *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void UpdateAnimation(float horizontalSpeed, float verticalSpeed)
    {
        anim.SetFloat("horizontal", Mathf.Abs(horizontalSpeed));
        anim.SetFloat("vertical", Mathf.Abs(verticalSpeed));
    }

    public void Interrupt(Transform attacker, float knockBack, float knockBackTime)
    {
        actionState = ActionState.Interrupted;
        Vector2 direction = (transform.position - attacker.position).normalized;
        rb.linearVelocity = direction * knockBack;

        StartCoroutine(KnockbackCounter(knockBackTime));
    }

    IEnumerator KnockbackCounter(float knockBackTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        actionState = ActionState.Idle;
    }
}
