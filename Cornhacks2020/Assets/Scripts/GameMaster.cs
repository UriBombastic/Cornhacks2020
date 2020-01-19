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
    private int EnemyKills =0;
    public int[] EnemyKillsRequired;
    public float[] spawnRates;
    public int RoundNumber =0;
    public float RoundEndTime;
    public EnemySpawner spawner;
    public GameObject roundOverScreen;
    public PlatformGenerator platformGen;
    public PlayerControl player;
    public GameObject FinalBoss;
    // Start is called before the first frame update
    void Start()
    {
        StartNewRound();
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

    public void HandleKills()
    {
        EnemyKills++;
        if (EnemyKills >= EnemyKillsRequired[RoundNumber])
            EndRound();
    }

    public void EndRound()
    {
        EnemyKills = 0;
        RoundNumber++;
        Enemy[] remainingEnemies = FindObjectsOfType<Enemy>();
        spawner.gameObject.SetActive(false);
        foreach (Enemy En in remainingEnemies)
            Destroy(En.gameObject);
        player.Health = player.MaxHealth;
        GameObject[] allPlatforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject go in allPlatforms)
            Destroy(go);
        StartCoroutine(HandleRoundEndScreen());
    }

    public IEnumerator HandleRoundEndScreen()
    {
        roundOverScreen.SetActive(true);
        yield return new WaitForSeconds(RoundEndTime);
        roundOverScreen.SetActive(false);

        if(RoundNumber==EnemyKillsRequired.Length)
            StartNewRound();
        else
        {
            yield return new WaitForSeconds(RoundEndTime);
            StartFinalRound();
        }

    }

    public void StartNewRound()
    {
        platformGen.GeneratePlatforms();
        spawner.gameObject.SetActive(true);
        spawner.SpawnTime = spawnRates[RoundNumber];
        spawner.canSpawn = true;

    }

    public void StartFinalRound()
    {
        Instantiate(FinalBoss);
    }
}
