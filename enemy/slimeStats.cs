using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeStats : stats
{
    public bool split = false;
    public GameObject slimePrefab;
    public GameObject levelManagerObject;
    private levelManager manager;
    private Animator anim;
    private Rigidbody2D rb;
    private slimeAI AI;
    private slimeAttack aiAttck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AI = GetComponent<slimeAI>();
        aiAttck = GetComponentInChildren<slimeAttack>();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        if (split)//alters the size of slimes if they are the baby slimes
        {
            transform.localScale = new Vector2(transform.localScale.x - 2, transform.localScale.y - 2);
            
        }
    }


    public override void damage(float damage)
    {
        base.damage(damage);
    }

    public override void death()
    {
        manager = levelManagerObject.GetComponent<levelManager>();
        if (!split)//checks if slime is not baby slime
        {
            //spawn two smaller slimes
            GameObject childSlime1 = Instantiate(slimePrefab, new Vector3(transform.position.x - 1f, transform.position.y, 0), Quaternion.identity);
            GameObject childSlime2 = Instantiate(slimePrefab, new Vector3(transform.position.x + 1f, transform.position.y, 0), Quaternion.identity);
            var enemy1 = childSlime1.GetComponent<slimeStats>();
            var enemy2 = childSlime2.GetComponent<slimeStats>();
            enemy1.maxHealth = 50;
            enemy2.maxHealth = 50;
            enemy1.split = true;
            enemy2.split = true;

        }
        //disable healthbar
        StartCoroutine("deathCo");//kills adult slime
    }
    private IEnumerator deathCo()
    {
        AI.enabled = false;//disables AI
        anim.SetBool("spinning", false);//supposed to disable spinning animations. It doesn't. No idea why. I dislike programming animations
        anim.enabled = false;
        Destroy(aiAttck);//disabling AI apparently was not enough. Destroying it seems to prevent attacking from continuing.
        rb.velocity = new Vector2(0, 0);//stops slime from moving
        //begins melting slime
        float counter = 0;
        Vector2 startSize = transform.localScale;
        while(counter < 5)//lower this 
        {
            counter += Time.deltaTime;
            transform.localScale = Vector2.Lerp(startSize, new Vector2(0, 0), counter / 5);//needs speeding up
            yield return null;
        }
        Destroy(gameObject);//delete
    }
}
  
