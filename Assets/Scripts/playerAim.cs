using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAim : MonoBehaviour
{
    Joystick joyStickAim;
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    float fireRate = 0.5f;
    private float lastShot = 0.0f;
    private float health = 100f;

    void Start()
    {
        joyStickAim = GameObject.FindGameObjectWithTag("JoyStickAim").GetComponent<Joystick>();
    }


    void Update()
    {
        RotateFirePlayer();
    }

    void RotateFirePlayer()
    {
        Vector3 joystickDirection = new Vector3(joyStickAim.Horizontal, joyStickAim.Vertical, 0);
        if (joystickDirection.sqrMagnitude > 0.01)
        {
            float angle = Mathf.Atan2(joystickDirection.x, joystickDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            if (Time.time > fireRate + lastShot)
            {
                FireProjectile(spawnPoint.position, projectilePrefab);
                lastShot = Time.time;
                setHealth(5);
            }
        }
    }

    public void setHealth(float value)
    {
        health -= value;
        GameManager.instance.setHealthBar(health / 100);
        Debug.Log(health);
    }

    void FireProjectile(Vector3 spawnPoint, GameObject projectileObj)
    {
        GameObject projectile = Instantiate(projectileObj, spawnPoint, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
    }
}