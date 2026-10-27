using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    [SerializeField] EnemySO enemyData;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] enemyIdle, enemyWalk, enemyAttack;
    
    void Awake(){
        HP = enemyData.maxHP;
    }
    
    void Update()
    {
        if(isDead)Died();
        Player player = FindObjectOfType<Player>();
        movement = player.transform.position - transform.position;
        movement = movement.normalized;
        rb.velocity = movement * enemyData.initSpeed;
        setAnimFacing(movement);
    }

    void Died(){
        Destroy(gameObject);
    }
    
    public void recieveDamage(float dmg){
        HP-=dmg;
    }
    public void receiveKnockback(Vector2 pos,float force){
        rb.velocity = force * (pos-pos) * (1 - enemyData.knockbackResistance);
    }
    
}