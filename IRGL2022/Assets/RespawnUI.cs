using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnUI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool checkIfAirplaneHasSpawned()
    {
        airplane _airplane = FindObjectOfType<airplane>();
        if(_airplane != null)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(checkIfAirplaneHasSpawned())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
