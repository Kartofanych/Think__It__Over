using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemyinfo : MonoBehaviour
{
    public int health = 30;
    Renderer[] characterMaterials;
    private Animator animator;


    public Texture2D[] albedoList;
    [ColorUsage(true, true)]
    public Color[] eyeColors;
    public enum EyePosition { normal, happy, angry, dead }
    public EyePosition eyeState;



    public float speed;
    public int positionOfPatrol;
    public Vector3 point;
    private bool movingBack;
    public Transform player;
    private Rigidbody rb;
    public Transform tr;

    // Start is called before the first frame update
    public void TakeDamage (int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
           
        }
    }
    private void Die()
    {
        ChangeEyeOffset(EyePosition.dead);
        ChangeAnimatorIdle("dead");
        StartCoroutine(DIEE());
        player.GetComponent<hero1>().kills++;
        // rb.AddForce(-tr.transform.forward * 600f);
        if(gameObject.tag == "first")
        {
            GetComponent<FirstEnemy>().dead = true;
        }
        if (SceneManager.GetActiveScene().name == "menu")
        {
            gameObject.GetComponent<bots_for_training>().Death();

        }
    }
    IEnumerator DIEE()
    {
        yield return new WaitForSeconds(0.75f);
        gameObject.SetActive(false);
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        point = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        if (SceneManager.GetActiveScene().name == "menu")
        {
           health = 80;

        }
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }
    // Update is called once per frame
    private Vector3 posPlayer;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    void Update()
    {
        float Angle = -Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x) / Mathf.PI * 180f +  90f;
        // градус поворота
        float RotAng = 180f * Time.deltaTime;
        // градус между поворотом объекта и углом цели
        float DeltaAng = Mathf.DeltaAngle(transform.eulerAngles.y, Angle);
        if (Mathf.Abs(DeltaAng) < RotAng)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Angle, transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles += new Vector3(0, RotAng * Mathf.Sign(DeltaAng), 0);
        }


       

        if(Vector3.Distance(transform.position, player.transform.position) < positionOfPatrol && Vector3.Distance(transform.position, player.transform.position) != 0f && health > 0)
        {

            animator.SetFloat("Blend", speed, StartAnimTime, Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }
    }


  
    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_MainTex", offset);
        }
    }
    public float addingForce1 = 300f;
    public float addingForce2 = 2000f;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Die")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * addingForce1);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(tr.transform.forward * addingForce2);
        }
    }
}
