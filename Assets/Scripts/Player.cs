using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D rb;

    public Bullet bulletPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check Input
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else {
            _turnDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        if (_thrusting) {
            rb.AddForce(transform.up * thrustSpeed);
        }

        if (_turnDirection != 0.0f) {
            rb.AddTorque(_turnDirection * turnSpeed);
        }
    }

    private void Shoot() {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid") 
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;
            gameObject.SetActive(false);

            //bad performance
            //FindObjectOfType other way to do this
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
