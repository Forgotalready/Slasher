using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class BloodInventoryCell : MonoBehaviour, ICell
{
  [SerializeField] private Image _icon;
  [SerializeField] private TMP_Text _text;
  private Item _item;
  public void Render(Item item)
  {
    _icon.sprite = item.ItemData.Icon;
    _text.text = (item.ItemData.Amount == 0) ? "" : item.ItemData.Amount.ToString();
    _item = item;
  }

  public Item Item { get=>_item; set=>_item = value; }
}