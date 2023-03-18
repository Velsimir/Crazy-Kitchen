using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectCounter;
    }

    [SerializeField] private float _speedMoving;
    [SerializeField] private float _speedRotation;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _counterLayerMask;
    [SerializeField] private Transform _counterTopPoint;

    public event EventHandler OnPickSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public static Player Instance { get; private set; }

    private float _playerRadius = .7f;
    private float _playerHeigh = 2f;
    private float _interactDistance = 1f;
    private float _moveDistance;
    private bool _canMove;
    private bool _isWalking;
    private Vector2 _inputVector;
    private Vector3 _moveDirection;
    private Vector3 _lastInteractDirection;
    private RaycastHit _rayCastHit;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Player");

        Instance = this;
    }

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void OnDisable()
    {
        _gameInput.OnInteractAction -= GameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction -= GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (GameStates.Instance.IsGamePlaying() == false) return;

        if (_selectedCounter != null)
                _selectedCounter.InteractAlternate(this);
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (GameStates.Instance.IsGamePlaying() == false) return;

        if (_selectedCounter != null)
                _selectedCounter.Interact(this);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void HandleInteraction()
    {
        _inputVector = _gameInput.GetMovementVectorNormalized();

        _moveDirection = new Vector3(_inputVector.x, 0f, _inputVector.y);

        if (_moveDirection != Vector3.zero)
            _lastInteractDirection = _moveDirection;

        if (Physics.Raycast(transform.position, _lastInteractDirection, out _rayCastHit, _interactDistance, _counterLayerMask))
        {
            if (_rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter)
                    SetSelectedCounter(baseCounter);
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        _selectedCounter = baseCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectCounter = _selectedCounter
        });
    }

    private void HandleMovement()
    {
        _inputVector = _gameInput.GetMovementVectorNormalized();

        _moveDirection = new Vector3(_inputVector.x, 0f, _inputVector.y);

        _moveDistance = _speedMoving * Time.deltaTime;

        _canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeigh,
        _playerRadius, _moveDirection, _moveDistance);

        if (_canMove == false)
        {
            Vector3 moveDirectionX = new Vector3(_moveDirection.x, 0, 0).normalized;

            _canMove = (_moveDirection.x < -0.5f || _moveDirection.x > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeigh,
        _playerRadius, moveDirectionX, _moveDistance);

            if (_canMove)
                _moveDirection = moveDirectionX;
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, _moveDirection.z).normalized;

                _canMove = (_moveDirection.z < -0.5f || _moveDirection.z > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeigh,
        _playerRadius, moveDirectionZ, _moveDistance);

                if (_canMove)
                    _moveDirection = moveDirectionZ;
            }
        }

        if (_canMove)
            transform.position += _moveDirection * _speedMoving * Time.deltaTime;

        _isWalking = _moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, _moveDirection, _speedRotation * Time.deltaTime);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKithcenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;

        if (kitchenObject != null)
            OnPickSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKithcenObject()
    {
        return _kitchenObject;
    }

    public void ClearKithcenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKithcenObject()
    {
        return _kitchenObject != null;
    }
}