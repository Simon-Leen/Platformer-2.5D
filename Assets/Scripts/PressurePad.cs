using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Moveable")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if(distance < 0.2f)
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.isKinematic = true;
                }

                MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
                if(mr != null)
                {
                    mr.material.color = Color.green;
                }

                Destroy(this);
            }
        }
    }
}
