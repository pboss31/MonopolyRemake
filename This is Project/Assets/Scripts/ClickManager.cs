using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;

public class ClickManager : MonoBehaviour
{
    StateManager theStateManager;
    PlayerData[] thePlayerData;
    SellMenu sellMenu;
    private Transform _selection;

    void Start()
    {
        sellMenu = GameObject.FindObjectOfType<SellMenu>();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
    }

    void Update()
    {
        if(_selection != null)
        {
            _selection.GetComponent<Renderer>().material.color = Color.white;
            _selection = null;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //highlight tile
        if(Physics.Raycast(ray, out hit))
        {
            Transform selection = hit.transform;

            //!IsSelling
            if (selection.parent.CompareTag("House") &&  !theStateManager.IsSelling)
            {
                selection.GetComponent<Renderer>().material.color = Color.yellow;


                
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject()) return;
                    //if this object implements IClickable -> do OnClick()
                    IClickable clickable = selection.parent.GetComponent<IClickable>();
                    if (clickable != null)
                        clickable.OnClick();
                }

                _selection = selection;
            }
            
            //IsSelling
            else if (selection.parent.CompareTag("House") && theStateManager.IsSelling && selection.name.Equals("Tile-Visual"))
            {
                //only currentplayer house can be select
                if (thePlayerData[theStateManager.CurrentPlayerId].PlayerHouse.Contains(hit.transform.parent.gameObject))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (EventSystem.current.IsPointerOverGameObject()) return;
                        //determine select or deselect
                        //by checking if this object is in SellHolder List

                        //not in -> select
                        if (!sellMenu.SellHolder.Contains(hit.transform.parent.gameObject))
                        {
                            //change color to red
                            hit.transform.GetComponent<Renderer>().material.color = Color.red;
                            //add this object
                            sellMenu.AddHouse(hit.transform.parent.gameObject);
                        }

                        //already in -> deselect
                        else
                        {
                            //change color to yellow
                            hit.transform.GetComponent<Renderer>().material.color = Color.yellow;
                            //remove this object  
                            sellMenu.RemoveHouse(hit.transform.parent.gameObject);
                        }
                    }
                }
            }
        }

    }
}
