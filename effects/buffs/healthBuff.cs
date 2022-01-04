using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBuff : effect
{
    public playerStats stats;
    void Start()
    {
        stats = GetComponentInParent<playerStats>();//reference for health
    }

    private IEnumerator healthBuffCo()
    {
        if(stats != null)//check if component exists
        {
            //doubles default health
            stats.maxHealth = 200;
            stats.healthBar.setMaxHealth(200);

            yield return new WaitForSeconds(duration);//time until buff ends

            if(stats.currentHealth > 100)//sets max health to default health if current health still greater than default max health
            {
                stats.maxHealth = 100;
                stats.healthBar.setMaxHealth(100);
                Destroy(this);//remove buff
            }
            else
            {
                Destroy(this);//no action needed other than remove buff
            }
        }
        else//if the component was not found, then do nothing but delete self
        {
            yield return null;
            Destroy(this);
        }
    }
}
