using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;

    public Transform player;
    public float range = 1.5f;
    private float squareRrange;
    private float squareDistance;

    public Rigidbody2D rb;
    private Vector2 direction;

    public Animator anim;
    public int facingX = 1;

    public ActionState actionState;
    public EntityCombat entityCombat;

    public enum ActionState
    {
        Idle,
        Attacking,
        Interrupted,
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        squareRrange = range * range;
        actionState = ActionState.Idle;
    }

    private void FixedUpdate()
    {
        Vector2 rawDirection = (Vector2)(player.position - transform.position);
        squareDistance = rawDirection.sqrMagnitude;
        direction = rawDirection.normalized;

        if (actionState == ActionState.Idle)
        {
            Move();
        }
        if (squareDistance <= squareRrange)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetFloat("horizontal", 0);
            anim.SetFloat("vertical", 0);
            if (CanAttack())
            {
                StartCoroutine(Attack());
            }
        }
    }
    private bool CanAttack()
    {
        return actionState != ActionState.Attacking &&
               actionState != ActionState.Interrupted &&
               entityCombat.timer <= 0;
    }

    public IEnumerator Attack()
    {
        actionState = ActionState.Attacking;
        anim.SetBool("attacking", true);

        // Wait one frame so the animator switches states
        yield return null;

        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
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

    // Chases the player up to a certain range, stop when dead
    private void Move()
    {
        if (direction.x > 0 && transform.localScale.x < 0 ||
            direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        if (GetComponent<EntityHealth>().isAlive && squareDistance > squareRrange)
            rb.linearVelocity = new Vector2(direction.x, direction.y) * speed;
        else
            rb.linearVelocity = Vector2.zero;

        anim.SetFloat("horizontal", Mathf.Abs(rb.linearVelocity.x));
        anim.SetFloat("vertical", Mathf.Abs(rb.linearVelocity.y));
    }

    private void Flip()
    {
        facingX *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}