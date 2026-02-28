using UnityEngine;

[CreateAssetMenu(menuName = "Combat/MageAbilities/DefaultMageAttack")]
public class DefaultMageAttack : AbilityObject
{
    public GameObject explosionPrefab;

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

        GameObject explosionInstance = Instantiate(
            explosionPrefab,
            nearestTarget.position,
            Quaternion.identity
            );

        Explosion explosion = explosionInstance.GetComponent<Explosion>();
        explosion.Initialize(
            owner,
            ownerCombat,
            damageMultiplier,
            knockBack,
            knockBackTime
        );
    }
}
