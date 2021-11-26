using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawnPrefab;
    private EnemyInfo enemyInfo;
    [SerializeField] private int maxNumber;
    [SerializeField] private float respawnRate = 5f;
    [SerializeField] private List<GameObject> currentEnemyList;
    private bool isSpawning = false;
    private bool isCleaningList = false;

    private void Awake()
    {
        enemyInfo = enemyToSpawnPrefab.GetComponent<EnemyController>().enemyInfo;
    }

    private void Update()
    {
        if (currentEnemyList.Count < maxNumber && !isSpawning)
        {
            StartCoroutine(SpawnEnemy());
        }

        if (!isCleaningList)
            StartCoroutine(RemoveNullsFromList());
    }


    private IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        GameObject go = Instantiate(enemyToSpawnPrefab, transform.position, Quaternion.identity);
        currentEnemyList.Add(go);
        yield return new WaitForSeconds(respawnRate);
        isSpawning = false;
    }

    private IEnumerator RemoveNullsFromList()
    {
        isCleaningList = true;
        for (var i = currentEnemyList.Count - 1; i > -1; i--)
        {
            if (currentEnemyList[i] == null)
                currentEnemyList.RemoveAt(i);
        }
        yield return new WaitForSeconds(10);
        isCleaningList = false;
    }


}
