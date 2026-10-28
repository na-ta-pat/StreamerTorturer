using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    [SerializeField] EnemySO enemyData;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] enemyIdle, enemyWalk, enemyAttack;
    [SerializeField] SpriteRenderer sr;
    Color originalSRcolor;
    float deathBreathTime;
    float hurtTime;
    float stunTime;
    float lifeSpan = 360;
    float born;
    float cooldownTimer;
    
    void Awake(){
        HP = enemyData.maxHP;
        combatStat = enemyData.combatStat;
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
        if(stunTime>Time.time){
            rb.velocity*=Mathf.Exp(-enemyData.knocbackRecovery*Time.deltaTime);
            if(rb.velocity.magnitude<0.1f)stunTime=0;
            return;
        }
        Player player = FindObjectOfType<Player>();
        movement = player.transform.position - transform.position;
        movement = movement.normalized;
        rb.velocity = movement * enemyData.initSpeed;
        setAnimFacing(movement);
    }

    void Died(){
        // if(enemyData.loot!=null)Instantiate(enemyData.loot, transform.position, Quaternion.identity);
        sr.color = new Color(1,1,1);
        if(deathBreathTime==0)
            deathBreathTime = Time.time + .5f;
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

    public void receiveKnockback(Vector2 pos,float force){
        Vector2 ePos = (Vector2) transform.position;
        float rotationRange = 10;
        Vector2 dir = Quaternion.Euler(0, 0, Random.Range(-rotationRange, rotationRange)) * (ePos-pos) ;
        dir = dir.normalized;
        rb.velocity = force * dir *(1 - enemyData.knockbackResistance);
        stunTime = Time.time + 1;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player)){
            if (cooldownTimer>Time.time)return;
            player.recieveDamage(enemyData.combatStat.attackPhysic);
            cooldownTimer = Time.time+enemyData.cooldown;
        }
    }
}