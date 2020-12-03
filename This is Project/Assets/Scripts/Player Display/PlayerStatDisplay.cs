using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class PlayerStatDisplay : MonoBehaviour
{
    protected PlayerNameInput thePlayerName;
    protected PlayerData[] thePlayerData;

    [SerializeField] protected TextMeshProUGUI playerName;
    [SerializeField] protected TextMeshProUGUI statVal;

    protected virtual void Start()
    {
        thePlayerData = GameObject.FindObjectsOfType<PlayerData>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray();
        thePlayerName = GameObject.FindObjectOfType<PlayerNameInput>();
    }

    protected virtual void Update()
    {
        
    }
}
