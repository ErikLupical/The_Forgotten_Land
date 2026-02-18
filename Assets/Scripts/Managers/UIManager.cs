using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Player Stats")]
    public GameObject playerStats;
    private EntityBehavior playerBehavior;

    private void Awake()
    {
        instance = this;
        playerBehavior = (EntityBehavior) GameObject.Find("Player").GetComponent("EntityBehavior");
        playerStats.SetActive(EnemyManager.instance.InCombat);
    }
}
