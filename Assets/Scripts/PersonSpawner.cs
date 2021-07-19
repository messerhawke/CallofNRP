using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
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
            var currentWave = waveConfigs[i]; 
            yield return StartCoroutine(SpawnAllCheliks(currentWave));
            //StartCoroutine(SpawnAllCheliks(currentWave));
            //yield return new WaitForSeconds(3);
        }

    }

    private IEnumerator SpawnAllCheliks(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfSpawns(); i++)
        {
            var newBoy = Instantiate(
                waveConfig.GetPersonPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newBoy.GetComponent<PersonPathing>().SetWaveConfig(waveConfig);
            
            yield return new WaitForSeconds(waveConfig.GetTimeSpawns()+UnityEngine.Random.Range(1,waveConfig.GetSpawnRandomness()));
        }
    }
}
