using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Joystick joystick;
    public Transform BulletSpawner;
    public GameObject BulletPrefab;
    public float salto;
    public int limiteSaltos = 1;
    private int numeroSaltos = 0;
    public float vel;
    Rigidbody2D rb;
    bool voltearPlayer = true;
    SpriteRenderer miPlayer;
    Animator animatorPlayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        miPlayer = GetComponent<SpriteRenderer>();
        animatorPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (numeroSaltos < limiteSaltos)
            {


                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0, salto), ForceMode2D.Impulse);
                numeroSaltos++;
            }
        }
        float mh = Input.GetAxis("Horizontal");
       
        if (mh > 0 && !voltearPlayer)
        {
            voltear();
        }
        else if (mh < 0 && voltearPlayer)
        {
            voltear();
        }

        rb.velocity = new Vector2(mh * vel, rb.velocity.y);
        animatorPlayer.SetFloat("velMov", Mathf.Abs(mh));
        PlayerShooting();
    }

    void voltear()
    {
        voltearPlayer = !voltearPlayer;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //miPlayer.flipX = !miPlayer.flipX;
    }

    public void PlayerShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(BulletPrefab, BulletSpawner.position, BulletSpawner.rotation);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Suelo")
        {
            numeroSaltos = 0;
        }
    }
}