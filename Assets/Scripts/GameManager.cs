using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager: MonoBehaviour{

    public string channelName;
    public bool testWithoutChat;

    public int FPS = 60;
    public static GameManager Instance { get; private set; }
    public Player player;
    public UI ui;
    public Enemy mockEnemy;
    public Controller controller;
    public Vector2 playerPos => player.transform.position;
    public Camera uiCam;
    public Camera mainCam;
    public UI uiPrefab;
    public TwitchChat chat;
    public DungeonMap map;
    public DungeonRoom currentRoom => map.currentRoom;
    public List<ChatType> availableSkill;
    // public Spawner currentSpawner => currentRoom.spawner;
    
    float tmp;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) {
            Destroy(this); 
            return;
        }
         
        Instance = this; 
        DontDestroyOnLoad(this);

        mainCam = Camera.main;

        uiCam = mainCam.transform.Find("UI Cam").GetComponent<Camera>();

        ui = FindFirstObjectByType<UI>(); // support when ui canvas presesnt inside the scene
        if(ui==null)ui = Instantiate(uiPrefab, new Vector2(0,0), Quaternion.identity);
        

        // Limit framerate to cinematic ...fps. https://docs.unity.com/en-us/engine/6000.0/script-reference/unityengine/application/targetframerate
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        //If vSyncCount == 1, rendering is synchronized to the vertical refresh rate of the display.
        Application.targetFrameRate = FPS;
        
    }

    void Start(){
        chat.channel = channelName;
        if(!testWithoutChat)chat.Connect();
        ui.UpdateMaxHP(player.maxHP);
        ui.UpdateHP(player.HP);
    }

    void Update(){
        
        if(testWithoutChat && !Input.GetKeyDown(KeyCode.L)){
            if(tmp<Time.time){
                Spawn(mockEnemy);
                tmp = Time.time+0.5f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Semicolon))
            SceneManager.LoadScene("DungeonRoom");
    
        var summon = chat.summondb.Pop();
        if(!testWithoutChat && summon!=null)Spawn(summon);
        // else print("wrong command");
    }
    void Spawn(Enemy enemy){
        currentRoom.Spawn(enemy);
    }

}