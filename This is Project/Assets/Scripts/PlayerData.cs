using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : MonoBehaviour, ITakeDamage
{
    StateManager theStateManager;

    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private int coin;
    [SerializeField] private string playerClass;
    [SerializeField] private List<GameObject> playerHouse;
    [SerializeField] private int playerId;
    [SerializeField] private string playerName;
    AudioSource sound;

    public int Hp { get => hp; set => hp = value; }
    public int Atk { get => atk; set => atk = value; }
    public int Coin { get => coin; set => coin = value; }
    public string Class { get => playerClass; set => playerClass = value; }
    public List<GameObject> PlayerHouse { get => playerHouse; set => playerHouse = value; }
    public string Name { get => playerName; set => playerName = value; }

    private bool enableUpdate = false;

    void Start()
    {
        StartCoroutine(GetCharacter());
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        playerHouse = new List<GameObject>();
        sound = GetComponent<AudioSource>();
    }

    IEnumerator GetCharacter()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/monopoly/GetCharacter.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string cInfo = www.downloadHandler.text;
            //split data to row -> Berserkser 150 120 300, ....
            string[] row = cInfo.Split('\n');
            foreach (string i in row)
            {
                string[] data = i.Split(' ');

                if (gameObject.name == "Player1-Piece" && data[0] == "Berserker")
                {
                    playerClass = data[0];
                    hp = int.Parse(data[1]);
                    atk = int.Parse(data[2]);
                    coin = int.Parse(data[3]);
                    playerId = 0;
                }
                else if (gameObject.name == "Player2-Piece" && data[0] == "Merchant")
                {
                    playerClass = data[0];
                    hp = int.Parse(data[1]);
                    atk = int.Parse(data[2]);
                    coin = int.Parse(data[3]);
                    playerId = 1;
                }
                else if (gameObject.name == "Player3-Piece" && data[0] == "Tank")
                {
                    playerClass = data[0];
                    hp = int.Parse(data[1]);
                    atk = int.Parse(data[2]);
                    coin = int.Parse(data[3]);
                    playerId = 2;
                }
                else if (gameObject.name == "Player4-Piece" && data[0] == "Assassin")
                {
                    playerClass = data[0];
                    hp = int.Parse(data[1]);
                    atk = int.Parse(data[2]);
                    coin = int.Parse(data[3]);
                    playerId = 3;
                } 
            }
            enableUpdate = true;
        }
    }

    void Update()
    {
        if (enableUpdate == false) return;
        //check if player is dead
        if (hp <= 0 || coin < 0)
        {
            theStateManager.CanAtk = false;
            theStateManager.PlayerPosition[playerId] = -1;
            theStateManager.DeadPlayer.Add(playerId);
            theStateManager.Die = true;
            Debug.Log(playerName + " is dead");

            List<GameObject> houseHolder = new List<GameObject>();
            houseHolder.AddRange(playerHouse);

            //delete all of this currplayer house
            foreach (GameObject house in houseHolder)
            {
                house.GetComponent<Building>().SellHouse();
            }

            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        sound.Play();
        hp -= dmg;
    }

    public void Pay(int payAmount, PlayerData player)
    {
        //player coin decrease 
        coin -= payAmount;
        //player coin increase
        player.Coin += payAmount;
    }
}
