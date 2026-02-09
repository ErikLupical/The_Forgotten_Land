using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [Header("Stats")]
    public int attack;

    [Header("Abilities")]
    public Collider2D hurtbox;
    public AbilityObject ability;

    [Header("Timing")]
    public float timer;

    private void Awake()
    {
        if (ability != null)
        {
            ability.hurtbox = hurtbox;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer = Mathf.Max(0, timer - Time.deltaTime);
        }
    }

    public void Normal()
    {
        if (ability != null)
        {
            ability.Execute();
            timer = ability.cooldown;
        }
    }

    //public void Knockback(Transform attacker, float knockback)
    //{
    //    GetComponent<EnemyMovement>().actionState = 
    //    actionState = ActionState.Interrupted;
    //    Vector2 direction = transform.position - attacker.position;
    //    rb.linearVelocity = direction.normalized * knockback;
    //}
}
