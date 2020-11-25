using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tile[] _nextTile;
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _basePrice;
    [SerializeField] private int _baseHp;
    //[SerializeField] private bool _isBuildable = true;
    //[SerializeField] private bool _isUpgradeable = false;

    public Tile[] NextTile { get => _nextTile; set => _nextTile = value; }
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public int BasePrice { get => _basePrice; set => _basePrice = value; }
    public int BaseHp { get => _baseHp; set => _baseHp = value; }
    //public bool IsBuildable { get => _isBuildable; set => _isBuildable = value; }  
    //public bool IsUpgradeable { get => _isUpgradeable; set => _isUpgradeable = value; }
    

    void Awake()
    {
        _nextTile = new Tile[1];
    }

}
