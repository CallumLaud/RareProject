using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAttack :  MonoBehaviour
{
    private float damageAmount;
    public float attackDuration;
    public float attackCooldown;
    public bool canAttack = true;
    private Animator anim;
    private slimeStats stats;
    private void Start()
    {
        stats = GetComponentInParent<slimeStats>();
        damageAmount = stats.attackValue;
        anim = GetComponentInParent<Animator>();
        damageAmount = stats.attackValue;
        attackDuration = 4;
        attackCooldown = 1.5f;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canAttack && collision.gameObject.tag == "Player")//checks if collider is player and that cooldown is not active
        {
            Debug.Log("Attacking");
            IEnumerator attackRoutine = attackCo(collision);
            StartCoroutine(attackRoutine);
            
        }
        
    }

    private IEnumerator preAttack()
    {
        yield return new WaitForSeconds(2f);
        //animation preparing
    }

    private IEnumerator attackCo(Collider2D other)
    {
        
        if (other.GetComponent<IDamage>() != null)
        {
            anim.SetBool("spinning", true);//activates spinning animation
            other.GetComponent<IDamage>().damage(damageAmount);//damages collider
            yield return new WaitForSeconds(attackDuration);//attack keeps going for duration of attackDuration. decrease value
            anim.SetBool("spinning", false);//disables animation
            StartCoroutine("attackCooldownCo");//cannot attack for length of cooldown

        }

    }

    private IEnumerator attackCooldownCo()
    {
        canAttack = false;//cooldown activated
        Debug.Log("Cooldown activated");
        yield return new WaitForSeconds(attackCooldown);//cooldown duration
        canAttack = true;
    }
}
