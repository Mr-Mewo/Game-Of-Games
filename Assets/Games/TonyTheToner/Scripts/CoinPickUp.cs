using System;
using System.Collections;
using UnityEngine;

public class CoinPickUp : MonoBehaviour {
    
    private ScoreManager _scoreManager;
    public new GameObject camera;
    public GameObject coinPS;

    private void Start() {
        _scoreManager = camera.gameObject.GetComponent< ScoreManager >();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag($"TriggerColliders")) {
            GameObject coinVFX = Instantiate(coinPS, transform.position, Quaternion.identity);
            
            Destroy( collision.gameObject );
            _scoreManager.AddScore(5);
            
            StartCoroutine(DestroyCoin(coinVFX));
        }
    }

    private IEnumerator DestroyCoin(GameObject coin) {
        yield return new WaitForSeconds(3f);
        Destroy(coin);
    }
}
