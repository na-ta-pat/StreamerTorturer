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
    Controller controller;

    void Awake() {
    }
    void Start()
    {
        controller = GameManager.Instance.controller;
    }

    void Update()
    {
        movement = controller.movement;
        looking = controller.looking;
        if(movement.magnitude>1)movement/=movement.magnitude;
        if(movement.magnitude>0)lastMove = movement;
        if(looking.magnitude==0)looking=lastMove;
        // transform.position += (Vector3) (movement) * initSpeed * Time.deltaTime; // collision problem
        rb.velocity = (movement) * initSpeed;
        UpdateAnim();
        // if(controller.attack)Attack(activeWeapon);
        if(controller.attackLeft)Attack(weapon1);
        if(controller.attackRight)Attack(weapon2);
        if(controller.isSwitchWeapon())SwitchWeapon();
        
    }

    void Attack(Weapon weapon){
        if ( cooldown < Time.time )return;
        else cooldown = weapon.cooldown + Time.time;
        Vector2 initPos = (Vector2) transform.position + looking.normalized * weapon.range;
        HitBox hitBox = Instantiate(weapon.hitBox, initPos, Quaternion.FromToRotation(Vector2.right, looking));
        hitBox.weapon = weapon;
        hitBox.playerPos = transform.position;
    }

    void UpdateAnim(){
        if (!isMoving)state = IDLE;
        else state = MOVING;
        setAnimFacing(looking);
        switch(state){
            case IDLE:
                if(animFacing<playerIdle.Length)ChangeAnimation(playerIdle[animFacing]);
            break;
            case MOVING:
                if(animFacing<playerWalk.Length)ChangeAnimation(playerWalk[animFacing]);
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
