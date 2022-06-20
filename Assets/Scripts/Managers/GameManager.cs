using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int checkPoint = 0;
    public HealthBar healthBar;
    public Transform[] spawnPoints;
    static GameManager _instance = null;
    public bool WinCondition = false;
    public bool GhostShipDead = false;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    int _lives = 3;
    int maxLives = 3;

    float _hp = 100;
    float maxHp = 100;
    public GameObject playerPrefab;

    public float hp
    {
        get { return _hp; }
        set
        {
            if (_hp > value)
            {
                Destroy(playerInstance);
            }
            _hp = value;
            if (_hp > maxHp)
                _hp = maxHp;
            onLifeValueChange.Invoke(value);

        }
    }

    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {
                Destroy(playerInstance);
                SpawnPlayer(currentLevel.spawnPoint.position);
            }
            _lives = value;
            if (_lives > maxLives)
                _lives = maxLives;
            onLifeValueChange.Invoke(value);

        }
    }

    [HideInInspector]public UnityEvent<float> onLifeValueChange;
    [HideInInspector] public GameObject playerInstance;
    [HideInInspector] public Level currentLevel;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    SpawnPlayer(spawnPoints[checkPoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(Vector3 spawnLocation)
    {  
        playerInstance = Instantiate(playerPrefab, spawnLocation, transform.rotation);
        healthBar.setHealth(_hp);
    }

    public void playerTakeDamage(float damage)
    {
        _hp -= damage ;
        healthBar.setHealth(_hp);
        onLifeValueChange.Invoke(_hp );

        if( _hp <= 0)
        {
            LoseLife();
        }
    }

    private void DeathSequence()
    {
        // Death Animation/Screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("EndScene");
    }

     private void LoseLife()
    {
        GameObject[] fireSkeles = GameObject.FindGameObjectsWithTag("FloatingSkull");
        foreach(GameObject fireSkele in fireSkeles)
        {
            Destroy(fireSkele);
        }

        Debug.Log(lives);
        if(_lives <= 0)
        {
            DeathSequence();
        }
        else
        {
            Destroy(playerInstance);
            _hp = maxHp;
            SpawnPlayer(spawnPoints[checkPoint].position);
             _lives --;
        }
    }

    public void PauseMenuChange(bool active)
    {
        if (active)
        {
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SaveState()
    {
        LoadSaveSystem.SaveState(this);
    }

    public void LoadState()
    {
       DataManager data = LoadSaveSystem.LoadState();
       
        checkPoint = data.checkPoint;
        _hp = data.hp;
        _lives = data.lives;
        Vector3 position = new Vector3();
        position.x =data.position[0];
        position.y =data.position[1];
        position.z =data.position[2];

        Destroy(playerInstance);
        SpawnPlayer(position);

        Debug.Log("Checkpoint : " + checkPoint);
        Debug.Log("Lives : " +lives);
        Debug.Log("Hp : " +hp);
        Debug.Log(position);
    }
}
