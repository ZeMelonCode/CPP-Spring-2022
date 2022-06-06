using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float Hp;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.hp = Hp;
        GameManager.instance.SpawnPlayer(spawnPoint.position);
        GameManager.instance.currentLevel = this;
    }

   
}
