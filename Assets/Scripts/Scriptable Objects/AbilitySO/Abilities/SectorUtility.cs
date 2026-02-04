using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public static class SectorUtility
{
    public static bool IsPointInSector(
        Vector2 origin,
        Vector2 facing,
        Vector2 target,
        float radius,
        float angle
    )
    {
        Vector2 toPoint = target - origin;

        if (toPoint.sqrMagnitude > radius * radius)
            return false;

        float halfAngle = angle * 0.5f;
        float dot = Vector2.Dot(facing.normalized, toPoint.normalized);
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        return theta <= halfAngle;
    }

    public static void AttackMelee(Entity attacker, float radius, float angle, float damageMultiplier, LayerMask hitMask)
    {
        Vector2 origin = attacker.transform.position;
        Vector2 facing = attacker.facingDirection;

        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, hitMask);

        foreach (Collider2D hit in hits)
        {
            if (hit.attachedRigidbody == null) continue;

            Entity target = hit.GetComponent<Entity>();
            if (target == null || target == attacker) continue;

            if (!IsPointInSector(
                origin,
                facing,
                target.transform.position,
                radius,
                angle))
                continue;

            int damage = (int)Mathf.Floor(attacker.attack * damageMultiplier);
            target.hitPoints -= damage;

            Debug.Log($"{attacker.entityName} hit {target.entityName} for {damage} damage.");
        }
    }
}
