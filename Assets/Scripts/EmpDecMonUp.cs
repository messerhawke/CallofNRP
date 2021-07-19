using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmpDecMonUp : MonoBehaviour
{
    //[SerializeField] int moneyPerClick = 1;
    [SerializeField] float timerInactivity = 0.5f;
    [SerializeField] AudioClip buttonSuccsed;
    [SerializeField] AudioClip buttonFail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PumpOverrCall()
    {
        FindObjectOfType<GameStatus>().ProcessPumpOver(buttonSuccsed, buttonFail);
        this.gameObject.GetComponent<Button>().interactable = false;
        StartCoroutine(ButtonNotInteractable());
    }
    private IEnumerator ButtonNotInteractable()
    {
        yield return new WaitForSeconds(timerInactivity);
        this.gameObject.GetComponent<Button>().interactable = true;

    }
}
