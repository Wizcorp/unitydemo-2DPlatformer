using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public int maxPlatforms;
    public GameObject platform;
    public float horizontalMin = 7.5f;
    public float horizontalMax = 14f;
    public float verticalMin = -6f;
    public float verticalMax = 6;


    private Vector2 originPosition;

    public GameObject[] platformsArray = new GameObject[5];
    public List<GameObject> platformsList = new List<GameObject>();

    void Start()
    {

        originPosition = transform.position;
        Spawn(maxPlatforms);
        for (int i = 0; i < platformsArray.Length; i++)
        {
            platformsList.Add(platformsArray[i]);
        }
    }

    void Update()
    {
        if (platformsList.Count < maxPlatforms)
        {
            Spawn(maxPlatforms - platformsList.Count);
        }

        for (int i = 0; i < platformsList.Count; i++)
        {
            if (platformsList[i] == null)
            {
                platformsList.Remove(platformsList[i]);
            }
        }
    }

    void Spawn(int numOfPlatforms)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        for (int i = 0; i < numOfPlatforms; i++)
        {
            Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
            GameObject env_PlatformTop = Instantiate(platform, randomPosition, Quaternion.identity);
            platformsList.Add(env_PlatformTop);
        }

        createDistance();
        
    }

    void createDistance()
    {
        for (int i = 0; i < platformsList.Count; i++)
        {
            for (int j = 0; j < platformsList.Count; j++)
            {
                if (platformsList[i] != null && platformsList[j] != null && platformsList[i] != platformsList[j])
                {
                    if (Vector2.Distance(platformsList[i].transform.position, platformsList[j].transform.position) < 10)
                    {
                        Vector3 direction = platformsList[i].transform.position - platformsList[j].transform.position;
                        direction.Normalize();

                        platformsList[i].transform.position = platformsList[i].transform.position + (direction * 5);
                    }
                }
            }
        }
    }
}