﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool poolCanExpand = true;
    
    public GameObject PoolContainer { get; set; } // --- NEW
    private List<GameObject> pooledObjects;
    // private GameObject parentObject; // --- DELETE THIS
    
    private void Start()
    {
        // parentObject = new GameObject("Pool"); // --- DELETE THIS
        PoolContainer = new GameObject($"Pool - {objectPrefab.name}"); // --- NEW
        Refill();
    }

    /// <summary>
    /// Creates our Pool
    /// </summary>
    public void Refill()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            AddObjectToPool();
        }
    }

    /// <summary>
    /// Return one object from our pool
    /// </summary>
    public GameObject GetObjectFromPool()
    {
        // Return one Object from the pool if we find one disable
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // If we need more objects, lets expand our pool
        if (poolCanExpand)
        {
            return AddObjectToPool();
        }

        return null;
    }

    /// <summary>
    /// Adds one object to the pool
    /// </summary>
    public GameObject AddObjectToPool()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        newObject.transform.parent = PoolContainer.transform; // --------
        
        pooledObjects.Add(newObject);
        return newObject;
    }
}
