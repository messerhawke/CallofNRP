using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> personWaypoints;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        personWaypoints = waveConfig.GetWaypoints();
        transform.position = personWaypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveChelik();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void MoveChelik()
    {
        if (waypointIndex <= personWaypoints.Count - 1)
        {
            var targetPosition = personWaypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
