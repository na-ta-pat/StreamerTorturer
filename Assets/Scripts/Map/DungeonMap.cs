using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomRow{
    public DungeonRoom[] rooms;
}
public class DungeonMap : MonoBehaviour
{
    public int width,height; // pixel of each cell
    public RoomRow[] rooms;
    public DungeonRoom getRoom(Vector2 pos){
        var origin = transform.position;
        int x = Mathf.FloorToInt((pos.x-origin.x)/width);
        int y = Mathf.FloorToInt((pos.y-origin.y)/height);
        return getRoom(x,y);
    }
    public bool isRoomInRange(int x, int y){return rooms.Length>x && rooms[x].rooms.Length>y;}
    public DungeonRoom getRoom(int x, int y){return isRoomInRange(x,y)?rooms[x].rooms[y]:null;}

}