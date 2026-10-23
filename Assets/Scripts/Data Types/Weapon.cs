// [System.Serializable]
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "WeaponSO",
    menuName = "Weapon"
)]
public class Weapon : ScriptableObject
{
    [SerializeField] WeaponType type;
    [SerializeField] bool isMagical;
    [SerializeField] int damage;

}