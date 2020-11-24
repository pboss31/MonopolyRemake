using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPosition = new int[NumberOfPlayers];
    }

    public float up1, up2, pay1, pay2, pay3, hp1, hp2, sell;

    public int NumberOfPlayers = 4;
    public int CurrentPlayerId = 0;
    public int[] PlayerPosition;

    public int DiceTotal;

    public bool IsDoneRolling = false;
    public bool IsDoneAnimating = false;
    public bool IsBuying = false;
    public bool IsDoneActing = false;
    public bool IsUpgrading = false;

    public void NewTurn()
    {
        // start a next player turn
        IsDoneRolling = false;
        IsDoneAnimating = false;
        IsDoneActing = false;
        
        //switch to next player
        CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfPlayers;
    }


    // Update is called once per frame
    void Update () {
		if (IsDoneRolling && IsDoneAnimating && IsDoneActing)
        {
            Debug.Log("Turn is done! -----------------------------------------------");
            NewTurn();
        }
	}
}
