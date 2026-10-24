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
    public string name;
    public float initSpeed;
    public int maxHP;
    public CombatStat combatStat;

}