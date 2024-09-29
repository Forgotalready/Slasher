using System;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
  [SerializeField] private Item _item;

  public Item Item
  {
    get => _item;
  }
}
