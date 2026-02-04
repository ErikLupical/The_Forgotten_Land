using UnityEngine;

public class TestMeleeAttack : MonoBehaviour
{
    private Entity entity;
    private TestAbility testAbility;

    void Start()
    {
        // Create Entity GameObject
        GameObject go = new GameObject("Test Entity");
        entity = go.AddComponent<Entity>();

        // Create MeleeAttack ScriptableObject
        testAbility = ScriptableObject.CreateInstance<TestAbility>();
        testAbility.damage = 10;
        testAbility.windup = 0.2f;
        testAbility.recovery = 0.3f;
        testAbility.cooldown = 1f;

        // Assign ability
        entity.normalAbility = testAbility;

        // Trigger attack
        Debug.Log("Triggering MeleeAttack...");
        entity.OnNormal();
    }

    void Update()
    {
        // Visual feedback of state changes
        Debug.Log($"State: {entity.state}, Timer: {entity.actionTimer:F2}");
    }
}
