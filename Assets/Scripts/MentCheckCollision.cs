using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentCheckCollision : MonoBehaviour
{
    [SerializeField] GameObject jailScrenObject;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit with" + collision);
        if (collision.CompareTag("Ment") | collision.CompareTag("StrongMent"))
        {
            StartCoroutine(GameOvering());
        }
        

    }

    private IEnumerator GameOvering()
    {
        jailScrenObject.SetActive(true);
        //FindObjectOfType<GameStatus>().TimeStopController();
        yield return new WaitForSeconds(1f);
        //FindObjectOfType<GameStatus>().TimeResumeController();
        FindObjectOfType<LevelLoader>().LoadLostScene();

    }
}
