using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{

    public Weapon weapon;
    public Vector2 playerPos;
    public float startTime;
    public HashSet<Enemy> hitEnemy;
    private Rigidbody2D rb;
    public void setVelocity(Vector2 v){
        if(v.magnitude==0)return;
        if (!rb) rb = GetComponent<Rigidbody2D>();
        rb.velocity = v;
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        hitEnemy = new();
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon.destroyTimer!=0 && Time.time-startTime>weapon.destroyTimer)Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy)){
            if(!hitEnemy.Add(enemy))return;
            enemy.recieveDamage(weapon.damage);
            enemy.receiveKnockback(playerPos,weapon.knocback);
            }
        if(weapon.destroyTimer==0)
            Destroy(gameObject);
    }
}
