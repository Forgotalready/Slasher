using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, ICell, IDragHandler, IBeginDragHandler, IEndDragHandler
{

  public event Action<Item> Eject;
  [SerializeField] private Image _icon;
  [SerializeField] private TMP_Text _text;
  private Item _item;
  private Vector3 startPosition;

  public Item Item
  {
    get => _item;
    set => _item = value;
  }

  private int _renderElemId;

  private Transform _dragParent;
  private int startDragIndex;
  public void Init(Transform dragParent)
  {
    _dragParent = dragParent;
  }

  public void Render(Item item)
  {
    _item = item;
    if (item == null)
    {
      Color iconColor = _icon.color;
      iconColor.a = 0.0f;
      _icon.color = iconColor;
      _text.text = "";
      return;
    }
    else
    {
      Color iconColor = _icon.color;
      iconColor.a = 255.0f;
      _icon.color = iconColor;
    }

    _icon.sprite = item.ItemData.Icon;
    _text.text = (item.ItemData.Amount == 0) ? "" : item.ItemData.Amount.ToString();
    _renderElemId = item.Id;
  }

  public void OnDrag(PointerEventData eventData)
  {
    _icon.transform.position = Input.mousePosition;
  }

  public void OnBeginDrag(PointerEventData eventData)
  {

    startDragIndex = 0;
    startPosition = _icon.transform.position;
    foreach (Transform child in _dragParent)
    {
      if (child != _icon.transform.parent) startDragIndex += 1;
      else break;
    }

    _icon.transform.SetParent(_dragParent);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform) _dragParent, _icon.transform.position))
    {
      InsertInParent();
    }
    else
    {
      _icon.transform.SetParent(_dragParent.GetChild(startDragIndex));
      _icon.transform.position = startPosition;
      Eject?.Invoke(Item);
    }
  }

  private void InsertInParent()
  {
    int closestElemIndex = 0;
    for (int i = 0; i < _dragParent.childCount; i++)
    {
      if (Vector3.Distance(_icon.transform.position, _dragParent.GetChild(i).position) <
          Vector3.Distance(_icon.transform.position, _dragParent.GetChild(closestElemIndex).position) && _icon.transform != _dragParent.GetChild(i)
         )
      {
        closestElemIndex = i;
      }
    }
    

    if (closestElemIndex == 0)
      SwapWithElem(1);
    else
      SwapWithElem(closestElemIndex);
  }

  private void SwapWithElem(int closestElemIndex)
  {
    _icon.transform.SetParent(_dragParent.GetChild(startDragIndex));
    _icon.transform.position = startPosition;

    InventoryCell changedCell = _dragParent.GetChild(closestElemIndex).gameObject.GetComponent<InventoryCell>();
    InventoryCell startCell = _dragParent.GetChild(startDragIndex).GetComponent<InventoryCell>();

    Item temp = changedCell.Item;
    changedCell.Item = startCell.Item;
    startCell.Item = temp;

    changedCell.Render(changedCell.Item);
    startCell.Render(startCell.Item);
  }
}