using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool _down = false;
    [SerializeField] private Transform _origin, _target;
    private float _speed = 2.0f;
    [SerializeField] private MeshRenderer _callButton;

    public void GoingDown(bool direction)
    {
        _down = direction;
    }

    private void FixedUpdate()
    {
        if(_down == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else if(_down == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _origin.position, _speed * Time.deltaTime);
        }
        if(transform.position == _target.position)
        {
            _callButton.material.color = Color.green;
        }
        else if(transform.position == _origin.position)
        {
            _callButton.material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
