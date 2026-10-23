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
public class Player : MonoBehaviour
{
    public CombatStat combatStat;
    public int state;
    const int IDLE = 0;
    const int MOVING = 1;
    public int facing; //up = 0, right = 1, down = 2, left = 3
    string currentAnim;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] playerIdle, playerWalk, playerSwinging, playerShooting, playerUseRod;
    [SerializeField] Weapon activeWeapon;

    void Awake() {

    }
    // weapns,1,2,armor
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // right-left,up-down (1 - -1)
        setFacing(input);
        // return;
        switch(state){
            case IDLE:
                ChangeAnimation(playerIdle[facing]);
            break;
            case MOVING:
                ChangeAnimation(playerWalk[facing]);
            break;
        }
    }
    
    void setFacing(Vector2 input){
        if(input.x!=0){
            if(input.x>0)facing=1;
            else facing=3;
        }else if(input.y!=0){
            if(input.y>0)facing=0;
            else facing=2;
        }
    }

    void ChangeAnimation(AnimationClip toAnim)
    {
        if(currentAnim != toAnim.name)
        {
            anim.Play(toAnim.name);
            currentAnim = toAnim.name;
        }
    }
}
