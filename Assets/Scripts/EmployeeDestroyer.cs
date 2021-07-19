using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit with" + collision);
        if (collision.gameObject.CompareTag("Employee"))
        { 
            FindObjectOfType<GameStatus>().ProcessAdditionalScore();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Volk"))
        {
            FindObjectOfType<GameStatus>().ProcessFakeAdditionalScore();
            Destroy(collision.gameObject);
        }



    }
}
