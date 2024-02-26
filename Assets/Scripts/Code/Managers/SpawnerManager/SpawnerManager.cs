using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] SpawnerData[] Spawners = new SpawnerData[0];
    [SerializeField] Vector2 SpawnArea = Vector2.zero;
    private int CurrentSpawnIndex = 0;
    private float SpawnDelay;

    private void Awake()
    {
        CurrentSpawnIndex = 0;
    }

    private void FixedUpdate()
    {
        //Change Spawner
        if (Spawners[CurrentSpawnIndex].SpawnEndDistance - transform.position.x <= 0f)
        {
            CurrentSpawnIndex++;
            Debug.Log("NewSpawner: " + CurrentSpawnIndex);
            SpawnDelay = Spawners[CurrentSpawnIndex].DelayBetweenSpawn;
        }
        //Decrease timers
        SpawnDelay -= Time.fixedDeltaTime;
        //Spawn
        if (SpawnDelay <= 0f)
        {
            Spawn();
            SpawnDelay = Spawners[CurrentSpawnIndex].DelayBetweenSpawn;
        }

    }


    private void Spawn()
    {
        //get spawn position
        Vector3 spawnPosition = Vector3.zero;
        spawnPosition.x = UnityEngine.Random.Range(-SpawnArea.x/2, SpawnArea.x/2);
        spawnPosition.y = UnityEngine.Random.Range(-SpawnArea.y/2, SpawnArea.y/2);
        
        //Instanciate
        Instantiate(Spawners[CurrentSpawnIndex].ObjectToSpawn, spawnPosition + transform.position, Quaternion.identity);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Spawners[CurrentSpawnIndex].GizsmoColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnArea.x, SpawnArea.y, 0f));

        for(int i = 0; i < Spawners.Length; i++)
        {
            Gizmos.color = Spawners[i].GizsmoColor;
            Gizmos.DrawSphere(transform.position + (Spawners[i].SpawnEndDistance * Vector3.right), 2f);
        }

    }
}
