using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private Transform _selection;

    private void Update()
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
            if (selection.parent.CompareTag("House"))
            {
                selection.GetComponent<Renderer>().material.color = Color.yellow;
                //if clickable -> open house info menu
                if (Input.GetMouseButtonDown(0))
                {
                    IClickable clickable = selection.parent.GetComponent<Building>();
                    clickable.OnClick();
                }
                _selection = selection;
            }
            
        }
    }
}
