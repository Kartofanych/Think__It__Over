using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForShar : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 spawn;
    void Start()
    {
        spawn = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = spawn;
        Quaternion quaternion = Quaternion.AngleAxis(1, new Vector3(1,1,1));
        transform.rotation = transform.rotation * quaternion;
    }
}
