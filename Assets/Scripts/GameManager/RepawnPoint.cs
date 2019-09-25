using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct RepawnCords
{
    public RepawnCords(Vector3 loc, Scene s)
    {
        location = loc;
        scene = s;
    }

    public Vector3 location;
    public Scene scene;
}

public class RepawnPoint : MonoBehaviour
{
    public bool spawnPointSet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !spawnPointSet)
        {
            spawnPointSet = true;
            GameManager.instance.SetRepawnPoint(new RepawnCords(gameObject.transform.position, 
                                                GameManager.instance.nextSceneHelper.GetCurrentScene()));
        }
    }
}
