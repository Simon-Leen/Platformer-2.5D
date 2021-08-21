using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _targetA, _targetB;
    private float _speed = 2.0f;
    private bool _switching = false;

    void FixedUpdate()
    {
        if(!_switching)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if(_switching)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }

        if(transform.position == _targetA.position)
        {
            _switching = false;
        }
        else if (transform.position == _targetB.position)
        {
            _switching = true;
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
