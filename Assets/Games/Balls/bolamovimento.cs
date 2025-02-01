using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolamovimento : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody rb;
    private bool NoChaoEsta;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NoChaoEsta)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("chao"))
        {
            NoChaoEsta = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            NoChaoEsta = false;
        }
    }

}
