using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jump = 22.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField] private int _coins;
    private UIManager _uIManager;
    private int _lives = 3;
    private Vector3 _direction, _velocity;
    private bool _canWallJump = false;
    private Vector3 _wallSurfaceNormal;
    private float _pushPower = 2.0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uIManager == null)
        {
            Debug.LogError("UIManager null in Player");
        }
        _uIManager.UpdateCoinsDisplay(_coins);
        _uIManager.UpdateLivesDisplay(_lives);
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if(_controller.enabled == true)
        {
            if (_controller.isGrounded)
            {
                _canWallJump = false;
                _direction = new Vector3(horizontalInput, 0, 0);
                _velocity = _direction * _speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity = _jump;
                    _canDoubleJump = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
                {
                    if(_canDoubleJump == true)
                    {
                        _yVelocity += _jump;
                        _canDoubleJump = false;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
                {
                    if(_canDoubleJump == true)
                    {
                        _canDoubleJump = false;
                    }
                    _yVelocity = _jump;
                    _velocity = _wallSurfaceNormal * _speed;
                }
                _yVelocity -= _gravity;
            }
            _velocity.y = _yVelocity;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Moveable")
        {
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if(rb != null)
            {
                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, 0);
                rb.velocity = pushDirection * _pushPower;
            }
        }
        if(_controller.isGrounded == false && hit.transform.tag == "Wall")
        {
            //Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }
    }

    public void AddCoins()
    {
        _coins++;
        _uIManager.UpdateCoinsDisplay(_coins);
    }

    public void UpdateLives(int lives)
    {
        _lives += lives;

        if (_lives < 1 )
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            _uIManager.UpdateLivesDisplay(_lives);
        }
    }

    public int Coins()
    {
        return _coins;
    }
}
