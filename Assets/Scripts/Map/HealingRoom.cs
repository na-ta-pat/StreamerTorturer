using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealingRoom : MonoBehaviour
{
    public Collider2D healingArea;
    public float HPHealPerSecond = .5f;
    public GameObject healingEffect;
    public float healingEffectPerSecond;
    // private bool isPlayerInside;
    float healingEffectTime;
    float stayTime;
    // void Start(){isPlayerInside=false;}
    void OnTriggerEnter2D(Collider2D other){if(other.TryGetComponent(out Player player))stayTime=0;}
    void OnTriggerStay2D(Collider2D other){
        if(other.TryGetComponent(out Player player)){
            player.isHealing=true;
            stayTime+=Time.deltaTime;
            if(stayTime*HPHealPerSecond>1){
                player.Heal();
                stayTime=0;
            }
            healingEffectTime+=Time.deltaTime;
            if(healingEffectTime*healingEffectPerSecond>1){
                Instantiate(healingEffect,player.transform.position, Quaternion.identity);
                healingEffectTime=0;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){if(other.TryGetComponent(out Player player))player.isHealing=false;}

}