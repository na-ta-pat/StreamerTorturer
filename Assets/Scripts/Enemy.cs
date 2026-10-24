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
        setFacing(movement);
    }

    void Died(){
        Destroy(gameObject);
    }
    
}