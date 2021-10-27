using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : EnemyMovement
{
    public void BiteAttack()
    {
        //0-20, int Random.Range are exclusive
        int critChance = Random.Range(0,21);
        float critDamage = 0;
        if (critChance >= 17) //if it lands 17 or above it will be a critical hit
        {
            critDamage = Random.Range(baseDamage/2, baseDamage*difficulty);
        }
        player.GetComponent<PlayerHandler>().DamagePlayer(baseDamage*difficulty+critDamage);
    }
}
