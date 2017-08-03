using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif





public class TwoPlayersGameController : MonoBehaviour {

    public Text[] buttonList;
    private string playerSide;
    [SerializeField]
    private string player1Side;
    [SerializeField]
    private string player2Side;
    private string computerSide;
    [SerializeField]
    private string currentSide;

    [SerializeField]
    public bool playerMove;
    [SerializeField]
    public bool player1Move;
    [SerializeField]
    public bool player2Move;

    [SerializeField]
    private float delay;

    [SerializeField]
    private int value;
    
    [SerializeField]
    private string player1 = "X";
    
    [SerializeField]
    private string player2 = "O";


    private bool isGameOver = false;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private GameObject restartButton;

    private int moveCount;
    private bool isDraw;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    [SerializeField]
    private GameObject startInfo;

    public GameMode currentSessionGameMode;


    public GameObject moveInfoPanel;
    public Text moveInfoText;

    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button menuButton;

    private void Awake()
    {
        
        if(GameObject.Find("gameModeObj") != null)
        {
            currentSessionGameMode = GameObject.Find("gameModeObj").GetComponent<GameMode>();
            //print("Current GameMode : " + currentSessionGameMode.GetGameMode());
        }
        gameOverPanel.SetActive(false);

        SetGameControllerRefferenceOnButtons();
        
        moveCount = 0;
        isDraw = false;
        restartButton.SetActive(false);
        playerMove = true; //fix-this
        player1Move = true;
        delay = 1f;//fix-this
        moveInfoPanel.SetActive(false);
        
        
    }



    //private void Update()
    //{
    //    ComputerPlay();
    //}



    //public void ComputerPlay()
    //{
    //    if (!playerMove && !isGameOver)
    //    {
    //        delay += delay * Time.deltaTime;
    //        if (delay >= 10)
    //        {
    //            //value = UnityEngine.Random.Range(0, 8);
    //            value = GetBestMove(computerSide);
    //            if (buttonList[value].GetComponentInParent<Button>().interactable)
    //            {
    //                buttonList[value].text = GetComputerSide();
    //                EndTurn();
    //                buttonList[value].GetComponentInParent<Button>().interactable = false;
    //                delay = 1f;
    //            }
    //        }
    //    }
    //}



