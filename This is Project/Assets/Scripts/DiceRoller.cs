using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DiceRoller : MonoBehaviour {

    AudioSource sound;

	void Start () {
        DiceValues = new int[2];
        thePlayerPiece = GameObject.FindObjectsOfType<PlayerPiece>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        sound = GetComponent<AudioSource>(); 
    }
	
    PlayerPiece[] thePlayerPiece;
    StateManager theStateManager;
    
    public int[] DiceValues;
    
    public void RollDice()
    {
        if(theStateManager.IsDoneRolling == true) { return;  }

        //roll the dice!
        sound.Play();
        theStateManager.DiceTotal = 0;
        for (int i = 0; i < DiceValues.Length; i++)
        {
            
            DiceValues[i] = Random.Range(1, 7);
            theStateManager.DiceTotal += DiceValues[i];

            theStateManager.IsDoneRolling = true;

        }
        
        thePlayerPiece[theStateManager.CurrentPlayerId].MovePlayer();
    }
    
    /*
    public int FixedRoll;
    public void RollDice()
    {
        if (theStateManager.IsDoneRolling == true) { return; }
        sound.Play();
        theStateManager.DiceTotal = FixedRoll;
        theStateManager.IsDoneRolling = true;
        thePlayerPiece[theStateManager.CurrentPlayerId].MovePlayer();
    }*/
}
