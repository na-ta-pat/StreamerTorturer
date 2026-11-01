using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAllEnemiesDefeated : MonoBehaviour
{
    public UnityEvent onAllEnemiesDefeated;
    List<Enemy> allEnemies;

    private void Start()
    {
        var instances = GameObject.FindObjectsOfType<Enemy>();
        allEnemies = new List<Enemy>(instances);
    }

    void Update()
    {
        if (IsConditionTrue())
        {
            onAllEnemiesDefeated?.Invoke();
            Destroy(gameObject);
        }
    }

    bool IsConditionTrue()
    {
        if (allEnemies.Count == 0)
            return true;
        else return false;
    }

}
