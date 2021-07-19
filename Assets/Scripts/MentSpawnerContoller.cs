using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentSpawnerContoller : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool loopingWaves = false;
    int startingWave = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
            yield return StartCoroutine(SpawnAllWaves());
        while (loopingWaves);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {

            Debug.Log("Doing all waves");
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllMents(currentWave));
            //StartCoroutine(SpawnAllCheliks(currentWave));
            //yield return new WaitForSeconds(3);
        }

    }

    private IEnumerator SpawnAllMents(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfSpawns(); i++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetPersonPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<PersonPathing>().SetWaveConfig(waveConfig);
            Debug.Log("Doing current wave");
            yield return new WaitForSeconds(waveConfig.GetTimeSpawns() + Random.Range(1, waveConfig.GetSpawnRandomness()));
        }
    }
}
