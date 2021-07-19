using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] AudioClip soundWhenDoorHit;
    [SerializeField] GameObject nowDoorOpened;
    [SerializeField] GameObject noButtonToUse;
    bool isDoorCollided=false;
    float timer = 0;
    // Start is called before the first frame update
    void Update()
    {
        if (isDoorCollided)
        {
            timer += Time.deltaTime;
            Debug.Log("Door is in Siege");
            if (timer >= 5)
            {
                if (this.GetComponent<DoorHealth>().GetDoorHealth() <= 0)
                {
                    Debug.Log("Door Dead");
                    Destroy(this.gameObject);
                    nowDoorOpened.SetActive(true);
                    noButtonToUse.SetActive(false);
                }
                else
                {
                    Debug.Log("Door Hitted");
                    AudioSource.PlayClipAtPoint(soundWhenDoorHit, Camera.main.transform.position, 0.8f);
                    this.GetComponent<DoorHealth>().DecreaseDoorHealth();
                    timer = 0;

                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ment") | collision.gameObject.CompareTag("StrongMent"))
        {
            //StartCoroutine(ProcessDoorHit());
            isDoorCollided = true;
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ment") | collision.gameObject.CompareTag("StrongMent"))
        {
            //StartCoroutine(ProcessDoorHit());
            isDoorCollided = false;
            timer = 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }


    private IEnumerator ProcessDoorHit()
    {
        if (this.GetComponent<DoorHealth>().GetDoorHealth() <= 0)
        {
            Debug.Log("Door Dead");
            Destroy(this.gameObject);
            nowDoorOpened.SetActive(true);
        }
        else
            this.GetComponent<DoorHealth>().DecreaseDoorHealth();
        yield return new WaitForSeconds(5f);
    }
}
