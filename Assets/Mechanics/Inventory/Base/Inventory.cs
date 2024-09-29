using System;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
  [SerializeField] private List<Item> _items;
  [SerializeField] private int _bloodId = 2;

  private int _inventoryCapacity = 5;
  private int _inventoryAmount = 0;
  private IPlayerController _playerController;

  [SerializeField] private GameObject _cellContainer;

  private bool isFirstRender = true;
  //[SerializeField] private GameObject _cellPrototype;
  //[SerializeField] private GameObject _bloodCellPrototype;
  [SerializeField] private Transform _parentTransform;

  [SerializeField] private List<GameObject> _inventoryView;
  
  public int Capacity
  {
    get => _inventoryCapacity;
  }

  public void AddItem(Item item)
  {
    if (item.Id == _bloodId)
    {
      _items[0].ItemData.Amount += 1;
      Render();
      return;
    }

    if (_inventoryAmount + 1 <= _inventoryCapacity)
    {
      _items.Add(item);
      _inventoryAmount++;
    }
    else
    {
      throw new InvalidOperationException("Inventory out of bounds");
    }

    Render();
  }

  public void DeleteItem(Item item)
  {
    RecalculateList();
    
    if (_inventoryAmount - 1 < 0)
    {
      throw new InvalidOperationException("Inventory out of bounds");
    }

    int elemIndex = 0;
    for (int i = 0; i < _items.Count; i++)
    {
      if (_items[i] == item)
      {
        elemIndex = i;
        break;
      }
    }

    _items.RemoveAt(elemIndex);
    _inventoryAmount--;

    _inventoryView[elemIndex].GetComponent<ICell>().Item = null;
    
    Render();
  }

  private void Render()
  {
    if (!isFirstRender)
      RecalculateList();


    InstantiateBloodCell();

    for (int i = 1; i < _inventoryCapacity; i++)
    {

      InventoryCell cell = _inventoryView[i].GetComponent<InventoryCell>();
      cell.Init(_parentTransform);
      cell.Eject += OnEject(cell);
      if (i >= _items.Count) cell.Render(null);
      else cell.Render(_items[i]);
    }

    isFirstRender = false;
  }

  private void RecalculateList()
  {
    List<Item> newList = new();
    foreach (GameObject obj in _inventoryView)
    {
      newList.Add(obj.GetComponent<ICell>().Item);
    }

    _items = newList;
  }

  private Action<Item> OnEject(InventoryCell cell)
  {
    return (Item deletedElem) =>
    {
      //Destroy(cell.gameObject);
      foreach (Item elem in _items)
      {
        if (elem == deletedElem)
        {
          DeleteItem(elem);
          break;
        }
      }
    };
  }
  

  private void InstantiateBloodCell()
  {
    ICell cell = _inventoryView[0].GetComponent<ICell>();
    cell.Render(_items[0]);
  }

  public void Init(IPlayerController playerController)
  {
    _inventoryAmount = _items.Count;

    _playerController = playerController;

    _playerController.itemCollected += AddItem;

    Render();
  }
}