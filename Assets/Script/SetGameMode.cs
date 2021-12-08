using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameMode : MonoBehaviour
{

    public SetRandomDestination setrandom;
    public SpawnCarAI AIcar;
    public SpawnCivAI AIciv;

    public void Start()
    {
        setrandom = FindObjectOfType<SetRandomDestination>();
        AIcar = FindObjectOfType<SpawnCarAI>();
        AIciv = FindObjectOfType<SpawnCivAI>();
    }


    public void SetGameModeEasy()
    {
        setrandom.numOfDeliveries = 10;
        AIcar.hubCarNum = 10;
        AIciv.pedestrianNum = 10;
    }

    public void SetModeMedium()
    {
        setrandom.numOfDeliveries = 15;
        AIcar.hubCarNum = 20;
        AIciv.pedestrianNum = 20;
    }
    public void SetModeHard()
    {
        setrandom.numOfDeliveries = 20;
        AIcar.hubCarNum = 35;
        AIciv.pedestrianNum = 35;
    }
    public void SetModeEndless()
    {
        setrandom.numOfDeliveries = 100000000000;
        AIcar.hubCarNum = 30;
        AIciv.pedestrianNum = 30;
    }
}
