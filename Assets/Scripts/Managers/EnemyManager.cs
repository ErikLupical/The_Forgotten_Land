using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private HashSet<GameObject> activeEnemies = new HashSet<GameObject>();

    public bool InCombat => activeEnemies.Count > 0;

    private void Awake()
    {
        // Simple singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterEntity(GameObject entity)
    {
        activeEnemies.Add(entity);
    }

    public void UnregisterEntity(GameObject entity)
    {
        activeEnemies.Remove(entity);
    }
}
