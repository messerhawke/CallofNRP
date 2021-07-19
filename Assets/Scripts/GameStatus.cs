using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameStatus : MonoBehaviour
{
    [Header("GUI - TMPobjects")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI employeesText;
    [SerializeField] TextMeshProUGUI moneyText;
    [Header("Audio Clips")]
    [SerializeField] AudioClip employeeHired;
    [SerializeField] AudioClip employeeSkipped;
    [SerializeField] AudioClip employeeHit;
    [SerializeField] AudioClip mentHit;
    [SerializeField] AudioClip mentDie;

    [Range(0, 100)] int startScore = 0;
    [Range(0, 10)] int startEmployees = 0;
    int startMoney = 0;
    [Header("Mechanics cost")]
    [SerializeField] int costToHire;
    [SerializeField] int costToHitEmployee = 2;
    [SerializeField] int fakeEmployeeDamageScore = 25;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = startScore.ToString();
        employeesText.text = startEmployees.ToString();
        moneyText.text = startMoney.ToString();
        TimeStopController();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Employee"))
                {
                    Debug.Log("My man");
                    StartCoroutine(SpinMyMan(hit.collider.gameObject, employeeHit));
                    DecreaseScore();
                } else
                if (hit.collider.gameObject.CompareTag("Ment"))
                {
                    Debug.Log("Sobaka");

                    if (hit.collider.gameObject.GetComponent<MentHealth>().GetMentHealth() <= 1)
                    {
                        StartCoroutine(SpinMyMan(hit.collider.gameObject, mentDie));
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<MentHealth>().DecreaseMentHealth();
                        AudioSource.PlayClipAtPoint(mentHit, Camera.main.transform.position, 0.7f);
                    }
                }
                else
                if (hit.collider.gameObject.CompareTag("Volk"))
                {
                    Debug.Log("Pes");
                    StartCoroutine(SpinMyMan(hit.collider.gameObject, employeeHit));
                }
                else
                if (hit.collider.gameObject.CompareTag("StrongMent"))
                {
                    Debug.Log("tafgai");
                    if (hit.collider.gameObject.GetComponent<MentHealth>().GetMentHealth() <= 1)
                    {
                        StartCoroutine(SpinMyMan(hit.collider.gameObject, mentDie));
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<MentHealth>().DecreaseMentHealth();
                        AudioSource.PlayClipAtPoint(mentHit, Camera.main.transform.position, 0.7f);
                    }
                }
                /*if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.gameObject.CompareTag("Employee"))
                    {
                        Debug.Log("My man");
                    }
                    else
                        Destroy(gameObject);
                }*/

            }
        }
    }

    private void DecreaseScore()
    {
        if (startScore >= costToHitEmployee)
        {
            startScore = startScore - costToHitEmployee;
            scoreText.text = startScore.ToString();
        }
        else if (startScore < costToHitEmployee)
        {
            startScore = 0;
            scoreText.text = startScore.ToString();
        }
    }

    private IEnumerator SpinMyMan(GameObject myMan, AudioClip soundWhenHit)
    {
        AudioSource.PlayClipAtPoint(soundWhenHit, Camera.main.transform.position, 0.7f);
        SpinBoys(myMan);
        yield return new WaitForSeconds(2f);
        AudioSource.PlayClipAtPoint(employeeSkipped, Camera.main.transform.position, 0.7f);
        Destroy(myMan);
    }
    private void SpinBoys(GameObject myMan)
    {
        Rigidbody2D myRigidbody2D = myMan.GetComponent<Rigidbody2D>();
        myMan.GetComponent<Collider2D>().enabled = false;
        Vector2 velocitytweak = new Vector2(UnityEngine.Random.Range(9, 11), UnityEngine.Random.Range(-6, 6));
        myRigidbody2D.velocity += velocitytweak;
        myRigidbody2D.AddTorque(UnityEngine.Random.Range(100, 500));
    }

    public void ProcessScoreFromCall(float timerInactivity)
    {

        StartCoroutine(ScorePerSec(timerInactivity));

    }

    public bool isLostCondition(int costToEnable)
    {
        return (startEmployees < 1 & startMoney < costToEnable);
    }

    public void CheckLostCondition(int costToEnable)
    {
        if (isLostCondition(costToEnable))
        {
            StartCoroutine(GameOvering());
        }
    }

    public int GetMoneyStatus()
    {
        return startMoney;
    }

    public void ElectricityCost(int costToEnable)
    {
        startMoney -= costToEnable;
        moneyText.text = startMoney.ToString();
    }

    private IEnumerator GameOvering()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<LevelLoader>().LoadLostScene();
    }

    private IEnumerator ScorePerSec(float timerInactivity)
    {
        for (int i = 1; i <= timerInactivity; i++)
        {
            if (startScore >= 100)
            {
                WinConditionCheck();
            }
            else if(startScore == 99)
            {
                startScore++;
                WinConditionCheck();
            }
            else
            {
                startScore++;
            }


                scoreText.text = startScore.ToString();
            //WinConditionCheck();
            yield return new WaitForSeconds(FindObjectOfType<PhoneController>().GetPhoneTick());
        }

    }

    private void WinConditionCheck()
    {
        if (startScore >= 100 & startEmployees>=10)
        {
            FindObjectOfType<LevelLoader>().LoadNextScene();
        }
    }

    public void ProcessAdditionalScore()
    {
        if (startEmployees >= 10)
        {
            WinConditionCheck();
            AudioSource.PlayClipAtPoint(employeeSkipped, Camera.main.transform.position, 0.8f);
        }
        else if (startEmployees == 9)
        {
            EmployeeHire();
            WinConditionCheck();
        }
        else
        {
            EmployeeHire();
        }

        employeesText.text = startEmployees.ToString();
    }

    public void ProcessFakeAdditionalScore()
    {
        if(startScore<fakeEmployeeDamageScore)
        {
            startScore = 0;
        }
        else
        {
            startScore -= fakeEmployeeDamageScore;
        }
        scoreText.text = startScore.ToString();
    }

    public void ProcessMoney(int moneyPerClick)
    {
        startMoney=startMoney+moneyPerClick;
        moneyText.text = startMoney.ToString();
    }

    private void EmployeeHire()
    {
        if (startMoney>=costToHire)
        {
            startEmployees++;
            startMoney = startMoney - costToHire;
            moneyText.text = startMoney.ToString();
            AudioSource.PlayClipAtPoint(employeeHired, Camera.main.transform.position, 0.8f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(employeeSkipped, Camera.main.transform.position, 0.8f);
        }
    }
    public void TimeStopController()
    {
        Time.timeScale = 0;
    }
    public void TimeResumeController()
    {
        Time.timeScale = 1;
    }
    public void ProcessPumpOver(AudioClip buttonSuccsed, AudioClip buttonFail)
    {
        if (startEmployees < 1)
        {
            AudioSource.PlayClipAtPoint(buttonFail, Camera.main.transform.position, 0.8f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(buttonSuccsed, Camera.main.transform.position, 0.8f);
            startEmployees--;
            employeesText.text = startEmployees.ToString();
            startMoney += costToHire;
            moneyText.text = startMoney.ToString();
            if (startScore < costToHitEmployee)
            {
                startScore = 0;
                scoreText.text = startScore.ToString();
            }
            else
            {
                startScore = startScore - costToHitEmployee;
                scoreText.text = startScore.ToString();
            }
        }
    }

}






/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrashOnClick : MonoBehaviour
{
// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == gameObject) 
                Destroy(gameObject);
        }
    }
}
}


    Vector2 velocitytweak = new Vector2(A,B);
    myRigidbody2D.velocity += velocitytweak;
    myRigidbody2D.AddTorque(UnityEngine.Random.Range(-50, 50));
 */
