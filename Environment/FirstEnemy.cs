using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gameObjects;
    public GameObject player;
    public bool dead = false;
    private bool onlyone = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dead == true && onlyone)
        {
            Death();
            dead = false;
            onlyone = false;
        }
    }
    private void Death()
    {
        player.GetComponent<hero1>().Starting();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }
}
