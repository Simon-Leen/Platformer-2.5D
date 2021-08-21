using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] GameObject _respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            CharacterController cc = other.GetComponent<CharacterController>();
            if(player != null)
            {
                player.UpdateLives(-1);
                
            }
            if(cc != null)
            {
                cc.enabled = false;
            }
            other.transform.position = _respawnPoint.transform.position;
            StartCoroutine(ControllerEnabled(cc));
        }
    }

    IEnumerator ControllerEnabled(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
