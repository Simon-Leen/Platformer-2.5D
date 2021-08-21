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
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;
        if(_controller.enabled == true)
        {
            if (_controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity = _jump;
                    _canDoubleJump = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
                {
                    _yVelocity += _jump;
                    _canDoubleJump = false;
                }
                _yVelocity -= _gravity;
            }
            velocity.y = _yVelocity;
            _controller.Move(velocity * Time.deltaTime);
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
}
