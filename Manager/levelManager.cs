using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    //used to spawn enemies

    public GameObject[] enemyTypes;//could cache enemies
    private GameObject[] spawnPoints;//needed to store the locations where enemies can spawn

    public int enemyCounter = 0;
    private bool spawning;

    void Start()
    {

    }

    private void Update()
    {
        GameObject[] enemyCount = GameObject.FindGameObjectsWithTag("character");//constantly checking for enemies
        if(enemyCount.Length == 0 && !spawning)//when there are no enemies in the level and the spawnGenerator is not activated, spawn enemies
        {
            Debug.Log("spawning");
            StartCoroutine("spawn");
            
        }
    }

    public void spawnGenerator()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");//obtains all spawnpoints in the level
        //iterate through enemy types, spawning a random number of each enemy types
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            Debug.Log("First iteration");
            int enemyTypeRand = Random.Range(0, enemyTypes.Length);//range of 0-1
            for(int j = 0; j < Random.Range(1, 5); j++)//spawns random number of enemies
            {
                Debug.Log("Spawning iteration");
                Debug.Log($"Spawnpoints Length: {spawnPoints.Length - 1}");
                int spawnPointsRand = Random.Range(0, spawnPoints.Length - 1);//spawnpoint to spawn at
                Vector3 position = spawnPoints[spawnPointsRand].gameObject.transform.position;//generates position
                Instantiate(enemyTypes[enemyTypeRand], position, Quaternion.identity);//spawns enemy at spawn location
            }
            
            
        }
    }
    IEnumerator spawn()
    {
        spawning = true;
        yield return new WaitForSeconds(1f);
        spawnGenerator();
        spawning = false;
    }
}
