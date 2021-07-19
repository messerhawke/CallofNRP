using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawns Wave Config")]
public class WaveConfig : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject personPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeForSpawns = 2f;
    [SerializeField] float spawnRandomizer = 3f;
    [SerializeField] int numberSpawnsAtOnce = 1;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetPersonPrefab()
    {
        return personPrefab;
    }
    /*public GameObject GetPathPrefab()
    {
        return pathPrefab;
    }*/
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public float GetTimeSpawns()
    {
        return timeForSpawns;
    }
    public float GetSpawnRandomness()
    {
        return spawnRandomizer;
    }
    public int GetNumberOfSpawns()
    {
        return numberSpawnsAtOnce;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
