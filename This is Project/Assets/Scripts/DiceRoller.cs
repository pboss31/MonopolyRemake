using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DiceRoller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DiceValues = new int[2];
        thePlayerPiece = GameObject.FindObjectsOfType<PlayerPiece>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }
	
    PlayerPiece[] thePlayerPiece;
    StateManager theStateManager;

    public int[] DiceValues;

    public void RollDice()
    {
        if(theStateManager.IsDoneRolling == true) { return;  }

        //roll the dice!
        theStateManager.DiceTotal = 0;
        for (int i = 0; i < DiceValues.Length; i++)
        {
            
            DiceValues[i] = Random.Range(1, 7);
            theStateManager.DiceTotal += DiceValues[i];
            theStateManager.DiceTotal = 1;

            //update visual to show the dice roll

            theStateManager.IsDoneRolling = true;

        }
        
        thePlayerPiece[theStateManager.CurrentPlayerId].MovePlayer();
    }
}
