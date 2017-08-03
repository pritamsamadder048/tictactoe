using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;


public class MenuController : MonoBehaviour
{

    public GameObject gameModeObj;
    public GameObject previousGameModeObj;
    private int bpress = 0;

    public void Awake()
    {
        if (GameObject.Find("gameModeObj") != null)
        {
            previousGameModeObj = GameObject.Find("gameModeObj");
            DestroyObject(previousGameModeObj);

        }
        gameModeObj.name = "gameModeObj";
        DontDestroyOnLoad(gameModeObj);
    }


    public void StartSinglePlayer()
    {
        bool isAdCompleted = false;
        if (Advertisement.IsReady() && Advertisement.isSupported)
        {
            Advertisement.Show(new ShowOptions() { resultCallback = HandleSinglePlayersAdd });
        }

        if(bpress>=5)
        {
            bpress = 0;
            gameModeObj.GetComponent<GameMode>().SetGameMode("SINGLEPLAYER");
            SceneManager.LoadScene("SinglePlayer");
        }
        else
        {
            bpress++;
        }
        //print("loading single player");
        //gameModeObj.GetComponent<GameMode>().SetGameMode( "SINGLEPLAYER");
        //SceneManager.LoadScene("SinglePlayer");
    }


    private void HandleSinglePlayersAdd(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                {
                    print("finished ad");
                    gameModeObj.GetComponent<GameMode>().SetGameMode("SINGLEPLAYER");
                    SceneManager.LoadScene("SinglePlayer");
                    break;
                }
            case ShowResult.Skipped:
                {
                    print("skipped ad");
                    break;
                }
            case ShowResult.Failed:
                {
                    print("failed ad");
                    break;
                }

            default:
                {
                    break;
                }
        }
    }
    public void StartTwoPlayers()
    {
        gameModeObj.GetComponent<GameMode>().SetGameMode("TWOPLAYER");
        SceneManager.LoadScene("TwoPlayers");
    }

    public void ExitGame()
    {
        //print("exitting..");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

}



