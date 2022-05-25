using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public HealthBar healthBar;
    public Transform[] spawnPoints;
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

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
        int spawner = Random.Range(0,4);
        SpawnPlayer(spawnPoints[spawner]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        healthBar.SetMaxHealth(_hp);
    }

    public void playerTakeDamage(float damage)
    {
        _hp -= damage ;
        healthBar.setHealth(_hp);
        onLifeValueChange.Invoke(_hp );

        if( _hp <= 0)
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        // Death Animation/Screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("EndScene");
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
}
