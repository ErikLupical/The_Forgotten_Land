using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Abilities/DefaultMeleeNormalAttack")]
public class DefaultMeleeNormalAttack : AbilityObject
{
    public override void Execute(Entity entity)
    {
        SectorUtility.AttackMelee(entity, radius, angle, damageMultiplier, hitMask);

    }
}