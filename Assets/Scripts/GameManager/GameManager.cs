using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject firstRespawnPoint;    
    public NextSceneHelper nextSceneHelper;
    public static GameManager instance = null;       //make this instance static so it can be used across scripts
    [HideInInspector]
    public Player player;
    [SerializeField]
    public RepawnCords currentRepawn;

    private void Awake()
    {
        //Set the instance only once.
        if (instance == null)
        {           
            instance = this;
        }
        else if (instance != this)
        {            
            //Enforces that there will always be one instance of a gameObject. This is for type errors prevention
            Destroy(this.gameObject);
            Debug.LogWarning("Another instance of GameManager have been created and destoryed!");
        }

        //Makes the gameobject not be unloaded when entering a new scene
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        currentRepawn = new RepawnCords(firstRespawnPoint.transform.position,
                                        nextSceneHelper.GetCurrentScene());
    }

    public void SetRepawnPoint(RepawnCords rP)
    {
        currentRepawn = rP;
    }

    public void PlayerDied()
    {
        //show some text, blah, blah
    }

    public void PlayerRespawn()
    {
        nextSceneHelper.RespawnOnScene(currentRepawn.scene);
        player.Respawn(currentRepawn.location);
        
    }

}
