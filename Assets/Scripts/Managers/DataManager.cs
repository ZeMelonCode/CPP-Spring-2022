using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataManager
{
    public float hp;
    public int lives;
    public float[] position;

    public int checkPoint;

    public DataManager(GameManager manager)
    {
        hp = manager.hp;
        lives = manager.lives;
        checkPoint =manager.checkPoint;

        position = new float[3];
        position[0] = manager.playerInstance.transform.position.x;
        position[1] = manager.playerInstance.transform.position.y;
        position[2] = manager.playerInstance.transform.position.z;
    }
    
}
