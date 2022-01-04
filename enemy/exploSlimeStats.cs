using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exploSlimeStats : stats
{
    public GameObject levelManagerObject;
    public GameObject explosionAnim;
    private levelManager manager;
    public float explosionRadius;
    public float explosionDamage;

    private bool blowingUp = false;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float alpha;
    private slimeAI AI;
    private Collider2D[] withinExplosion;
    [SerializeField]private LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<slimeAI>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }
    void Update()
    {

    }

    public override void damage(float damage)
    {
        base.damage(damage);//inherits from base stats
    }

    public override void death()
    {
        StartCoroutine("blowUpCO");
    }

    IEnumerator blowUpCO()
    {
        //forewarn of explosion to player
        rb.velocity = new Vector2(0, 0);//stops slime from moving
        AI.enabled = false;//disables AI
        blowingUp = true;

        yield return new WaitForSeconds(5f);

        //checks for player within explosion 
        withinExplosion = Physics2D.OverlapCircleAll(transform.position, 6, targetLayer);
        foreach (BoxCollider2D collider in withinExplosion)//iterates over every collider contained in explosion
        {
            var player = collider.GetComponent<playerStats>();//checks every collider for playerstats script

            if (player)
            {
                //calculating amount of damage to inflict based on position within explosion
                float distance = Vector2.Distance(player.transform.position, transform.position);
                collider.GetComponent<IDamage>().damage(explosionDamage + distance / explosionRadius * 10);//this needs altering. Probably should just be single value for simplicity sake
                Debug.Log($"contained within explosion. took {distance/explosionRadius * 100} damage");

            }
        }
        //creates explosion
        Instantiate(explosionAnim, transform.position, Quaternion.identity);//need to delete once instantiated with script
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()//debugging purposes
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }

}
