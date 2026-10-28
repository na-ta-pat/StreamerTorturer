using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonRoom : MonoBehaviour
{
    // public bool isPlayerInside=>;
    public Spawner[] spawners;
    public void Spawn(Enemy enemy){
        int index = Random.Range(0,spawners.Length);
        Spawn(enemy,index);
    }
    public void Spawn(Enemy enemy, int index){
        spawners[index].Spawn(enemy);
    }
    void OnValidate(){
        spawners = GetComponentsInChildren<Spawner>(true);
    }
}