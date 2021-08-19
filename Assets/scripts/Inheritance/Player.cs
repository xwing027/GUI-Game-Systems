using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Stats
{
    public string playerName;

    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.tag == "Enemy") //if connecting with the enemy, you lose 10 health points
        {
            collision.GetComponent<LifeForce>().currentHealth -= 10;
        }
    }
}
