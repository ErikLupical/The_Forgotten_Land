using UnityEngine;

public abstract class AbilityObject : ScriptableObject
{
    [Header("Ability Information")]
    public string abilityName;
    public enum AbilityType
    {
        Knight,
        Archer,
        Monk,
        Rogue
    }
    [TextArea(3, 5)]
    public string description;

    [Header("Timing")]
    public float cooldown;

    [Header("Hurtbox")]
    public Collider2D hurtbox;
    public GameObject owner => hurtbox?.transform.parent?.gameObject;
    public EntityCombat ownerCombat => owner?.GetComponent<EntityCombat>();

    [Header("Impact")]
    public float damageMultiplier;
    public float knockBack;

    public abstract void Execute();
}
