using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject respawnPoint; //TODO: make more dynamic

    [HideInInspector]
    public Player player;
    //make this instance static so it can be used across scripts
    public static GameManager instance = null;

    private bool respawn_Input = false;

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
            Destroy(gameObject);
            Debug.LogWarning("Another instance of GameManager have been created and destoryed!");
        }

        //Makes the gameobject not be unloaded when entering a new scene
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        PlayerRespawn();
    }

    void Update()
    {
        CheckInput();

        //If player presses respawn while still alive, respawn
        if (respawn_Input)
        {
            PlayerRespawn();
        }
        
    }

    public void PlayerDied()
    {
        //show some text, blah, blah
    }

    public void PlayerRespawn()
    {
        player.Respawn(respawnPoint.transform.position);                
    }

    private void CheckInput()
    {
        respawn_Input = Input.GetButtonDown("Respawn");
    }

}
