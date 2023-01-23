using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_sc : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject[] bonusPrefabs;

    [SerializeField]
    private GameObject enemyContainer;

    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning() {
        StartCoroutine( SpawnEnemyRoutine() );
        StartCoroutine( SpawnBonusRoutine() );
    }

    public void OnPlayerDeath() {
        stopSpawning = true;
    }

    IEnumerator SpawnEnemyRoutine() {
        while (stopSpawning == false) {
            Vector3 position = new Vector3(Random.Range(-12f, 12f), 7.3f, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnBonusRoutine() {
        while (stopSpawning == false) {
            Vector3 position = new Vector3(Random.Range(-12f, 12f), 7.3f, 0);
            int randomBonusId = Random.Range(0,10);
            if (randomBonusId >= 0 && randomBonusId <= 2) {
                Instantiate(bonusPrefabs[randomBonusId], position, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }
}
