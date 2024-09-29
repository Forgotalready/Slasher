using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private List<AttackSO> _comboAttacks;
    [SerializeField] private List<GameObject> _vfxSlashes;

    private Vector2 _inputDirection;

    #region Variables
    private CharacterInputController _characterInputController;
    private Animator _animator;
    private int _comboCounter = 0;
    private bool _canAttack = true;

    #endregion

    #region Properties

    public float DelayBetweenAttacks => _playerConfig.delayBetweenAttacks;
    public float ComboCooldown => _playerConfig.comboCooldown;


    #endregion

    #region Actions
    public Action AttackStart;
    public Action AttackEnd;
    #endregion

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _characterInputController = GetComponent<CharacterInputController>();
        _animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        _characterInputController.GameInput.Gameplay.Attack.performed += Attack;
    }

    private void OnDisable()
    {
        _characterInputController.GameInput.Gameplay.Attack.performed -= Attack;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        AttackStart?.Invoke();
        _inputDirection = _characterInputController.GameInput.Gameplay.Movement.ReadValue<Vector2>();
        if (_comboCounter < _comboAttacks.Count)
        {
            if (_canAttack)
            {
                _canAttack = false;
                _animator.runtimeAnimatorController = _comboAttacks[_comboCounter].animatorOverrideController;
                _animator.Play("Attack", 0, 0);
                _animator.speed = _comboAttacks[_comboCounter].attackSpeed;
            }
        }
    }

    private void AllowToAttack()
    {
        _animator.speed = 0.5f;
        _canAttack = true;
    }


    private void EndCombo()
    {
        _comboCounter = 0;
        _canAttack = true;
        _animator.speed = 1;
        _animator.SetTrigger("AttackEnd");
        DisableSlashes();
        AttackEnd?.Invoke();
    }
    private void ActivateSlahs()
    {
        _vfxSlashes[_comboCounter].SetActive(true);
        _comboCounter++;
    }

    private void DisableSlashes()
    {
        foreach (var slash in _vfxSlashes)
        {
            slash.SetActive(false);
        }
    }
}
