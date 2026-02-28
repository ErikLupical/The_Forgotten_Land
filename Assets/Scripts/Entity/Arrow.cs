using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public float lifeSpan = 2f;
    public float speed;

    private GameObject owner;
    private EntityCombat ownerCombat;
    private float damageMultiplier;
    private float knockBack;
    private float knockBackTime;

    public void Initialize(
        GameObject owner,
        EntityCombat ownerCombat,
        Vector2 direction,
        float damageMultiplier,
        float knockBack,
        float knockBackTime)
    {
        this.owner = owner;
        this.ownerCombat = ownerCombat;
        this.damageMultiplier = damageMultiplier;
        this.knockBack = knockBack;
        this.knockBackTime = knockBackTime;

        transform.right = direction; // rotate arrow to face direction
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifeSpan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner == null)
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject == owner) return;
        if (owner.CompareTag(collision.tag)) return;

        EntityHealth hitHealth = collision.GetComponent<EntityHealth>();
        EntityBehavior hitBehavior = collision.GetComponent<EntityBehavior>();
        if (hitHealth == null) return;

        int damage = Mathf.FloorToInt(ownerCombat.attack * damageMultiplier);
        hitHealth.ChangeHealth(-damage);
        hitBehavior.Interrupt(transform, knockBack, knockBackTime);
    }
}
