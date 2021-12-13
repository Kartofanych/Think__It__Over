using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class second_lvl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform tr;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(tr.forward * 2000f);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 2000f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "jump_harder")
        {
            GetComponent<Rigidbody>().AddForce(tr.forward * 2000f);
            GetComponent<Rigidbody>().AddForce(Vector3.up * 500f);
        }
    }
}
