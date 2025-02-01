using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lara {
    public class CoinPickUP : MonoBehaviour {

        public ScoreManager scoreManager;
        public GameObject   cam;

        public GameObject coinPS;

        private void Start() {
            scoreManager = cam.GetComponent<ScoreManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Coin") {
                GameObject temp_VFX = Instantiate(coinPS, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                scoreManager.AddScore(5);
                StartCoroutine(DestroyAfterSeconds(temp_VFX));
            }
        }


        IEnumerator DestroyAfterSeconds(GameObject objectToDestroy) {
            yield return new WaitForSeconds(3f);
            Destroy(objectToDestroy);
        }
    }
}