using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour
{
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite inactiveSprite;
    [SerializeField] float timesToTick = 10f;
    [SerializeField] float phoneTimerTick = 0.8f;
    [SerializeField] AudioClip phoneActive;
    [SerializeField] AudioClip phoneInActive;


    public void PhoneCall()
    {
        //Debug.Log("Hello?");
        FindObjectOfType<GameStatus>().ProcessScoreFromCall(timesToTick);
        AudioSource.PlayClipAtPoint(phoneInActive, Camera.main.transform.position, 0.5f);
        this.gameObject.GetComponent<Button>().interactable = false;
        //this.gameObject.GetComponent<Image>().sprite = inactiveSprite;
        StartCoroutine(ButtonNotInteractable());
        //CallInactivity();
        //AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, clickVolume);
    }
    private IEnumerator ButtonNotInteractable()
    {
        
        yield return new WaitForSeconds(timesToTick * phoneTimerTick);
        
        AudioSource.PlayClipAtPoint(phoneActive, Camera.main.transform.position, 0.5f);
        this.gameObject.GetComponent<Button>().interactable = true;
        //this.gameObject.GetComponent<Image>().sprite = activeSprite;

    }

    public float GetPhoneTick()
    {
        return phoneTimerTick;
    }

}
