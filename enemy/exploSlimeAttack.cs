using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exploSlimeAttack : MonoBehaviour
{
    private exploSlimeStats slimeStats;
    private void Start()
    {
        slimeStats = GetComponentInParent<exploSlimeStats>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            slimeStats.StartCoroutine("blowUpCO");//begins death explosion
        }
    }



}
