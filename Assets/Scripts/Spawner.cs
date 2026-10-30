using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    public TilemapRenderer tilemapRenderer;
    public Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Tilemap>().CompressBounds();
        if (tilemapRenderer==null)tilemapRenderer = GetComponent<TilemapRenderer>();
        bounds = tilemapRenderer.bounds;
        print(bounds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 Rand(Bounds b)=>new Vector3(Random.Range(b.min.x,b.max.x),Random.Range(b.min.y,b.max.y),Random.Range(b.min.z,b.max.z));
    public Enemy Spawn(Enemy enemy, Vector2 pos)=>Instantiate(enemy, pos, Quaternion.identity);
    public Enemy Spawn(Enemy enemy, Bounds b)=>Spawn(enemy, Rand(b));
    public Enemy Spawn(Enemy enemy)=>Spawn(enemy,bounds);
}
