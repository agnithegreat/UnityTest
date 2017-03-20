using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject[] prefabs;
    public int precreated;
    public bool growable;

    private List<GameObject> _pool;

    void Start()
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < precreated; i++)
        {
            Create(i % prefabs.Length);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            GameObject instance = _pool[i];
            if (!instance.activeInHierarchy)
            {
                return instance;
            }
        }

        if (growable)
        {
            return Create(Random.Range(0, prefabs.Length));
        }
        return null;
    }

    private GameObject Create(int id)
    {
        GameObject concrete = prefabs[id];
        GameObject instance = Instantiate(concrete);
        instance.SetActive(false);
        _pool.Add(instance);
        return instance;
    }

    public GameObject Create()
    {
        int rand = Random.Range(0, prefabs.Length);
        return Instantiate(prefabs[rand]);
    }
}
