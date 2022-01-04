using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour, IDamage
{
    public healthBarScript healthBar;
    public float currentHealth;
    public float maxHealth;
    public float iFrames;
    public float attackValue;
    public int scoreValue;
    public bool isInvincible;

    public virtual void damage(float amount)//damages those that inherit from stats
    {
        if (isInvincible == false)//Checks if Iframes are still active
        {
            //alters health and health bar
            currentHealth -= amount;
            healthBar.setHealth(currentHealth);

            if (currentHealth <= 0)//kills character
            {
                death();
            }
            else
            {
                StartCoroutine("invincibleCo");//activates Iframes co-routine
            }
            
        }
    }
    public virtual void heal(float amount)//healths those that inherit from stats
    {
        if(currentHealth + amount > maxHealth)//prevents heal func from exceeding max health
        {
            currentHealth = maxHealth;
            healthBar.setHealth(currentHealth);
        }
        else
        {
            currentHealth += amount;
            healthBar.setHealth(currentHealth);
        }
    }

    public virtual void death()//death func for classes that inherit from stats
    {

    }
    IEnumerator invincibleCo()
    {
        isInvincible = true;
        yield return new WaitForSeconds(iFrames);//could be something different than WaitForSeconds
        isInvincible = false;
    }
}
