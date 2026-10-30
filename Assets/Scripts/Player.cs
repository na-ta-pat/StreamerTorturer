using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction{

    static public string[] strMoveDict = {"up", "right", "down", "left", };
    static public Vector2[] vec2MoveDict = {Vector2.up, Vector2.right, Vector2.down, Vector2.left, };
}

[System.Serializable]
public class HoldingWeapons{
    public Weapon weapon;
    public float lastUse;
    public bool isOnCooldown=>lastUse+weapon.cooldown>Time.time;
    public void Use(Player player){
        if(isOnCooldown)return;
        else ForceUse(player);
    }
    public float getReloadPercent(){ // for ui
        float o = (Time.time-lastUse)/weapon.cooldown;
        if(o>1)o=1;
        return o;
    }
    public void ForceUse(Player player){
        lastUse = Time.time;
        Vector2 playerPos = player.transform.position;
        Vector2 looking = player.looking;
        Vector2 looking4dir = Direction.vec2MoveDict[player.animFacing];
        Vector2 initPos = (Vector2) playerPos + looking.normalized * weapon.range;

        var anim = Player.Instantiate(weapon.anim, playerPos, Quaternion.FromToRotation(Vector2.right, looking4dir),player.transform);
        
        // HitBox hitBox = Player.Instantiate(weapon.hitBox, initPos, Direction.vec2MoveDict[player.animFacing]);
        HitBox hitBox = Player.Instantiate(weapon.hitBox, initPos, Quaternion.FromToRotation(Vector2.right, looking4dir));
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
    public int _maxHP = 4;
    public int maxHP{get=>_maxHP;set{_maxHP=value;GameManager.Instance.ui.UpdateMaxHP(maxHP);}}
    public bool enableInvincible;
    public float speedBoost;
    public int state;
    bool attackState;
    const int IDLE = 0;
    const int MOVING = 1;
    const int SWING = (int)WeaponType.Swinging;
    const int USEROD = (int)WeaponType.Rod;
    Vector2 lastMove;
    float cooldown;
    string currentAnim;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] playerIdle, playerWalk, playerSwinging, playerShooting, playerUseRod;
    // [SerializeField] Weapon activeWeapon,weapon1,weapon2;
    public HoldingWeapons lWeapon,rWeapon;
    [SerializeField] Armor activeArmor;
    [SerializeField] float knockbackResistance;
    Controller controller;

    public float invincibleCooldownTime;
    float invincibleTime;
    public bool isInvincible => invincibleTime>Time.time;
    public bool forceAnim;

    public override int HP{get=>_HP;set{_HP=value;GameManager.Instance.ui.UpdateHP(HP);}}

    void Awake() {
    }
    void Start()
    {
        HP = maxHP;
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
        if(!StuntMech(8))rb.velocity = (movement) * initSpeed;
        // if(controller.attack)Attack(activeWeapon);
        if(!lWeapon.isOnCooldown && controller.attackLeft.isPressed){
            forceAnim = true;
            lWeapon.Use(this);
            state = (int)lWeapon.weapon.type;
        }
        if(!rWeapon.isOnCooldown && controller.attackRight.isPressed){
            forceAnim = true;
            rWeapon.Use(this);
            state = (int)rWeapon.weapon.type;
        }
        // if(controller.isSwitchWeapon())SwitchWeapon();
        UpdateAnim();
        
    }

    void Died(){
        HP = maxHP; //test
        maxHP+=1;

    }

    void UpdateAnim(){
        if(! forceAnim && attackState && anim.GetCurrentAnimatorStateInfo(0).normalizedTime<.9f)return;
        else attackState = false;
        setAnimFacing(looking);
        switch(state){
            case IDLE:
                ChangeAnimation(playerIdle, animFacing);
            break;
            case MOVING:
                ChangeAnimation(playerWalk, animFacing);
            break;
            case SWING:
                attackState = true;
                ChangeAnimation(playerSwinging, animFacing);
                forceAnim = false;
            break;
            case USEROD:
                attackState = true;
                ChangeAnimation(playerUseRod, animFacing);
                forceAnim = false;
            break;
        }
        if (!isMoving)state = IDLE;
        else state = MOVING;
    }
    
    // void ChooseWeapon(int index=0){ // if index = 1/2 set weapon according to. 
    //     if(index==1)activeWeapon = weapon1;
    //     else if(index==2)activeWeapon = weapon2;
    // }
    // void SwitchWeapon() => activeWeapon = activeWeapon == weapon1 ? weapon2 : weapon1;


    void ChangeAnimation(AnimationClip toAnim)
    {
        if(forceAnim || currentAnim != toAnim.name)
        {

            anim.Play(toAnim.name, 0, 0f);

            currentAnim = toAnim.name;
        }
    }

    void ChangeAnimation(AnimationClip[] toAnim, int index){
        if(animFacing<toAnim.Length)ChangeAnimation(toAnim[index]);
    }


    public void recieveDamage(int dmg){
        if(enableInvincible && isInvincible)return;
        HP-=dmg;
        
        invincibleTime = invincibleCooldownTime+Time.time;
    }

    // public void receiveKnockback(Vector3 pos)=>receiveKnockback(pos,0);
    public void receiveKnockback(Vector2 pos,float force){
        if(enableInvincible && isInvincible)return;
        receiveKnockback(pos,force,knockbackResistance);
    }


}
