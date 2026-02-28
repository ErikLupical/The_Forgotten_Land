using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [Header("Stats")]
    public int attack;

    [Header("Abilities")]
    public Collider2D rangedHurtbox;
    public Collider2D meleeHurtbox;

    public AbilityObject defaultKnightAbility;
    public AbilityObject defaultArcherAbility;
    public AbilityObject defaultMageAbility;
    public AbilityObject defaultPersonAbility;

    public AbilityObject ability;

    [Header("Timing")]
    public float timer;

    private void Awake()
    {
        EntityBehavior.EntityType type = GetComponent<EntityBehavior>().type;

        switch (type)
        {
            case EntityBehavior.EntityType.Knight:
                ability = defaultKnightAbility; break;
            case EntityBehavior.EntityType.Archer:
                ability = defaultArcherAbility; break;
            case EntityBehavior.EntityType.Mage:
                ability = defaultMageAbility; break;
            case EntityBehavior.EntityType.Person:
                ability = defaultPersonAbility; break;
        }

        if (type == EntityBehavior.EntityType.Knight || type == EntityBehavior.EntityType.Person)
        {
            if (ability != null) ability.hurtbox = meleeHurtbox;
        }
        if (type == EntityBehavior.EntityType.Archer || type == EntityBehavior.EntityType.Mage)
        {
            if (ability != null) ability.hurtbox = rangedHurtbox;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer = Mathf.Max(0, timer - Time.deltaTime);
        }
    }

    public void Normal()
    {
        if (ability != null)
        {
            ability.Execute();
            timer = ability.cooldown;
        }
    }
}
