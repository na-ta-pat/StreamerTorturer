using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// public string[] MoveDict = {"up", "right", "down", "left", };
public class Player : Character
{
    public CombatStat combatStat;
    public float speedBoost;
    public int state;
    const int IDLE = 0;
    const int MOVING = 1;
    Vector2 lastMove;
    float cooldown;
    string currentAnim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] playerIdle, playerWalk, playerSwinging, playerShooting, playerUseRod;
    [SerializeField] Weapon activeWeapon,weapon1,weapon2;
    [SerializeField] Armor activeArmor;

    void Awake() {

    }
    // weapns,1,2,armor
    void Start()
    {
        
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(movement.magnitude>1)movement/=movement.magnitude;
        if(movement.magnitude>0)lastMove = movement;
        // transform.position += (Vector3) (movement) * initSpeed * Time.deltaTime; // collision problem
        rb.velocity = (movement) * initSpeed;
        UpdateAnim();
        cooldown -= Time.deltaTime ;
        if(Input.GetKeyDown(KeyCode.K))Attack();
        
    }

    void Attack(){
        if ( cooldown > 0 )return;
        else cooldown = activeWeapon.cooldown;
        Vector2 initPos = (Vector2) transform.position + lastMove.normalized * activeWeapon.range;
        HitBox hitBox = Instantiate(activeWeapon.hitBox, initPos, Quaternion.FromToRotation(Vector2.right, lastMove));
        hitBox.damage = activeWeapon.damage;
    }

    void UpdateAnim(){
        if (movement==Vector2.zero)state = IDLE;
        else state = MOVING;
        switch(state){
            case IDLE:
                ChangeAnimation(playerIdle[facing]);
            break;
            case MOVING:
                ChangeAnimation(playerWalk[facing]);
                setFacing(movement);
            break;
        }
    }
    
    void ChooseWeapon(int index=0){ // if index = 1/2 set weapon according to. 
        if(index==1)activeWeapon = weapon1;
        else if(index==2)activeWeapon = weapon2;
    }
    void SwitchWeapon() => activeWeapon = activeWeapon == weapon1 ? weapon2 : weapon1;


    void ChangeAnimation(AnimationClip toAnim)
    {
        if(currentAnim != toAnim.name)
        {
            anim.Play(toAnim.name);
            currentAnim = toAnim.name;
        }
    }
    




}
