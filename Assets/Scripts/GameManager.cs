using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager: MonoBehaviour{

    public static GameManager Instance { get; private set; }
    public Player player;
    public UI ui;
    public Enemy mockEnemy;
    public Controller controller;
    public Vector2 playerPos => player.transform.position;
    // public ChatGameplay chatGamePlay;
    float tmp;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) {
            Destroy(this); 
            return;
        }
         
        Instance = this; 
        DontDestroyOnLoad(this);
        
    }

    void Update(){
        
        if(!Input.GetKeyDown(KeyCode.L)){
            if(tmp<Time.time){
                Spawn(mockEnemy);
                tmp = Time.time+0.5f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Semicolon))
            SceneManager.LoadScene("DungeonRoom");
    }
    void Spawn(Enemy enemy){
        DungeonRoom[] room = FindObjectsByType<DungeonRoom>(FindObjectsSortMode.None);
        room[0].Spawn(enemy);
    }

}