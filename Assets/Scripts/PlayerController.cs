using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 10f;
    [SerializeField]
    private float jumpStrenght = 5f;

    [SerializeField]
    AudioClip hitSound;
    [SerializeField]
    AudioClip jumpSound;

    [SerializeField]
    GameObject DeathEffect;

    public int health;
    public int maxHealth = 3;

    private Rigidbody rb;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            CheckBounds();
            Movement();
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(-v, 0, h) * baseSpeed * Time.deltaTime;

        rb.velocity += moveVector;
    }

    private void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 1f);

        if (hit.collider)
        {
            rb.AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            if (health > 1)
            {
                health--;
                audioSource.PlayOneShot(hitSound);
            }
            else
            {
                health--;
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                gameManager.GameOver();
            }

        }
        else if (collision.gameObject.tag == "Finish")
        {
            gameManager.Victory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpeedBoost")
        {
            rb.AddForce(other.transform.forward * 20f, ForceMode.VelocityChange);
        }
    }

    private void CheckBounds()
    {
        if (transform.position.y < -3f)
        {
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            gameManager.GameOver();
        }
    }
}
