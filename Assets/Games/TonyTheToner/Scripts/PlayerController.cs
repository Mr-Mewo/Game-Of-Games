using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody2D _rb2d;
    
    public float speed, 
                 jumpPower;

    private bool _canJump = true;
    
    private void Start() {
        _rb2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        Vector2 movement = transform.right * (speed * Input.GetAxis("Horizontal"));
        
        transform.Translate(movement * Time.deltaTime );


        if (transform.position.y < -10) {
            Application.Quit();
        }
    }
    
    private void FixedUpdate() {
        if (Input.GetButtonDown("Jump") && _canJump) {
            _rb2d.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            _canJump = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag($"Colliders")) {
            _canJump = true;
        }
    }

}
