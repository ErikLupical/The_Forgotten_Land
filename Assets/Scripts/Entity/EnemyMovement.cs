using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private bool aggressive;
    public float range = 1.5f;

    private float squareRrange;
    private float squareDistance;
    private Vector2 direction;

    public EntityBehavior entityBehavior;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        // Debugging
        //Aggressive = true;
    }

    private void Start()
    {
        squareRrange = range * range;
        entityBehavior.actionState = EntityBehavior.ActionState.Idle;
    }

    private void FixedUpdate()
    {
        if (entityBehavior.actionState != EntityBehavior.ActionState.Idle) return;

        if (aggressive)
        {
            Vector2 rawDirection = (Vector2)(player.position - transform.position);
            squareDistance = rawDirection.sqrMagnitude;
            direction = rawDirection.normalized;

            if (entityBehavior.actionState == EntityBehavior.ActionState.Idle)
            {
                Move();
            }
            if (squareDistance <= squareRrange)
            {
                entityBehavior.rb.linearVelocity = Vector2.zero;
                entityBehavior.UpdateAnimation(0, 0);

                if (entityBehavior.CanAttack() && player.gameObject.activeInHierarchy)
                {
                    StartCoroutine(entityBehavior.Attack());
                }
            }
        }
    }

    private void OnDisable()
    {
        if (aggressive)
            EnemyManager.instance?.UnregisterEntity(gameObject);
    }

    // Chases the player up to a certain range, stop when dead
    private void Move()
    {
        if (direction.x > 0 && transform.localScale.x < 0 ||
            direction.x < 0 && transform.localScale.x > 0)
        {
            entityBehavior.Flip();
        }

        entityBehavior.UpdateAnimation(Mathf.Abs(direction.x), Mathf.Abs(direction.y));

        if (GetComponent<EntityHealth>().isAlive && squareDistance > squareRrange)
            entityBehavior.rb.linearVelocity = new Vector2(direction.x, direction.y) * entityBehavior.speed;
        else
            entityBehavior.rb.linearVelocity = Vector2.zero;
    }

    public bool Aggressive
    {
        get => aggressive;
        set
        {
            if (aggressive == value) return;

            aggressive = value;

            if (aggressive)
                EnemyManager.instance?.RegisterEntity(gameObject);
            else
                EnemyManager.instance?.UnregisterEntity(gameObject);
        }
    }
}