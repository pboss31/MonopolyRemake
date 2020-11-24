using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerPiece : MonoBehaviour {

    StateManager theStateManager;
    PlayerAction[] thePlayerAction;

    // Use this for initialization
    void Start ()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        targetPosition = this.transform.position;
        thePlayerAction = GameObject.FindObjectsOfType<PlayerAction>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
    }

    [SerializeField] private Tile currTile;
    public Tile CurrTile { get => currTile; set => currTile = value; }

    public int PlayerId;

    Tile[] moveQueue;
    int moveQueueIndex;
    int spacesToMove;

    bool isAnimating = false;

    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.15f;
    float smoothTimeVertical = 0.1f;
    float smoothDistance = 0.01f;
    float smoothHeight = 0.5f; //0.5f

    void Update()
    {
        if (isAnimating == false) { return; }

        if (Vector3.Distance( new Vector3(this.transform.position.x, targetPosition.y, this.transform.position.z), targetPosition) < smoothDistance)
        {
            // we have reached that target. how's our height?

            //going down
            if (moveQueueIndex == moveQueue.Length && this.transform.position.y > smoothDistance)
            {
                this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(this.transform.position.x, 0, this.transform.position.z),ref velocity, smoothTimeVertical);
            }
            else
            {
                //right pos,  right height -> advance the cube
                AdvanceMoveQueue();
            }
        }
        
        else if (this.transform.position.y < (smoothHeight - smoothDistance))
        {
            //going up
            this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(this.transform.position.x, smoothHeight, this.transform.position.z), ref velocity, smoothTimeVertical);
        }
        
        else
        {
            //move
            this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(targetPosition.x, smoothHeight, targetPosition.z), ref velocity, smoothTime);
        }
        
    }

    void AdvanceMoveQueue()
    {
        // moveQueue != null 
        if (moveQueueIndex < moveQueue.Length)
        {
            SetNewTargetPosition(moveQueue[moveQueueIndex].transform.position);
            moveQueueIndex++;
        }
        //movement = null, so we are done animating
        else
        {         
            Debug.Log("DONE advancing!!");
            this.isAnimating = false;

            //doing some actions when done animating
            thePlayerAction[theStateManager.CurrentPlayerId].OnCall(); 
        }
    }

    void SetNewTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        //velocity = Vector3.zero;
    }

    //happen right after DiceRoller is done
    public void MovePlayer()
    {
        //is this the correct player
        if (theStateManager.CurrentPlayerId != PlayerId) { return; }

        //have we rolled?
        if (theStateManager.IsDoneRolling == false) { return; }

        //set spacestomove
        spacesToMove = theStateManager.DiceTotal;

        //where should we end up
        moveQueue = new Tile[spacesToMove];
        Tile finalTile = currTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            finalTile = finalTile.NextTile[0];
            moveQueue[i] = finalTile;
        }

        moveQueueIndex = 0;
        currTile = finalTile;
        this.isAnimating = true;


        Debug.Log("Player: " + theStateManager.CurrentPlayerId + " | CurrTiles = " + currTile + " | " + currTile.Id + " " + currTile.Name + " " + currTile.GetComponent<Building>().Price);
    }

}
