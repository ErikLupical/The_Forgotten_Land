using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [Header("Stats")]
    public int attack;

    [Header("Abilities")]
    public AbilityObject ability;

    [Header("Timing")]
    public float cooldown;
    public float timer;

    private void Start()
    {
        if (ability != null)
        {
            cooldown = ability.cooldown;
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
        if (ability != null) ability.Execute();
    }
}
