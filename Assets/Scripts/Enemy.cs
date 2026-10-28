using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    [SerializeField] EnemySO enemyData;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] enemyIdle, enemyWalk, enemyAttack;
    float stunTime;
    float lifeSpan = 360;
    float born;
    
    void Awake(){
        HP = enemyData.maxHP;
        combatStat = enemyData.combatStat;
        born = Time.time;
    }
    
    void Update()
    {
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
        Destroy(gameObject);
    }
    
    public void recieveDamage(int dmg){
        HP-=dmg;
    }
    public void receiveKnockback(Vector2 pos,float force){
        Vector2 ePos = (Vector2) transform.position;
        float rotationRange = 10;
        Vector2 dir = Quaternion.Euler(0, 0, Random.Range(-rotationRange, rotationRange)) * (ePos-pos) ;
        dir = dir.normalized;
        rb.velocity = force * dir *(1 - enemyData.knockbackResistance);
        stunTime = Time.time + 1;
    }
    
}