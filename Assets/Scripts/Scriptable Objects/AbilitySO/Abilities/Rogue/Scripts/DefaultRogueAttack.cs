using UnityEngine;

[CreateAssetMenu(menuName = "Combat/RogueAbilities/DefaultRogueAttack")]
public class DefaultRogueAttack : AbilityObject
{
    public override void Execute()
    {
        Collider2D[] targets = new Collider2D[10];
        Physics2D.OverlapCollider(hurtbox, ContactFilter2D.noFilter, targets);

        foreach (Collider2D target in targets)
        {
            if (target == null) continue;

            GameObject hit = target.gameObject;
            if (hit == owner) continue;
            // Friendly fire check
            if (owner.CompareTag(hit.tag)) continue;

            EntityHealth hitHealth = hit.GetComponent<EntityHealth>();
            EntityBehavior hitBehavior = hit.GetComponent<EntityBehavior>();
            if (hitHealth == null) continue;

            int damage = Mathf.FloorToInt(ownerCombat.attack * damageMultiplier);
            hitHealth.ChangeHealth(-damage);
            hitBehavior.Interrupt(owner.transform, knockBack, knockBackTime);
        }
    }
}