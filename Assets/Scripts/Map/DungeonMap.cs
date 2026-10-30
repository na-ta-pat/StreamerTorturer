using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class RoomRow{
//     public DungeonRoom[] rooms;
// }
public class DungeonMap : MonoBehaviour
{
    public DungeonRoom currentRoom;
    public DungeonRoom[] allRooms;
    public int width,height; // pixel of each cell
    public RoomTrigger[] roomTrigger;
    public Dictionary<RoomTrigger,RoomTrigger> triggerMap = new();
    // public RoomRow[] rooms;

    void OnValidate(){
        allRooms = FindObjectsByType<DungeonRoom>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        if(allRooms.Length>0)currentRoom = allRooms[0]; //todo
    }

    void Start(){
        roomTrigger = FindObjectsByType<RoomTrigger>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        fillTriggerMap();
    }

    void fillTriggerMap(){
        foreach(var a in roomTrigger){
            float m = 999999;
            RoomTrigger mr = null;
            foreach(var b in roomTrigger){
                if(a.transform.parent == b.transform.parent)continue;
                float d = (a.transform.position - b.transform.position).magnitude;
                if(d<m){
                    m=d; mr = b;
                }
            }
            triggerMap[a] = mr;
        }
    }
    
    public void roomTriggerEnter(RoomTrigger roomTrigger){
        // when player step on some trigger: it will trigger the closest trigger
        triggerMap[roomTrigger].transform.parent.gameObject.SetActive(true);
    }
    public void roomTriggerExit(RoomTrigger roomTrigger){
        DungeonRoom room = findRoom(GameManager.Instance.player.transform.position);
        DungeonRoom triggerRoom = roomTrigger.transform.parent.GetComponent<DungeonRoom>();
        if(room != triggerRoom)triggerRoom.gameObject.SetActive(true);
    }

    public DungeonRoom findRoom(Vector2 pos){
        foreach(var r in allRooms){
            if(r.bounds.Contains(pos))return r;
        }return null;
    }


    // public DungeonRoom getRoom(Vector2 pos){
    //     var origin = transform.position;
    //     int x = Mathf.FloorToInt((pos.x-origin.x)/width);
    //     int y = Mathf.FloorToInt((pos.y-origin.y)/height);
    //     return getRoom(x,y);
    // }
    // public bool isRoomInRange(int x, int y){return rooms.Length>x && rooms[x].rooms.Length>y;}
    // public DungeonRoom getRoom(int x, int y){return isRoomInRange(x,y)?rooms[x].rooms[y]:null;}

}