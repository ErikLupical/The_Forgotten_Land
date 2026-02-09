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

    [Header("Hitbox")]
    [HideInInspector] public Collider2D hurtbox;
    public LayerMask hitMask;

    [Header("Impact")]
    public float damageMultiplier;
    public float knockBack;

    public virtual void Initialize(GameObject owner)
    {
        hurtbox = owner.GetComponentInChildren<Collider2D>();
    }

    public abstract void Execute();
}
