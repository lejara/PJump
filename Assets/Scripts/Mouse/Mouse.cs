using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public static Mouse instance = null;
    

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
            Debug.LogWarning("Another instance of Mouse have been created and destoryed!");
        }

        //Makes the gameobject not be unloaded when entering a new scene
        DontDestroyOnLoad(this);

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouseCursorUpdate();
    }

    void MouseCursorUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }
}
