using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable, IPlayerController
{
    #region Variables

    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Transform _groundCheckPosition;
    [SerializeField] private float _checkerRadius;
    [SerializeField] private LayerMask _checkerLayerMask;
    [SerializeField] private bool useGravity = true;

    private CharacterController _characterController;
    private CombatSystem _combatSystem;
    private Vector2 _inputDirection;
    private Vector3 _moveDirection;
    private float _verticalSpeed = -1f;
    private Quaternion _rotation;

    private bool _onGround = false;
    private bool _canMove = true;

    private Animator _animator;

    public event Action<Item> itemCollected;


    #endregion

    #region Properties

    public float MovementSpeed => _playerConfig.movementSpeed;
    public float TurnSpeed => _playerConfig.turnSpeed;
    public float MovementSmoothing => _playerConfig.movementSmoothing;
    public float TurnSmoothing => _playerConfig.turnSmoothing;
    public float Gravity => _playerConfig.gravity;

    #endregion

    private void OnEnable()
    {
        _combatSystem.AttackStart += AttackStart;
        _combatSystem.AttackEnd += AttackEnd;
    }



    private void OnDisable()
    {
        _combatSystem.AttackStart -= AttackStart;
        _combatSystem.AttackEnd -= AttackEnd;
    }
    public void Init()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _combatSystem = GetComponent<CombatSystem>();
    }

    private void FixedUpdate()
    {
        CheckGrounding();
        if(_canMove)
        {
            Move();
        }

        Rotate();
        
        if(useGravity)
        {
            DoGravity();
        }
        AnimationControll();
    }


    public void InputUpdate(Vector2 inputDirection)
    {
        _inputDirection = inputDirection;  
    }

    private void Move()
    {
        _moveDirection = Vector3.Lerp(_moveDirection, new Vector3(_inputDirection.x, 0f, _inputDirection.y), MovementSmoothing);
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 relativeDirection = matrix.MultiplyPoint3x4(_moveDirection);

        var relativeRotation = (transform.position + relativeDirection) - transform.position;
        _rotation = Quaternion.LookRotation(relativeRotation, Vector3.up);

        _characterController.Move(relativeDirection * MovementSpeed * Time.fixedDeltaTime);
        
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, TurnSpeed * TurnSmoothing * Time.deltaTime);
    }

    private void CheckGrounding()
    {
        if(Physics.CheckSphere(_groundCheckPosition.position, _checkerRadius, _checkerLayerMask))
        {
            _onGround = true;
        }
        else
        {
            _onGround = false;
        }
    }

    private void DoGravity()
    {
        if(!_onGround)
        {
            _verticalSpeed -= Gravity * Time.deltaTime;
        }
        else
        {
            _verticalSpeed = -1f;
        }
        _characterController.Move(Vector3.up * _verticalSpeed * Time.deltaTime);
    }

    private void AnimationControll()
    {
        _animator.SetFloat("speed", _moveDirection.magnitude * MovementSpeed);
    }

    private void AttackStart()
    {
        _canMove = false;

        var inputDiraction = new Vector3(_inputDirection.x, 0f, _inputDirection.y);
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 relativeDirection = matrix.MultiplyPoint3x4(inputDiraction);
        var relativeRotation = (transform.position + relativeDirection) - transform.position;
        _rotation = Quaternion.LookRotation(relativeRotation, Vector3.up);

    }
    private void AttackEnd()
    {
        _canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Item")) 
        {
            Destroy(collision.gameObject);
            itemCollected?.Invoke(collision.gameObject.GetComponent<ItemComponent>().Item);
        }
    }
}
