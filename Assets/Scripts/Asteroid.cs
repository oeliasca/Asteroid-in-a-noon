using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50;
    public float lifeTime = 30;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        transform.localScale = Vector3.one * size;
        rb.mass = size;

    }

    public void SetTrajectory(Vector2 direction) 
    {
        rb.AddForce(direction * speed);

        Destroy(gameObject, lifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") 
        {
            if (size * 0.5 >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManager>().AsteroidDestroid(this);
            Destroy(gameObject);
            
        }
    }

    private void CreateSplit() 
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;


        Asteroid half = Instantiate(this, position,transform.rotation);
        half.size = size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized*speed);
    }
}
