using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CombatStat{
    public float attackPhysic;
    public float attackMagic;
    public float defensePhysic;
    public float defenseMagic;
}
// public string[] MoveDict = {"up", "right", "down", "left", };
public class Charactor : MonoBehaviour
{
    public float initSpeed=1;
    public CombatStat combatStat;
    public int facing; //up = 0, right = 1, down = 2, left = 3
    
    protected void setFacing(Vector2 movement){
        if(movement.x!=0){
            if(movement.x>0)facing=1;
            else facing=3;
        }else if(movement.y!=0){
            if(movement.y>0)facing=0;
            else facing=2;
        }
    }

}
