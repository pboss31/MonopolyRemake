using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    public InputField Player1Field;
    public InputField Player2Field;
    public InputField Player3Field;
    public InputField Player4Field;

    public Button StartButton;

    public void callStart()
    {
        transform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Player1Field.text.Length > 0 && Player2Field.text.Length > 0 && Player3Field.text.Length > 0 && Player4Field.text.Length > 0)
        {
            StartButton.interactable = true;
        }
    }
}

