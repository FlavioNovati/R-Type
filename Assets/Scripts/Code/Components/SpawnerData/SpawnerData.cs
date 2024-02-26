using UnityEngine;

[System.Serializable]
public class SpawnerData
{
    [SerializeField] public Transform ObjectToSpawn;
    [SerializeField] public float DelayBetweenSpawn;
    [SerializeField] public float SpawnEndDistance;
    [SerializeField] public Color GizsmoColor;
}
