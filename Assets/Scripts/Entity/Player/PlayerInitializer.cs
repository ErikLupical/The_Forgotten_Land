using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    void Start()
    {
        EntityBehavior behavior = GetComponent<EntityBehavior>();
        EntityHealth health = GetComponent<EntityHealth>();
        EntityCombat combat = GetComponent<EntityCombat>();

        CharacterStats stats = GameData.selectedStats;

        behavior.faction = GameData.selectedFaction;
        behavior.type = GameData.selectedType;

        behavior.speed = stats.speed;

        combat.attack = stats.attack;

        health.maxHealth = stats.hp;
        health.currentHealth = stats.hp;
    }
}
