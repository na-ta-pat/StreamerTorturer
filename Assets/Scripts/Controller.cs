using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Vector2 movement;
    public Vector2 looking;
    public bool attack;
    bool switchWeapon;
    bool switchWeaponSeen;
    public bool enableMouse=true;
    public float mouseDistanceActivation = 10f;
    Vector2 mouseLastPos;
    float mouseLastTime;
    public float mouseTimeDeactivation=1f;
    public bool attackLeft;
    public bool attackRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        looking = Vector2.zero;
        UpdateInput();
        Cursor.visible = enableMouse;
        if(switchWeapon==false)switchWeaponSeen=false;
    }
    public bool isSwitchWeapon(){
        if(switchWeapon){
            if (!switchWeaponSeen)return true;
            else switchWeaponSeen = true;
        }
        return false;
    }
    void UpdateInput(){
        var gamepad = Gamepad.current; //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/Gamepad.html
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;
        if (gamepad!=null){
            movement = gamepad.leftStick.ReadValue();
            looking = gamepad.rightStick.ReadValue();
            attack = gamepad.buttonWest.isPressed;
            switchWeapon = gamepad.buttonWest.isPressed;
            return;
        } 
        if (keyboard!=null){
            attack = keyboard.kKey.isPressed;
            switchWeapon = keyboard.qKey.isPressed;
        } 
        if (mouse!=null){
            Vector2 mousePos = mouse.position.ReadValue();
            CheckMouseEnable(mousePos);
            if(enableMouse){
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                    new Vector3(mousePos.x, mousePos.y, 0f)
                );
                looking = (Vector2)  (worldPos - GameManager.Instance.player.transform.position);
            }
            attackLeft = mouse.leftButton.isPressed;
            attackRight = mouse.rightButton.isPressed;
        }else enableMouse = false;
    }
    
    bool CheckMouseEnable(Vector2 mousePos){
        if((mousePos-mouseLastPos).magnitude > mouseDistanceActivation)enableMouse=true;
        if(enableMouse){
            if((mouseLastPos-mousePos).magnitude>0)mouseLastTime=Time.time;
            if(Time.time - mouseLastTime > mouseTimeDeactivation)enableMouse = false;
        }else{
            if(Time.time - mouseLastTime > mouseTimeDeactivation){
                mouseLastTime = Time.time;
                mouseLastPos = mousePos;
            }
        }
        return enableMouse;
    }
}
