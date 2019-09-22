using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void RespawnOnScene(Scene scene)
    {
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }
}
