using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    private string currentGameMode;

    public void SetGameMode(string gm)
    {
        currentGameMode = gm;
    }

    public string GetGameMode()
    {
        return currentGameMode ;
    }


}
