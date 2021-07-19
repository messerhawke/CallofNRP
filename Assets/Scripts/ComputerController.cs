using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour
{
    [SerializeField] int moneyPerClick = 1;
    [SerializeField] float timerInactivity = 1f;
    [SerializeField] AudioClip cashRing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ComputerCall()
    {
        FindObjectOfType<GameStatus>().ProcessMoney(moneyPerClick);
        AudioSource.PlayClipAtPoint(cashRing, Camera.main.transform.position, 0.3f);
        this.gameObject.GetComponent<Button>().interactable = false;
        StartCoroutine(ButtonNotInteractable());
        //CallInactivity();
        //AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, clickVolume);
    }
    private IEnumerator ButtonNotInteractable()
    {
        yield return new WaitForSeconds(timerInactivity);
        this.gameObject.GetComponent<Button>().interactable = true;

    }
}
