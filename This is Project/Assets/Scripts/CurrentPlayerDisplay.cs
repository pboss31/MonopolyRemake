using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerDisplay : MonoBehaviour
{

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerName = GameObject.FindObjectOfType<PlayerNameInput>();
        myText = GetComponent<Text>();
    }

    StateManager theStateManager;
    PlayerNameInput thePlayerName;
    Text myText;

    void Update()
    {
        string[] numberNames = { thePlayerName.Player1Field.text, thePlayerName.Player2Field.text, thePlayerName.Player3Field.text, thePlayerName.Player4Field.text };
        myText.text = "Current Player: " + numberNames[theStateManager.CurrentPlayerId];
    }
}