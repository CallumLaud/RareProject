using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    Animator anim;
    bool attacking;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        attack1();
    }
    void attack1()
    {
        if (Input.GetKeyDown(KeyCode.E) && attacking == false)//prevents spamming
        {
            attacking = true;
            anim.SetTrigger("Attack1");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);//stores colliders of everything in hitbox

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log($"Hitting: {enemy.name}");
                if(enemy.GetComponent<stats>() != null)//checks if collider has stats 
                {
                    enemy.GetComponent<stats>().damage(50);//damages anything with stats
                }
            }

            StartCoroutine("meleeCooldown");//attack cooldown
        }
    }
    private void OnDrawGizmosSelected()//debugging purposes
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    IEnumerator meleeCooldown()//cooldown coroutine
    {
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        anim.ResetTrigger("Attack1");

    }
}
