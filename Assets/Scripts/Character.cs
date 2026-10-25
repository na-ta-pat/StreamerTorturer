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
public class Character : MonoBehaviour
{
    public float initSpeed=1;
    public Vector2 movement;
    public Vector2 looking;
    public int animFacing; //up = 0, right = 1, down = 2, left = 3
    public int HP;
    public bool isDead => HP<=0;
    public bool isMoving => movement.magnitude > 0;
    public bool isLooking => looking.magnitude > 0;
    
    protected void setAnimFacing(Vector2 looking){
        animFacing = _setAnimFacing(looking);
    }
    protected int _setAnimFacing(Vector2 looking){
        Vector2 nl = looking.normalized;
        if(looking.x>0.7071f)return 1;
        if(looking.x<-0.7071f)return 3;
        if(looking.y>0)return 0;
        if(looking.y<0)return 2;
        return 2;
    }
    protected void setAnimFacingLegacy(Vector2 movement){
        if(movement.x!=0){
            if(movement.x>0)animFacing=1;
            else animFacing=3;
        }else if(movement.y!=0){
            if(movement.y>0)animFacing=0;
            else animFacing=2;
        }
    }


}
