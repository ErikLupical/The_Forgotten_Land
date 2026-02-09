using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Abilities/DefaultMeleeNormalAttack")]
public class DefaultKnightAttack : AbilityObject
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

            EntityHealth hitHealth = hit.GetComponent<EntityHealth>();
            //Debug.Log("Hit Health: " + hitHealth);
            if (hitHealth == null) continue;

            // Friendly fire check
            if (owner.CompareTag(hit.tag)) continue;

            int damage = Mathf.FloorToInt(ownerCombat.attack * damageMultiplier);
            hitHealth.ChangeHealth(-damage);
        }
    }
}