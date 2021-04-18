using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float _jumpHeight = 1.0f;

    private CharacterController _playerController;
    private Player _playerInput;
    
    private void Awake()
    {
        _playerInput = new Player();
        _playerController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);

        _groundedPlayer = _playerController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        var movementInput = _playerInput.PlayerMain.Move.ReadValue<Vector2>();
        var move = new Vector3(movementInput.x, 0, movementInput.y);
        _playerController.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (_playerInput.PlayerMain.Jump.triggered && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _playerController.Move(_playerVelocity * Time.deltaTime);
    }
}