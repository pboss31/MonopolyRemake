using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceTotalDisplay : MonoBehaviour {

    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    StateManager theStateManager;

	void Update ()
    {
        GetComponent<Text>().text = theStateManager.DiceTotal.ToString();
	}
}
