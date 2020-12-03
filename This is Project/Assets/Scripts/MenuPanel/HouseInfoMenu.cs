using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HouseInfoMenu : MonoBehaviour
{
    StateManager theStateManager;

    public TextMeshProUGUI houseName;
    public TextMeshProUGUI level;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI priceToBuy;
    public TextMeshProUGUI priceFall;
    [SerializeField] private Button cancel;

    private int ChoiceMade = 0;

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }


    public void Cancel()
    {
        ChoiceMade = 1;
    }

    void Update()
    {
        if (theStateManager.IsOpeningHouseMenu == false) { return; }
        if (ChoiceMade >= 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            theStateManager.IsOpeningHouseMenu = false;
            ChoiceMade = 0;
        }
    }

    public void OpenHouseInfoMenu(Building building)
    {
        //open the menu and display name, level, hp, price, price_fall
        theStateManager.IsOpeningHouseMenu = true;
        transform.GetChild(0).gameObject.SetActive(true);

        houseName.text = building.HouseNo + "\n" + building.GetComponent<Tile>().Name;
        level.text = "LEVEL " + building.Level.ToString();
        hp.text = "Health " + building.Hp.ToString() + " points";

        if(building.Owner == -1)
        {
            priceToBuy.text = "Buy " + building.Price + " coins";
            priceFall.text = "Rent " + building.FallPrice() + " coins";
            return;
        }
        switch (building.Level)
        {
            case 1:
                priceToBuy.text = "Upgrade " + building.UpgradePrice() + " coins";
                priceFall.text = "Rent " + building.FallPrice() + " coins";
                break;
            case 2:
                priceToBuy.text = "Upgrade : " + building.UpgradePrice() + " coins";
                priceFall.text = "Rent " + building.FallPrice() + " coins";
                break;
            case 3:
                priceToBuy.text = "Upgrade : Max Level" + " coins";
                priceFall.text = "Rent : " + building.FallPrice() + " coins";
                break;         
        }
    }
}
