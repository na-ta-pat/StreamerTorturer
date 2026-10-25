using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager: MonoBehaviour{

    public static GameManager Instance { get; private set; }
    public Player player;
    public UI ui;
    public Enemy mockEnemy;

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
        
        if(Input.GetKeyDown(KeyCode.L))Spawn(mockEnemy);

        if (Input.GetKeyDown(KeyCode.Semicolon))
            SceneManager.LoadScene("DungeonRoom");
    }
    void Spawn(Enemy enemy){
        Vector2 position = new Vector2(0,0); 
        Instantiate(enemy, position, Quaternion.identity);
    }

}