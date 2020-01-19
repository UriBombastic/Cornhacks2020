using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform[] spawnSpaces;
    public GameObject[] platforms;
    public int minPlatforms;
    public int maxPlatforms;
    void Start()
    {

    }

  public void GeneratePlatforms()
    {
        int numPlatforms = Random.Range(minPlatforms, maxPlatforms);
        for(int i = 0; i < numPlatforms; i++)
        {
            int platformIndex = Random.Range(0, platforms.Length);
            int spawnIndex = Random.Range(0, spawnSpaces.Length);
            Instantiate(platforms[platformIndex], spawnSpaces[spawnIndex]);

        }
    }


}
