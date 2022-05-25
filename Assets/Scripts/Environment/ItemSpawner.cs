using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    void Start()
    {
        Instantiate(items[Random.Range(0,(items.Length - 1))], transform.position, transform.rotation);
    }
}
