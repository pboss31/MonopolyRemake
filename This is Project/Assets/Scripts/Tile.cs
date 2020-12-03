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

    public Tile[] NextTile { get => _nextTile; set => _nextTile = value; }
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public int BasePrice { get => _basePrice; set => _basePrice = value; }
    public int BaseHp { get => _baseHp; set => _baseHp = value; }   

    void Awake()
    {
        _nextTile = new Tile[1];
    }

}
