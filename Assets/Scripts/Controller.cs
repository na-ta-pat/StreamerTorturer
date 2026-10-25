using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Vector2 movement;
    public Vector2 looking;
    public bool attack;
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

        var gamepad = Gamepad.current; //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/Gamepad.html
        if (gamepad!=null){
            movement = gamepad.leftStick.ReadValue();
            looking = gamepad.rightStick.ReadValue();
            attack = gamepad.buttonWest.isPressed;
            return;
        }

        var keyboard = Keyboard.current;
        if (keyboard!=null){
            attack = keyboard.kKey.isPressed;
        }

        var mouse = Mouse.current;
        if (mouse!=null && enableMouse){
            
        Vector2 mousePos = mouse.position.ReadValue();

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, 10f)
        );
            looking = (Vector2)  (worldPos - GameManager.Instance.player.transform.position);
        }
    }
}
