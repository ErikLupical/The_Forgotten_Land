using UnityEngine;

[CreateAssetMenu(menuName = "Combat/ArcherAbilities/DefaultArcherAttack")]
public class DefaultArcherAttack : AbilityObject
{
    public GameObject arrowPrefab;

    public override void Execute()
    {
        Collider2D[] targets = new Collider2D[10];
        Physics2D.OverlapCollider(hurtbox, ContactFilter2D.noFilter, targets);

        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D target in targets)
        {
            if (target == null) continue;

            GameObject hit = target.gameObject;
            if (hit == owner) continue;
            if (owner.CompareTag(hit.tag)) continue;

            EntityHealth hitHealth = hit.GetComponent<EntityHealth>();
            if (hitHealth == null) continue;

            float distance = Vector2.Distance(owner.transform.position, hit.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = hit.transform;
            }
        }

        if (nearestTarget == null) return;

        Vector2 direction = (nearestTarget.position - owner.transform.position).normalized;

        GameObject arrowInstance = Instantiate(
            arrowPrefab,
            owner.transform.position,
            Quaternion.identity
            );

        Arrow arrow = arrowInstance.GetComponent<Arrow>();
        arrow.Initialize(
            owner,
            ownerCombat,
            direction,
            damageMultiplier,
            knockBack,
            knockBackTime
        );
    }
}
