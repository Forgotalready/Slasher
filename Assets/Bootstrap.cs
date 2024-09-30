using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] Inventory _inventory;
    [SerializeField] CombatSystem _combatSystem;
    [SerializeField] PlayerModel _playerModel;
    [SerializeField] GameObject _player;
    [SerializeField] BasePlayerStats _playerStats;
    [SerializeField] Weapon _weapon;
    [SerializeField] CharacterInputController _characterInputController;

    private void Awake()
    {
        _playerMovement.Init();
        _characterInputController.Init();
        _combatSystem.Init();
        _playerModel.Init(_playerStats, _weapon);
        _inventory.Init(_playerMovement);
    }
}
