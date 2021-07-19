using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log(hit.collider.name);
                    if(hit.collider.gameObject.CompareTag("Employee"))
                    {
                        Debug.Log("My man");
                    }
                    else
                    Destroy(gameObject);
                }

            }
        }
    }
}
