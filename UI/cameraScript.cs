using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate()
    {
        Follow();//fixed update to ensure follow script is smooth
    }

    void Follow()
    {
        Vector3 playerPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, playerPos, smoothFactor * Time.fixedDeltaTime);//linearly interpolates to player
        transform.position = playerPos;
    }
}
