using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameMode : MonoBehaviour
{
    public void SetGameModeEasy()
    {
        GetComponent<SetRandomDestination>().numOfDeliveries = 10;
        GetComponent<SpawnCarAI>().hubCarNum = 10;
        GetComponent<SpawnCivAI>().pedestrianNum = 10;
    }

    public void SetModeMedium()
    {
        GetComponent<SetRandomDestination>().numOfDeliveries = 15;
        GetComponent<SpawnCarAI>().hubCarNum = 20;
        GetComponent<SpawnCivAI>().pedestrianNum = 20;
    }
    public void SetModeHard()
    {
        GetComponent<SetRandomDestination>().numOfDeliveries = 20;
        GetComponent<SpawnCarAI>().hubCarNum = 35;
        GetComponent<SpawnCivAI>().pedestrianNum = 35;
    }
    public void SetModeEndless()
    {
        GetComponent<SetRandomDestination>().numOfDeliveries = float.PositiveInfinity;
        GetComponent<SpawnCarAI>().hubCarNum = 30;
        GetComponent<SpawnCivAI>().pedestrianNum = 30;
    }
}
