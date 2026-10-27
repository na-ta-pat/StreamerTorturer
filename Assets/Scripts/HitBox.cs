using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{

    public Weapon weapon;
    public Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy)){
            enemy.recieveDamage(weapon.damage);
            enemy.receiveKnockback(playerPos,weapon.knocback);
            }
        Destroy(gameObject);
    }
}
