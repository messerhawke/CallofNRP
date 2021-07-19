using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public void OffControlSound()
    {
        this.gameObject.GetComponent<AudioSource>().volume = 0;
    }
    public void OnControlSound()
    {
        this.gameObject.GetComponent<AudioSource>().volume = 0.6f;
    }
}
