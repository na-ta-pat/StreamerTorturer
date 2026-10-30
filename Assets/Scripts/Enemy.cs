using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    public EnemySO enemyData;
    public override CombatStat combatStat{get=>enemyData.combatStat;set{}}
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] enemyIdle, enemyWalk, enemyAttack;
    [SerializeField] Loot loot;
    SpriteRenderer sr;
    Color originalSRcolor;
    float deathBreathTime;
    float hurtTime;
    float lifeSpan = 360;
    float born;
    float cooldownTimer;
    
    void Awake(){
        HP = enemyData.maxHP;
        born = Time.time;
    }

    void Start(){
        if(sr == null) sr = GetComponentsInChildren<SpriteRenderer>()[0]; 
        originalSRcolor = sr.color;
    }
    
    void Update()
    {
        if(hurtTime<Time.time)resetHurtColor();
        if(Time.time>lifeSpan+born)Died();
        if(isDead)Died();
        if(StuntMech(enemyData.knocbackRecovery))return;
        Player player = FindObjectOfType<Player>();
        movement = player.transform.position - transform.position;
        movement = movement.normalized;
        rb.velocity = movement * enemyData.initSpeed;
        setAnimFacing(movement);
    }

    void Died(){
        sr.color = new Color(1,1,1);
        if(deathBreathTime==0){
            if(loot!=null)Instantiate(loot, transform.position, Quaternion.identity);
            deathBreathTime = Time.time + .5f;
        }
        if(Time.time < deathBreathTime)return;
        Destroy(gameObject);
    }
    
    public void recieveDamage(int dmg,bool isMagical){
        HurtColor();
        HP-=dmg;
    }

    private void HurtColor(){
        hurtTime = Time.time+0.1f;
        Color red = new Color(1,0,0);
        sr.color = red;
    }
    void resetHurtColor(){
        sr.color = originalSRcolor;
    }

    public void receiveKnockback(Vector2 pos,float force)=>receiveKnockback(pos,force,enemyData.knockbackResistance);

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player)){
            if (cooldownTimer>Time.time)return;
            player.receiveKnockback(transform.position,enemyData.attackKnockback);
            player.recieveDamage(enemyData.combatStat.attackPhysic);
            cooldownTimer = Time.time+enemyData.cooldown;
        }
    }
}