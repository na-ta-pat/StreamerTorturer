using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealingRoom : MonoBehaviour
{
    public Collider2D healingArea;
    public float HPHealPerSecond = .5f;
    private bool isPlayerInside;
    float stayTime;
    void Start(){isPlayerInside=false;}
    void OnTriggerEnter2D(Collider2D other){if(other.TryGetComponent(out Player player))stayTime=0;}
    void OnTriggerStay2D(Collider2D other){
        if(other.TryGetComponent(out Player player)){
            stayTime+=Time.deltaTime;
            if(stayTime*HPHealPerSecond>1){
                player.Heal();
                stayTime=0;
            }
        }
    }
}