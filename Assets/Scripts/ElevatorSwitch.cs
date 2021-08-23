using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    [SerializeField] private MeshRenderer _callButton;
    private int _requiredCoins = 8;
    private Elevator _elevator;

    private void Start()
    {
        _callButton.material.color = Color.red;
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
        if(_elevator == null)
        {
            Debug.LogError("Elevator null in Elevator Switch");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && (player.Coins() >= _requiredCoins) && (_callButton.material.color == Color.red))
                {
                    _callButton.material.color = Color.blue;
                    _elevator.GoingDown(true);
                }
                else if (Input.GetKeyDown(KeyCode.E) && (player.Coins() >= _requiredCoins) && (_callButton.material.color == Color.green))
                {
                    _callButton.material.color = Color.blue;
                    _elevator.GoingDown(false);
                }
            }
        }
    }
}
