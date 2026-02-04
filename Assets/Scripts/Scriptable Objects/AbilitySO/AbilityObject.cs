using UnityEngine;

public abstract class AbilityObject : ScriptableObject
{
    [Header("Ability Information")]
    public string abilityName;
    [TextArea(3, 5)]
    public string description;

    [Header("Timing")]
    public float windup;
    public float recovery;
    public float cooldown;

    [Header("Charge")]
    public float chargeCost;
    public float chargeGain;

    [Header("Hitbox")]
    public float radius;
    [Range(0f, 360f)]
    public float angle;
    public LayerMask hitMask;

    [Header("Impact")]
    public float damageMultiplier;
    public float knockBack;

    [Header("Behavior")]
    public bool canBeInterrupted;

    public abstract void Execute(Entity entity);
}
