using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lara {
    public class PlayerController : MonoBehaviour {
        public GameObject player;
        public GameObject gameOverScreen;
        Rigidbody2D       rb;
        public float      speed;
        public float      jumpForce;
        bool              canJump = true;
        float             ypos    = -5f;

        // Start is called before the first frame update
        void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update() {
            Vector2 movement = transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(movement);

            if (transform.position.y < ypos) {
                Die();
            }
        }

        void Die() {
            player.SetActive(false);
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;

            Debug.Log("O personagem morreu! Fechando o jogo...");
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        private void FixedUpdate() {
            Jump();
        }

        void Jump() {
            if (Input.GetButtonDown("Jump") && canJump) {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
            }

            if (rb.linearVelocity.y == 0) {
                canJump = true;
            }
        }
    }
}