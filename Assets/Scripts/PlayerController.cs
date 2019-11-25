using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 10f;
    [SerializeField]
    private float jumpStrenght = 5f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
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
        rb.AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            gameObject.SetActive(false);
        }
    }
}
