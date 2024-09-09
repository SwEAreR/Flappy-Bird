using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float minHeight = -1f;
    [SerializeField] private float maxHeight = 2f;
    public Queue<GameObject> pipesPool = new Queue<GameObject>();
    private int defaultCount = 8;

    private void Start()
    {
        Init();
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }
    
    private void Spawn()
    {
        GameObject pipe = GetPipesFromPool();
        pipe.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }

    private void Init()
    {
        for (int i = 0; i < defaultCount; i++)
        {
            var obj = Instantiate(prefab, this.transform);
            pipesPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    private GameObject GetPipesFromPool()
    {
        GameObject pipe;
        if (pipesPool.Count > 0)
        {
            pipe = pipesPool.Dequeue();
            pipe.SetActive(true);
        }
        else
        {
            pipe = Instantiate(prefab, this.transform);
        }
        pipe.transform.localPosition = Vector3.zero;
        return pipe;
    }
    
    public void CollectPipes(GameObject pipe)
    {
        if (!pipesPool.Contains(pipe))
        {
            pipesPool.Enqueue(pipe);
            pipe.SetActive(false);
        }
    }
}
