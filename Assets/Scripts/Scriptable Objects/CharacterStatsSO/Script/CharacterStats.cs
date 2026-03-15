using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Game/Character Stats")]
public class CharacterStats : ScriptableObject
{
    public int attack;
    public int speed;
    public int hp;

    public AbilityObject defaultAbility;
}