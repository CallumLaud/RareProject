using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovment : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    BoxCollider2D box;
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sRender;

    public float speed = 5.0f;
    public float jumpPower = 20.0f;
    float horizontal;

    bool attacking;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        sRender = GetComponent<SpriteRenderer>();
        
    }
    void Update()
    {
        

        float x = Input.GetAxisRaw("Horizontal");//gets keys associated with horizontal
        float move = x * speed;
        rb.velocity = new Vector2(move, rb.velocity.y);
        if(x == 0 )
        {
            anim.SetBool("Running", false);//triggers idle

        }
        else if(x > 0)
        {
            transform.localScale = new Vector2(1, 1);//flips game-object to turn left
            anim.SetBool("Running", true);//triggers animation
 
        }
        else if(x < 0)
        {
            transform.localScale = new Vector2(-1, 1);//flips game-object to turn right
            anim.SetBool("Running", true);//triggers animation
        }
        jump();//checks for jump
    }

    void jump()
    {
        if (groundCheck())
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //continues to run whilst jumping or just gets stuck in jumping animation. Struggling with anims alot
                anim.SetBool("Running", false);
                anim.ResetTrigger("noJump");
                anim.SetTrigger("jump");

                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }
    bool groundCheck()
    {
        float testHeight = .01f;
        RaycastHit2D rayCastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, testHeight, mask);//uses raycast to check for ground tiles
        Color raycolor;
        if(rayCastHit.collider != null)//checks if hitting ground
        {
            //darn animations
            anim.ResetTrigger("jump");
            anim.SetTrigger("noJump");
            raycolor = Color.green;
        }
        else
        {
            raycolor = Color.red;
        }

        return rayCastHit.collider != null;
    }

    void attack1()//basic attack
    {
        if (Input.GetKeyDown(KeyCode.E) && attacking == false)
        {
            attacking = true;
            anim.SetTrigger("Attack1");
            StartCoroutine("meleeCooldown");
        }
    }

    IEnumerator meleeCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        anim.ResetTrigger("Attack1");

    }
}
