using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("destroyCo");
    } 

    IEnumerator destroyCo()//deletes once neccessary
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
