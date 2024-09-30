using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFillSync : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _textMeshPro;
    [SerializeField] PlayerModel _playerModel;

    void Update()
    {
        int hp = _playerModel.Health;
        _slider.value = hp;
        _textMeshPro.text = _slider.value.ToString() + "%";
    }
}
