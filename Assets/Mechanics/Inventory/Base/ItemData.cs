using UnityEngine;
using System;

[Serializable]
public class ItemData : IItemData
{
  [SerializeField] private string _name;
  [SerializeField] private Sprite _icon;
  [SerializeField] private int _startAmount;
  private int _nowAmount;
  
  public string Name
  {
    get => _name;
    set => _name = value;
  }

  public Sprite Icon
  {
    get => _icon;
    set => _icon = value;
  }

  public int Amount
  {
    get => _nowAmount;
    set => _nowAmount = value;
  }

  public void OnEnable()
  {
    _nowAmount = _startAmount;
  }

  public IItemData Clone()
  {
    return new ItemData
    { 
            Name = this.Name,
            Icon = this.Icon,
            Amount = this.Amount
    };
  }
}
