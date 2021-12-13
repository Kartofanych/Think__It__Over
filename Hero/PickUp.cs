using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUp : MonoBehaviour
{
    public Transform Destination;

    private void OnMouseDown()
    {
        if (Vector3.Distance(Destination.position, transform.position) < 3f)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = Destination.position;
            transform.parent = GameObject.Find("Hands").transform;
        }

    }

    private void OnMouseDrag()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.parent = null;
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(Destination.GetComponentInParent<Transform>().forward * 0.8f, ForceMode.Impulse);
        }
    }

    private void OnMouseUp()
    {
        transform.parent = null;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
