using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int _state;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals(_state.ToString()))
        {
            GetComponent<MeshRenderer>().material = other.gameObject.GetComponent<MeshRenderer>().material;
        }
    }
}
