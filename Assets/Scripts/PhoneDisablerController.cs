using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhoneDisablerController : MonoBehaviour
{
    [SerializeField] GameObject phonkButton;
    [SerializeField] int startDelayToDisablePhonk;
    [SerializeField] int timeBetweenToDisablePhonk;
    [SerializeField] int timerAdditionalRandomess = 5;
    [SerializeField] bool isElectricityEventActive = false;
    [SerializeField] GameObject buttonToEnablePhonk;
    [SerializeField] int costToEnablePhonk = 10;
    [SerializeField] AudioClip buttonSuccsed;
    [SerializeField] AudioClip buttonFail;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDelay());
    }
    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Random.Range(startDelayToDisablePhonk, startDelayToDisablePhonk + timerAdditionalRandomess));
        while (isElectricityEventActive)
        {
            yield return StartCoroutine(DisablePhonk());
        }
    }
    private IEnumerator DisablePhonk()
    {

        phonkButton.SetActive(false);
        buttonToEnablePhonk.SetActive(true);
        yield return new WaitForSeconds(Random.Range(timeBetweenToDisablePhonk, timeBetweenToDisablePhonk + timerAdditionalRandomess));
    }

    public void PhonkEnabler()
    {
        if (FindObjectOfType<GameStatus>().GetMoneyStatus() >= costToEnablePhonk)
        {
            AudioSource.PlayClipAtPoint(buttonSuccsed, Camera.main.transform.position, 0.8f);
            phonkButton.SetActive(true);
            phonkButton.GetComponent<Button>().interactable = true;
            buttonToEnablePhonk.SetActive(false);
            
            FindObjectOfType<GameStatus>().ElectricityCost(costToEnablePhonk);
        }
        else
        {
            AudioSource.PlayClipAtPoint(buttonFail, Camera.main.transform.position, 0.8f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
