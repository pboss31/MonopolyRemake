using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HouseInfoMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI houseName;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI priceToBuy;
    [SerializeField] private TextMeshProUGUI priceFall;
    [SerializeField] private Button cancel;

    private int ChoiceMade = 0;

    StateManager theStateManager;

    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }


    public void Cancel()
    {
        ChoiceMade = 1;
    }

    // Update is called once per frame
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

        houseName.text = building.GetComponent<Tile>().Name;
        level.text = "LEVEL" + building.Level.ToString();
        hp.text = "HP : " + building.Hp.ToString();

        if(building.Owner == -1)
        {
            priceToBuy.text = "Buy Price : " + building.Price;
            priceFall.text = "Fall Price : " + building.FallPrice();
            return;
        }
        switch (building.Level)
        {
            case 1:
                priceToBuy.text = "Up Price : " + building.UpgradePrice();
                priceFall.text = "Fall Price : " + building.FallPrice();
                break;
            case 2:
                priceToBuy.text = "Up Price : " + building.UpgradePrice();
                priceFall.text = "Fall Price : " + building.FallPrice();
                break;
            case 3:
                priceToBuy.text = "Up Price : MAX LVL";
                priceFall.text = "Fall Price : " + building.FallPrice();
                break;         
        }
    }
}
