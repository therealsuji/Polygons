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
    private AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip hitClip;
    public bool dead = false;
    void Start()
    {
        joyStickAim = GameObject.FindGameObjectWithTag("JoyStickAim").GetComponent<Joystick>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        RotateFirePlayer();
        deathCheck();
    }

    void RotateFirePlayer()
    {
        Vector3 joystickDirection = new Vector3(joyStickAim.Horizontal, joyStickAim.Vertical, 0);
        if (joystickDirection.sqrMagnitude > 0.01 && !dead)
        {
            float angle = Mathf.Atan2(joystickDirection.x, joystickDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            if (Time.time > fireRate + lastShot)
            {
                audioSource.clip = shootClip;
                audioSource.Play();
                FireProjectile(spawnPoint.position, projectilePrefab);
                lastShot = Time.time;
                setHealth(-5);
            }
        }
    }

    public void setHealth(float value)
    {
        health += value;
        GameManager.instance.setHealthBar(health / 100);
    }

    void FireProjectile(Vector3 spawnPoint, GameObject projectileObj)
    {
        GameObject projectile = Instantiate(projectileObj, spawnPoint, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
    }

    void deathCheck()
    {
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject,1000);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            audioSource.clip = hitClip;
            audioSource.Play();
            setHealth(-20);
        }
        else if (other.tag == "shard")
        {
            setHealth(2);
        }
    }
}