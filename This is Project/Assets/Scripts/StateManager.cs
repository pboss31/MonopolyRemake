using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	void Start () {
        PlayerPosition = new int[NumberOfPlayers];
        DeadPlayer = new List<int>();
    }

    public float up1, up2, pay1, pay2, pay3, hp1, hp2, sell;

    public int NumberOfPlayers = 4;
    public int CurrentPlayerId = 0;
    public int[] PlayerPosition;
    public List<int> DeadPlayer;

    public int DiceTotal;

    public bool IsDoneRolling = false;
    public bool IsDoneAnimating = false;
    public bool IsBuying = false;
    public bool IsDoneActing = false;
    public bool IsUpgrading = false;
    public bool IsOpeningHouseMenu = false;
    public bool IsSelling = false;
    public bool CanAtk = false;
    public bool Die = false;

    public bool readdyForAtk = false;

    void Update () {
		if (IsDoneRolling && IsDoneAnimating && IsDoneActing)
        {
            Debug.Log("Turn is done! -----------------------------------------------");
            NewTurn();     
        }

        if (DeadPlayer.Count == 3)
        {
            GameObject.FindObjectOfType<WinMenu>().OpenWinMenu();
            Destroy(gameObject);
        }
	}

    public void NewTurn()
    {
        // start a next player turn
        IsDoneRolling = false;
        IsDoneAnimating = false;
        IsDoneActing = false;
        
        //switch to next player
        CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfPlayers;
        Debug.Log("RIGHT NOW " + CurrentPlayerId);
        if (DeadPlayer.Contains(CurrentPlayerId)) NewTurn();
    }
}
