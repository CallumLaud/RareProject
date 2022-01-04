using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPotion : items
{
    public float toHeal;

    public override void onUse(Collider2D other)
    {
       playerStats statsReference;
       statsReference = other.GetComponent<playerStats>();
       if (statsReference.currentHealth < statsReference.maxHealth)//checks if the item should be used
       {
           statsReference.heal(toHeal);//heals 
           Destroy(gameObject);//deletes object
       }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.GetComponent<playerStats>() != null)//only works on player
        {
            onUse(collision);//activats item use func
        }
    }

}
