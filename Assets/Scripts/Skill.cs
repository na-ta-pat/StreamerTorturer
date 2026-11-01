using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// skill use enum ChatType in Chat.cs



public class Skill : MonoBehaviour
{
    public ChatType chatType;
    public float cooldown;
    public bool isReady=>lastUse+cooldown<Time.time;
    public float precReady=>Mathf.Max((Time.time-lastUse)/cooldown,1);
    float lastUse;
    void Start(){
        lastUse = Time.time;
    }
    public virtual bool Call(){
        if(!isReady)return false;
        lastUse = Time.time;
        return true;
    }
}