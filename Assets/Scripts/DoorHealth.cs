using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealth : MonoBehaviour
{
    [SerializeField] int doorHealth = 5;
    // Start is called before the first frame update
    public int GetDoorHealth()
    {
        return doorHealth;
    }
    public void DecreaseDoorHealth()
    {
        doorHealth--;
    }

}