    /// <summary>
    /// get best suitable move for given character movchar
    /// </summary>
    /// <param name="movchar"> moveChar should be either "X" or "O" </param>
    /// <returns></returns>
    private int GetBestMove(string movchar)
    {
        int value = 0;

        int looseChance = 0;
        int winChance = 0;
        int winindex = -1;
        int looseindex = -1;


        /*/////////////////// Row basis checking//////////////////////////////*/
        {   /////////////////// Row basis checking

            looseChance = 0;
            winChance = 0;
            winindex = -1;
            looseindex = -1;

            for (int i = 0; i < buttonList.Length; i += 3) // check for row
            {
                looseChance = 0;
                winChance = 0;
                

                if (buttonList[i].text == movchar) // column 0
                {
                    winChance += 1;

                }
                else if (buttonList[i].text != movchar && buttonList[i].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = i;
                }

                if (buttonList[i + 1].text == movchar) // column 1
                {
                    winChance += 1;

                }
                else if (buttonList[i + 1].text != movchar && buttonList[i + 1].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = i + 1;
                }

                if (buttonList[i + 2].text == movchar)  // column 2
                {
                    winChance += 1;

                }
                else if (buttonList[i + 2].text != movchar && buttonList[i + 2].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = i + 2;
                }

                if ((winChance >= 2) && ((winChance + looseChance) < 3))
                {
                    return value;
                }
                else if ((looseChance >= 2) && ((winChance + looseChance) < 3))
                {
                    looseindex = value;
                }

                //print("row basis : i : " + i + " winchance : " + winChance + " loosechance : " + looseChance + " loose index : " + looseindex);
            }

            if (looseindex >= 0)
            {
                //print("column so giving you a hint press here : " + looseindex);
                return looseindex;
            }

        }




        /*/////////////////// Column basis checking//////////////////////////////*/
        {   ////////////////// column basis chacking
            looseChance = 0;
            winChance = 0;
            winindex = -1;
            looseindex = -1;

            for (int i = 0; i < 3; i++) // check for column
            {
                looseChance = 0;
                winChance = 0;

                if (buttonList[i].text == movchar) // row 0
                {
                    winChance += 1;

                }
                else if (buttonList[i].text != movchar && buttonList[i].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = i;
                }

                if (buttonList[i + 3].text == movchar) // row 1
                {
                    winChance += 1;

                }
                else if (buttonList[i + 3].text != movchar && buttonList[i + 3].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = i + 3;
                }

                if (buttonList[i + 6].text == movchar)  // row 2
                {
                    winChance += 1;

                }
                else if (buttonList[i + 6].text != movchar && buttonList[i + 6].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = i + 6;
                }

                
                if ((winChance >= 2) && ((winChance + looseChance) < 3))
                {
                    return value;
                }
                else if ((looseChance >= 2) && ((winChance + looseChance) < 3))
                {
                    looseindex = value;
                    
                }
                //print("column basis : i : " + i + " winchance : " + winChance + " loosechance : " + looseChance + " loose index : " + looseindex);
            }

            if (looseindex >= 0)
            {
                //print("column so giving you a hint press here : " + looseindex);
                return looseindex;
            }
        }


        /*/////////////////// Diagonal basis checking //////////////////////////*/
        {
            

            looseChance = 0;
            winChance = 0;
            winindex = -1;
            looseindex = -1;
            {   //check for diagonal 0,4,8
                looseChance = 0;
                winChance = 0;
                if (buttonList[0].text == movchar) // diagonal 0
                {
                    winChance += 1;

                }
                else if (buttonList[0].text != movchar && buttonList[0].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = 0;
                }

                if (buttonList[4].text == movchar) // diagonal 4
                {
                    winChance += 1;

                }
                else if (buttonList[4].text != movchar && buttonList[4].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = 4;
                }

                if (buttonList[8].text == movchar)  // diagonal 8
                {
                    winChance += 1;

                }
                else if (buttonList[8].text != movchar && buttonList[8].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = 8;
                }


                if ((winChance >= 2) && ((winChance + looseChance) < 3))
                {
                    return value;
                }
                else if ((looseChance >= 2) && ((winChance + looseChance) < 3))
                {
                    looseindex = value;


                }
                //print("diagonal 0,4,8 basis :  " + " winchance : " + winChance + " loosechance : " + looseChance + " loose index : " + looseindex);
            }


            {   //check for diagonal 2,4,6
                looseChance = 0;
                winChance = 0;
                if (buttonList[2].text == movchar) // diagonal  2
                {
                    winChance += 1;

                }
                else if (buttonList[2].text != movchar && buttonList[2].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = 2;
                }

                if (buttonList[4].text == movchar) // diagonal 4
                {
                    winChance += 1;

                }
                else if (buttonList[4].text != movchar && buttonList[4].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = 4;
                }

                if (buttonList[6].text == movchar)  // diagonal 6
                {
                    winChance += 1;

                }
                else if (buttonList[6].text != movchar && buttonList[6].GetComponentInParent<Button>().interactable == false)
                {
                    looseChance += 1;
                }
                else
                {
                    value = 6;
                }


                if ((winChance >= 2) && ((winChance + looseChance) < 3))
                {
                    return value;
                }
                else if ((looseChance >= 2) && ((winChance + looseChance) < 3))
                {
                    looseindex = value;


                }
                //print("diagonal 2,4,6 basis :  " + " winchance : " + winChance + " loosechance : " + looseChance + " loose index : " + looseindex);
            }

            if (looseindex >= 0)
            {
                //print("diagonal so giving you a hint press here : " + looseindex);
                return looseindex;
            }

        }




        /*////////////////// Now for the best part..make her intelligent..////////////*/
        {
            int[] emptyCorner = new int[4];
            int ni = 0;
            int totalEmptyCorners = 0;

            //print("before assigning totalEmptyCorners : " + totalEmptyCorners);
            if(buttonList[0].GetComponentInParent<Button>().interactable == true)
            {
                emptyCorner[ni] = 0;
                totalEmptyCorners += 1;
                ++ni;
            }
            if (buttonList[2].GetComponentInParent<Button>().interactable == true)
            {
                emptyCorner[ni] = 2;
                totalEmptyCorners += 1;
                ++ni;
            }
            if (buttonList[6].GetComponentInParent<Button>().interactable == true)
            {
                emptyCorner[ni] = 6;
                totalEmptyCorners += 1;
                ++ni;
            }
            if (buttonList[8].GetComponentInParent<Button>().interactable == true)
            {
                emptyCorner[ni] = 8;
                totalEmptyCorners += 1;
                ni =0;
            }
            //print("after assigning totelEmptyCorners : " + totalEmptyCorners);
            if (totalEmptyCorners > 0)
            {

                ni = UnityEngine.Random.Range(0, totalEmptyCorners);
                value = emptyCorner[ni];
                ni = 0;
                totalEmptyCorners = 0;
                return value;
            }
            else
            {
                value = UnityEngine.Random.Range(0, 9);
                ni = 0;
                totalEmptyCorners = 0;
                return value;
            }

        }

        
    }




