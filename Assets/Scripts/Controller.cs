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
        } else if (keyboard!=null){
            attack = keyboard.kKey.isPressed;
            switchWeapon = keyboard.qKey.isPressed;
        } else if (mouse!=null && enableMouse){
            Vector2 mousePos = mouse.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(mousePos.x, mousePos.y, 10f)
            );
            looking = (Vector2)  (worldPos - GameManager.Instance.player.transform.position);
        }
    }
}
