using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : stats
{

    // Start is called before the first frame update
    void Start()
    {
        //sets max health
        currentHealth = maxHealth;
        healthBar.setMaxHealth(currentHealth);
    }

    public override void damage(float amount)
    {
        base.damage(amount);//inherits from parent stats
    }

    public override void heal(float amount)
    {
        base.heal(amount);//inherits from parent stats
    }

    public override void death()
    {
        //death code here
    }


}