    /// <summary>
    /// exit game
    /// </summary>
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }




    /// <summary>
    /// load the menu
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    void SetGameControllerRefferenceOnButtons()
    {
        for (int i=0;i<buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerRefferenceTwoPlayers(this);
            //print("setting i : " + i + " grid space name : " + buttonList[i].GetComponentInParent<GridSpace>().name);

        }
    }

    public void SetStartingSide(string startingSide)
    {
        if(player1Move)
        {
            player1 = startingSide;
            player1Side = startingSide;
            currentSide = startingSide;
            if (player1 == "X")
            {
                player2 = "O";
                player2Side = "O";
                SetPlayerColors(playerX, playerO);
            }
            else
            {
                player2 = "X";
                player2Side = "X";
                SetPlayerColors(playerO, playerX);
            }
        }
        else
        {
            player2 = startingSide;
            player2Side = startingSide;
            currentSide = startingSide;
            if (player2 == "X")
            {
                player1 = "O";
                player1Side = "O";
                SetPlayerColors(playerX, playerO);
            }
            else
            {
                player1 = "X";
                player1Side = "X";
                SetPlayerColors(playerO, playerX);
            }
        }
        



        StartGame(true);

    }

    void StartGame(bool cleanStart=false)
    {
        SetBoardInteractable(true,cleanStart);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
        
        if(player1Move)
        {
            currentSide = GetPlayer1Side();
            moveInfoText.text = "Player-1's TURN!";
        }
        else
        {
            currentSide = GetPlayer2Side();
            moveInfoText.text = "Player-2's TURN!";
        }
        isGameOver = false;

        moveInfoPanel.SetActive(true);
        //print("current side = " + currentSide);
    }



    /// <summary>
    /// get player side mainly used by single player game
    /// </summary>
    /// <returns></returns>
    public string GetPlayerSide()
    {
        return playerSide;
    }

    /// <summary>
    /// get the player 1 s symbol in two player game
    /// </summary>
    /// <returns></returns>
    public string GetPlayer1Side()
    {
        return player1;
    }

    /// <summary>
    /// get the player 2 s symbol in two player game
    /// </summary>
    /// <returns></returns>
    public string GetPlayer2Side()
    {
        return player2;
    }

    /// <summary>
    /// getcomputerside mainly used to get computers symbol in single player mode
    /// </summary>
    /// <returns></returns>
    public string GetComputerSide()
    {
        return computerSide;
    }

    public void EndTurn()
    {
        ++moveCount;
        if (CheckWin())
        {
            return;
        }
        else
        {
            ChangeSide();
            if (player1Move)
            {
                
                moveInfoText.text = "Player-1's Turn!";
            }
            else
            {
                
                moveInfoText.text = "Player-2's Turn!";
            }
        }
    }

    bool CheckWin()
    {
        for(int i=0;i<buttonList.Length;i+=3) // check for row
        {
            if(buttonList[i].text==currentSide && buttonList[i+1].text == currentSide && buttonList[i+2].text == currentSide)
            {
                GameOver();
                return true;
            }
        }

        for (int i = 0; i < 3; i ++) // check for column
        {
            if (buttonList[i].text == currentSide && buttonList[i + 3].text == currentSide && buttonList[i + 6].text == currentSide)
            {
                GameOver();
                return true;
            }
        }

        if (buttonList[0].text == currentSide && buttonList[4].text == currentSide && buttonList[8].text == currentSide) //check for diagonal 1
        {
            GameOver();
            return true;
        }
        if (buttonList[2].text == currentSide && buttonList[4].text == currentSide && buttonList[6].text == currentSide) //check for diagonal 2
        {
            GameOver();
            return true;
        }

        if(moveCount>=9)
        {
            isDraw = true;
            GameOver();
            return true;
        }

        return false;


    }

    void SetPlayerColors(Player newPlayer,Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }


    void GameOver()
    {
        SetBoardInteractable(false);

        if(isDraw)
        {
            SetGameOverText("It's a DRAW !");
            SetPlayerColorsInactive();
            isDraw = false;
        }
        else
        {
            //SetGameOverText( currentSide + " Wins !");

        if(currentSide==player1)
            {
                SetGameOverText("Player-1 Wins");
            }
        else
            {
                SetGameOverText("Player-2 Wins");
            }
            
        }

        isGameOver = true;
    }

    void ChangeSide()
    {
        //playerSide = (playerSide == player1) ? player2 : player1;

        player1Move = (player1Move == true) ? false : true;
        player2Move = !player1Move;
        currentSide = (player1Move==true) ? player1 : player2;

        //if(playerSide=="X")
        if(currentSide=="X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);

        }
    }

    void SetGameOverText(string value)
    {
        gameOverText.text=value;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        
        moveCount = 0;
        isDraw = false;
        isGameOver = false;
        delay = 1f;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();

        SetBoardInteractable(false, true);
        startInfo.SetActive(true);
        moveInfoPanel.SetActive(false);


    }

    void SetBoardInteractable(bool toggle, bool resetBoard=false)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
            if(resetBoard)
            {
                buttonList[i].text = "";
            }

        }

        restartButton.SetActive(!toggle);
    }


    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    public string GetCurrentSide()
    {
        return currentSide;
    }
}
