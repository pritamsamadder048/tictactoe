using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    public Button button;
    public Text buttonText;
    
    public GameController gameController;
    public TwoPlayersGameController twoPlayersGameController;
    /// <summary>
    /// //Single player grid space controller
    /// </summary>
    public void SetSpace()
    {
        if (gameController.playerMove)
        {
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.EndTurn();
        }
    }

    public void SetGameControllerRefference(GameController controller)
    {
        gameController = controller;
    }

    /// <summary>
    /// Two Player gridspace controller
    /// </summary>
    public void SetSpaceTwoPlayers()
    {
        
        buttonText.text = twoPlayersGameController.GetCurrentSide();
        button.interactable = false;
        twoPlayersGameController.EndTurn();
        //print("you clicked : " + button.name);
        
    }

    public void SetGameControllerRefferenceTwoPlayers(TwoPlayersGameController controller)
    {
        twoPlayersGameController = controller;
        //print("button name : " + button.name);
        
    }
}
