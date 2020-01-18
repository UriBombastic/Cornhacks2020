using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    private static GameMaster _instance;
    public static GameMaster Instance()
    {
        if(_instance == null)
            _instance = FindObjectOfType<GameMaster>();

        return _instance;
    }

    public GameObject DeathScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDeath()
    {
        Time.timeScale = 0;
        DeathScreen.SetActive(true);
    }
}
