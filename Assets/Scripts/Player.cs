using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// public string[] MoveDict = {"up", "right", "down", "left", };

[System.Serializable]
public class HoldingWeapons{
    public Weapon weapon;
    public float lastUse;
    public bool isOnCooldown=>lastUse+weapon.cooldown>Time.time;
    public void Use(Player player){
        if(isOnCooldown)return;
        else ForceUse(player);
    }
    public void ForceUse(Player player){
        lastUse = Time.time;
        Vector2 playerPos = player.transform.position;
        Vector2 looking = player.looking;
        Vector2 initPos = (Vector2) playerPos + looking.normalized * weapon.range;
        HitBox hitBox = Player.Instantiate(weapon.hitBox, initPos, Quaternion.FromToRotation(Vector2.right, looking));
        hitBox.weapon = weapon;
        hitBox.damage = weapon.damage * (weapon.isMagical? player.combatStat.attackMagic:player.combatStat.attackPhysic);
        hitBox.playerPos = playerPos;
        hitBox.setVelocity (looking.normalized * weapon.velocity);
    }
}

public class Player : Character
{
    public CombatStat _combatStat;
    public override CombatStat combatStat{get=>_combatStat;set=>_combatStat=value;}
    public int maxHP = 7;
    public bool enableInvincible;
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
    // [SerializeField] Weapon activeWeapon,weapon1,weapon2;
    [SerializeField] HoldingWeapons lWeapon,rWeapon;
    [SerializeField] Armor activeArmor;
    Controller controller;

    public float invincibleCooldownTime;
    float invincibleTime;
    public bool isInvincible => invincibleTime>Time.time;

    void Awake() {
        HP = maxHP;
        HP-=1; // test health ui
    }
    void Start()
    {
        controller = GameManager.Instance.controller;
        invincibleTime = Time.time;
    }

    void Update()
    {
        if(HP<=0)Died();
        movement = controller.movement;
        looking = controller.looking;
        if(movement.magnitude>1)movement/=movement.magnitude;
        if(movement.magnitude>0)lastMove = movement;
        if(looking.magnitude==0)looking=lastMove;
        // transform.position += (Vector3) (movement) * initSpeed * Time.deltaTime; // collision problem
        rb.velocity = (movement) * initSpeed;
        UpdateAnim();
        // if(controller.attack)Attack(activeWeapon);
        if(!lWeapon.isOnCooldown && controller.attackLeft.isPressed)lWeapon.Use(this);
        if(!rWeapon.isOnCooldown && controller.attackRight.isPressed)rWeapon.Use(this);
        // if(controller.isSwitchWeapon())SwitchWeapon();
        
    }

    void Died(){
        HP = maxHP; //test
        maxHP+=1;
        GameManager.Instance.ui.UpdateMaxHP(maxHP);
        GameManager.Instance.ui.UpdateHP(HP);

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
    
    // void ChooseWeapon(int index=0){ // if index = 1/2 set weapon according to. 
    //     if(index==1)activeWeapon = weapon1;
    //     else if(index==2)activeWeapon = weapon2;
    // }
    // void SwitchWeapon() => activeWeapon = activeWeapon == weapon1 ? weapon2 : weapon1;


    void ChangeAnimation(AnimationClip toAnim)
    {
        if(currentAnim != toAnim.name)
        {
            anim.Play(toAnim.name);
            currentAnim = toAnim.name;
        }
    }


    public void recieveDamage(int dmg){
        if(enableInvincible && isInvincible)return;
        HP-=dmg;
        GameManager.Instance.ui.UpdateHP(HP);
        invincibleTime = invincibleCooldownTime+Time.time;
    }



}
