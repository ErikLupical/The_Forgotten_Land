using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Abilities/ExampleAbility")]
public class ExampleAbility : AbilityObject
{
    public override void Execute(Entity entity)
    {
        Debug.Log($"The player has attacked and hit an enemy.");
    }
}
