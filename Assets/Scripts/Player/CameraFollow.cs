using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTrans;

    
    public Vector3 offset;         

    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;      
    private Vector3 maxBounds;     



    void Start()
    {
        playerTrans = Player.instance.GetComponent<Transform>(); 
                                                                 
        offset = transform.position - playerTrans.position;

        minBounds = boundBox.bounds.min;    //Gets the minimum x and y for the bounds
        maxBounds = boundBox.bounds.max;    // Gets the maximum x and y for the bounds

        //Gets the width and height of the camera view
        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    void LateUpdate()
    {
        if (playerTrans != null)
        {

            float clampedX = Mathf.Clamp(playerTrans.position.x + offset.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(playerTrans.position.y + offset.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
