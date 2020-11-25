using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceTotalDisplay : MonoBehaviour {

    private void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    StateManager theStateManager;

	// Update is called once per frame
	void Update ()
    {
        GetComponent<Text>().text = theStateManager.DiceTotal.ToString();
	}
}
