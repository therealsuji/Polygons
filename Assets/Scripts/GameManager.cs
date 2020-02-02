using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public  GameObject player;

    public  Image healthBar;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        

    }

   public  void setHealthBar(float amount)
    {
        healthBar.fillAmount = amount;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
