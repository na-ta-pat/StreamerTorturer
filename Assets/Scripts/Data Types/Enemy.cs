// [System.Serializable]
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemySO",
    menuName = "Enemy"
)]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public float initSpeed;
    public int maxHP;
    public CombatStat combatStat;
    public float knockbackResistance; // 1 = immune to knockback; 0 = get full knockback
    public float knocbackRecovery = 4;


}