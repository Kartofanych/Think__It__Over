using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunScript : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public LayerMask everythink;
    public Transform gunTip, camera1, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private Animator anim;
    private bool ready_for_shoot = true;
    public ParticleSystem flash;
    public GameObject impact_effect;
    public int kol_bul = 5, vs = 30;
    public bool isCooldown = false;
    public GameObject text_but, im;

    private int n = 2;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown("n") && isGrabbling == false)
        {
            n++;
            if (n % 2 == 0)
            { 
                im.SetActive(false);
            }
            else
            {
                im.SetActive(true);
            }
        }

        if (n % 2 == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && ready_for_shoot && kol_bul > 0)
            {
                Shoot();
                StartCoroutine(Animation_shoot());

            }else
            if(Input.GetMouseButtonDown(0) && ready_for_shoot && kol_bul == 0) {
                isCooldown = true;
                StartCoroutine(Animation_cool_down());

            }
        }
    }
    IEnumerator Animation_shoot()
    {
        ready_for_shoot = false;
        anim.SetBool("isShooting", true);
        yield return new WaitForSeconds(0.32f);
        anim.SetBool("isShooting", false);
        ready_for_shoot = true;
    }
    IEnumerator Animation_cool_down()
    {
        anim.SetBool("isCooldown", true);
        yield return new WaitForSeconds(1.75f);
        anim.SetBool("isCooldown", false);
        kol_bul = 5;
        text_but.GetComponent<TMPro.TextMeshProUGUI>().text = kol_bul.ToString() + "/" + vs.ToString();
        ready_for_shoot = true;
    }
    public float impactForce = 30f;
    private void Shoot()
    {
        kol_bul--;
        vs--;
        //string itog =  + "/" + vs.ToString;
        text_but.GetComponent<TMPro.TextMeshProUGUI>().text = kol_bul.ToString() + "/" + vs.ToString();
        flash.Play();
        RaycastHit hit;
        if (Physics.Raycast(camera1.position, camera1.forward, out hit,maxDistance, everythink))
        {
            if (hit.collider.tag == "menu")
            {
                SceneManager.LoadScene("menu");
            }else
            if (hit.collider.tag == "lvl1")
            {
                SceneManager.LoadScene("tutor");
            }else
            if (hit.collider.tag == "lvl2")
            {
                SceneManager.LoadScene("first_level");
            }else
            if (hit.collider.tag == "lvl3")
            {
                SceneManager.LoadScene("second_level");
            }

            if(hit.distance < 20f && (hit.collider.tag == "ice" || hit.collider.tag == "Die"))
            {
                player.GetComponent<Rigidbody>().AddForce(-gameObject.transform.right * 1500f);
            }else if(hit.distance < 10f)
            {
                player.GetComponent<Rigidbody>().AddForce(-gameObject.transform.right * 700f);
            }
            Enemyinfo enemy = hit.collider.GetComponentInParent<Enemyinfo>();
            if (enemy != null)
            {
                if (hit.collider.tag == "body")
                {
                    enemy.TakeDamage(35);
                }
                if(hit.collider.tag == "head")
                {
                    enemy.TakeDamage(70);
                }

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

            }
            GameObject go = Instantiate(impact_effect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(go, 2f);
        }

    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        isGrabbling = true;
        if (Physics.Raycast(camera1.position, camera1.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 6f;
            joint.damper = 5f;
            joint.massScale = 10f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    public bool isGrabbling = false;
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        isGrabbling = false;
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
