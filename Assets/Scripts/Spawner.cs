using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    [SerializeField] SpawnnableObject[] objects;

    [SerializeField] float minSpawnRate = 1f;
    [SerializeField] float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void spawn()
    {
        float spawnChance = Random.value;
        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }
        Invoke(nameof(spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
