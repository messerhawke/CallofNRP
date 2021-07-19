using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentHealth : MonoBehaviour
{
    [SerializeField] int mentHealth = 3;
    
    public int GetMentHealth()
    {
        return mentHealth;
    }
    public void DecreaseMentHealth()
    {
        mentHealth--;
    }
}
