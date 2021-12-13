using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bots_for_training : MonoBehaviour
{
    public bool dead = false;
    private bool onlyone = true;
    public Transform[] transforms;
    public GameObject create, kol;
    // Start is called before the first frame update
    void Start()
    {
        kol = GameObject.FindWithTag("kol");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*if (dead == true && onlyone)
        {
            Death();
            dead = false;
            onlyone = false;
        }*/
    }
    public void Death()
    {
        kol.GetComponent<TMPro.TextMeshProUGUI>().text = " " + (int.Parse(kol.GetComponent<TMPro.TextMeshProUGUI>().text) + 1);
        Instantiate(create, transforms[0].position, transform.rotation);
        Instantiate(create, transforms[1].position, transform.rotation);
        Destroy(gameObject);
    }
}
