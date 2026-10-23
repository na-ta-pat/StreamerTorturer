using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// public string[] MoveDict = {"up", "right", "down", "left", };
public class Player : Charactor
{
    public float speedBoost;
    public int state;
    public Vector2 movement;
    const int IDLE = 0;
    const int MOVING = 1;
    string currentAnim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip[] playerIdle, playerWalk, playerSwinging, playerShooting, playerUseRod;
    [SerializeField] Weapon activeWeapon,weapon1,weapon2;
    [SerializeField] Armor activeArmor;

    void Awake() {

    }
    // weapns,1,2,armor
    void Start()
    {
        
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = (movement) * initSpeed;
        // transform.position += (Vector3) (movement) * initSpeed * Time.deltaTime; // collision problem
        if (movement==Vector2.zero)state = IDLE;
        else state = MOVING;
        switch(state){
            case IDLE:
                ChangeAnimation(playerIdle[facing]);
            break;
            case MOVING:
                ChangeAnimation(playerWalk[facing]);
                setFacing(movement);
                facing=0;
            break;
        }
    }
    
    void ChooseWeapon(int index=0){ // if index = 1/2 set weapon according to. Else, switch
        if(index==1)activeWeapon = weapon1;
        else if(index==2)activeWeapon = weapon2;
    }
    void SwitchWeapon() => activeWeapon = activeWeapon == weapon1 ? weapon2 : weapon1;


    void ChangeAnimation(AnimationClip toAnim)
    {
        if(currentAnim != toAnim.name)
        {
            anim.Play(toAnim.name);
            currentAnim = toAnim.name;
        }
    }
    




}
