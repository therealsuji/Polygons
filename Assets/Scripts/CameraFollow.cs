using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform player;
    Vector3 offsetToPlayer;
    public float smoothVal;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offsetToPlayer = transform.position - player.position;

    }

    void LateUpdate()
    {
        Vector3 newPos = player.position + offsetToPlayer;
        transform.position = Vector3.Lerp(transform.position, newPos, smoothVal);
    }
}
