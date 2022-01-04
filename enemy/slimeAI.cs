using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAI : MonoBehaviour
{
    
    public LayerMask aggroMask;
    public float speed;
    public float attackRange;
    public GameObject hitbox;
    public float maxWayDistance;
    public float range;

    private Rigidbody2D rb;
    private Collider2D[] withinAggroColliders;
    private Transform target;
    private float distancFromTarget;
    private bool facingRight;
    private bool facingLeft;
    private Vector2 wayPoint;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        setNewWayPoint();
        wander();
        
    }

    // Update is called once per frame
/*    void Update()
    {
        chase();
    }*/

    private void FixedUpdate()
    {
        distancFromTarget = Vector2.Distance(target.position, transform.position);
        withinAggroColliders = Physics2D.OverlapCircleAll(transform.position, 6, aggroMask);
        if(withinAggroColliders.Length > 0 && distancFromTarget > attackRange)
        {
            chase();
        }
        else if (withinAggroColliders.Length <= 0 && distancFromTarget > attackRange)
        {
            wander();
        }
        else if (withinAggroColliders.Length > 0 && distancFromTarget < attackRange)
        {
            attack();
            
        }
    }



    private void chase()
    {
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector2(5, 5);
            facingLeft = false;
            facingRight = true;
            rb.velocity = new Vector2(speed, 0);
        }
        else if(transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(-5, 5);
            facingRight = false;
            facingLeft = true;
            rb.velocity = new Vector2(-speed, 0);
        }

    }

/*    private void stopChasing()
    {
        Debug.Log("Stopped chasing");
        wander();
        
    }*/
    
    //need to make function for attacking
    private void attack()
    {
        
        Collider2D collider = hitbox.GetComponent<BoxCollider2D>();
        collider.enabled = true;
    }
    
    private void wander()
    {
        Vector2 movePosition = transform.position;
        movePosition.x = Mathf.MoveTowards(transform.position.x, wayPoint.x, speed * Time.deltaTime);
        rb.MovePosition(movePosition);
        if (transform.position.x < movePosition.x) transform.localScale = new Vector2(5, 5);
        else if (transform.position.x > movePosition.x) transform.localScale = new Vector2(-5, 5);
        if(Vector2.Distance(transform.position, wayPoint) < range)
        {
            setNewWayPoint();
            
        }
        
    }
    private void setNewWayPoint()
    {
        wayPoint = new Vector2(Random.Range(-maxWayDistance, maxWayDistance), transform.position.y);

    }

/*    private IEnumerator wait()
    {
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(5f);
    }
*/
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 6);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
