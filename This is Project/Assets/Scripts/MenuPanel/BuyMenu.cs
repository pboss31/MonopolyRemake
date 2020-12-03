using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BuyMenu : MonoBehaviour
{
    StateManager theStateManager;
    PlayerPiece[] thePlayerPiece;
    PlayerData[] thePlayerData;

    private Tile currTile;
    [SerializeField] private GameObject buyBtn;
    [SerializeField] private GameObject cancelBtn;
    public Text tilePriceDisplay;
    public AudioSource sound;

    private int choiceMade = 0;

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerPiece = GameObject.FindObjectsOfType<PlayerPiece>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        sound = GetComponent<AudioSource>();
    }

    public void Buy()
    {
        sound.Play();
        currTile.GetComponent<Building>().BuyHouse();
        choiceMade = 1;
    }

    public void Cancel()
    {
        choiceMade = 2;
    }

    void Update()
    {
        if(theStateManager.IsBuying == false) { return; }
        if (choiceMade >= 1)
        {
            //close the menu
            transform.GetChild(0).gameObject.SetActive(false);
            theStateManager.IsBuying = false;
            //theStateManager.IsDoneActing = true;
            theStateManager.readdyForAtk = true;
            choiceMade = 0;
        }
    }

    public void OpenBuyMenu()
    {
        currTile = thePlayerPiece[theStateManager.CurrentPlayerId].CurrTile;
        //open the menu and display price
        theStateManager.IsBuying = true;
        transform.GetChild(0).gameObject.SetActive(true);
        tilePriceDisplay.text = "Price : " + currTile.GetComponent<Building>().Price;
    }
}
