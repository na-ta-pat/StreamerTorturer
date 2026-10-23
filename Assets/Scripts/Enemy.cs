using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Charactor
{
    [SerializeField] Rigidbody2D rb;
    void Update()
    {
        Player player = FindObjectOfType<Player>();
        Vector2 input = player.transform.position - transform.position;
        input = input.normalized;
        rb.velocity = input * initSpeed;
    }
    
}