using UnityEngine;


public class GameManager: MonoBehaviour{

    public static GameManager Instance { get; private set; }
    public Player player;
    public Enemy mockEnemy;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
            Destroy(this); 
        else 
            Instance = this; 
    }

    void Update(){
        
        if(Input.GetKeyDown(KeyCode.L))Spawn(mockEnemy);
    }
    void Spawn(Enemy enemy){
        Vector2 position = new Vector2(0,0); 
        Instantiate(enemy, position, Quaternion.identity);
    }

}