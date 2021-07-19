using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectricityController : MonoBehaviour
{
    [SerializeField] GameObject electricityOutObject;
    [SerializeField] int startDelayToDisableElectricity;
    [SerializeField] int timeBetweenToDisableElectricity;
    [SerializeField] int timerAdditionalRandomess=5;
    [SerializeField] bool isElectricityEventActive = false;
    [SerializeField] GameObject buttonToEnableElectricity;
    [SerializeField] int costToEnableElectro = 10;
    [SerializeField] AudioClip buttonSuccsed;
    [SerializeField] AudioClip buttonFail;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Random.Range(startDelayToDisableElectricity, startDelayToDisableElectricity+timerAdditionalRandomess));
        while (isElectricityEventActive)
        {
            yield return StartCoroutine(DisableElectricity());
        }
    }

    private IEnumerator DisableElectricity()
    {

        electricityOutObject.SetActive(true);
        buttonToEnableElectricity.SetActive(true);
        FindObjectOfType<GameStatus>().CheckLostCondition(costToEnableElectro);
        yield return new WaitForSeconds(Random.Range(timeBetweenToDisableElectricity,timeBetweenToDisableElectricity+timerAdditionalRandomess));
    }

    public void ElectricityEnabler()
    {
        if (FindObjectOfType<GameStatus>().isLostCondition(costToEnableElectro) == false & FindObjectOfType<GameStatus>().GetMoneyStatus() >= costToEnableElectro)
        {
            AudioSource.PlayClipAtPoint(buttonSuccsed, Camera.main.transform.position, 0.8f);
            electricityOutObject.SetActive(false);
            buttonToEnableElectricity.gameObject.SetActive(false);
            FindObjectOfType<GameStatus>().ElectricityCost(costToEnableElectro);
            
        }else
        {
            AudioSource.PlayClipAtPoint(buttonFail, Camera.main.transform.position, 0.8f);
            FindObjectOfType<GameStatus>().CheckLostCondition(costToEnableElectro);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
