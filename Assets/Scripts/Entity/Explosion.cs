using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameObject owner;
    private EntityCombat ownerCombat;
    private float damageMultiplier;
    private float knockBack;
    private float knockBackTime;

    private CircleCollider2D circleCollider;

    public void Initialize(
        GameObject owner,
        EntityCombat ownerCombat,
        float damageMultiplier,
        float knockBack,
        float knockBackTime)
    {
        this.owner = owner;
        this.ownerCombat = ownerCombat;
        this.damageMultiplier = damageMultiplier;
        this.knockBack = knockBack;
        this.knockBackTime = knockBackTime;

        circleCollider = GetComponent<CircleCollider2D>();

        DealDamage();
    }

    private void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            circleCollider.radius * transform.lossyScale.x
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == owner) continue;
            if (owner.CompareTag(hit.tag)) continue;

            EntityHealth hitHealth = hit.GetComponent<EntityHealth>();
            EntityBehavior hitBehavior = hit.GetComponent<EntityBehavior>();

            if (hitHealth == null) continue;

            int damage = Mathf.FloorToInt(ownerCombat.attack * damageMultiplier);
            hitHealth.ChangeHealth(-damage);

            if (hitBehavior != null)
                hitBehavior.Interrupt(transform, knockBack, knockBackTime);
        }
    }

    // Call this from Animation Event at end of explosion animation
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
