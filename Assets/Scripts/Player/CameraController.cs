using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Camera playerCam;
    public BoxCollider2D boundBox;

    public float camOffset;

    private float halfHeight, halfWidth;

    private void Awake()
    {
        halfHeight = playerCam.orthographicSize;
        halfWidth = halfHeight * playerCam.aspect;

        boundBox = CameraBounds.instance.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && playerCam != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundBox.bounds.min.x + halfWidth, boundBox.bounds.max.x - halfWidth),
                Mathf.Clamp(player.transform.position.y, boundBox.bounds.min.y + halfHeight, boundBox.bounds.max.y - halfHeight),
                -camOffset);
        }
    }
}
