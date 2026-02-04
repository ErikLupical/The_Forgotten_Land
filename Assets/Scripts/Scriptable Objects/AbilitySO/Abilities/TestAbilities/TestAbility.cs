using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Abilities/TestAbility1")]
public class TestAbility : AbilityObject
{
    public int damage = 10;

    public override void Execute(Entity entity)
    {
        Debug.Log($"{entity.name} deals {damage} damage");
    }
}
