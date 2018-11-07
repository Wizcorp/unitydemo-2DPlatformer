using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnType
    {
        public GameObject   character;
        public int          maxInstances;

        [HideInInspector]
        public List<GameObject> instances;
    }

	public float spawnTime = 5f;		// The amount of time between each spawn.
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    public SpawnType[] spawnTypes;

    void Awake()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(spawnTime);
        while(true)
        {
            Spawn();
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    int GetSpawnInstanceNullSlot(SpawnType spawnType)
    {
        for (int i = 0; i < spawnType.maxInstances; ++i)
        {
            if (spawnType.instances[i] == null)
                return i;
        }
        return -1;
    }

    bool CanSpawn(SpawnType spawnType)
    {
        if (spawnType.instances.Count < spawnType.maxInstances)
            return true;

        if (GetSpawnInstanceNullSlot(spawnType) >= 0)
            return true;

        return false;
    }

    void Spawn(SpawnType spawnType)
    {
        GameObject go = Instantiate(spawnType.character, transform.position, transform.rotation);

        Vector2 initialOrientation = new Vector2(transform.localScale.x, 0f);
        go.GetComponent<Character>().SetOrientation(initialOrientation);

        if (spawnType.instances.Count < spawnType.maxInstances)
        {
            spawnType.instances.Add(go);
        }
        else
        {
            int slot = GetSpawnInstanceNullSlot(spawnType);
            spawnType.instances[slot] = go;
        }

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }

    void Spawn ()
	{
        if (spawnTypes.Length == 0)
            return;

        int startIndex = Random.Range(0, spawnTypes.Length);
        int typeIndex = startIndex;

        while (!CanSpawn(spawnTypes[typeIndex])) 
        {
            typeIndex = (typeIndex + 1) % spawnTypes.Length;
            if (typeIndex == startIndex)
            {
                typeIndex = -1;
                break;
            }
        }

        if (typeIndex == -1)
            return;

        Spawn(spawnTypes[typeIndex]);
	}
}
