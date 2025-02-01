using System;
using UnityEngine;

namespace _Scripts {
    public class PlayerMovement : MonoBehaviour {
        
        [Tooltip("The Player's movement speed.")]
        public float speed = 5f;
        
        [Tooltip("The reference to the camera.")]
        public Transform cameraTransform;
        

        private void Update() {
            transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            
            Console.Out.WriteLine($"X: {moveX}, Y: {moveZ}");
            
            Vector3 movement = transform.right * moveX + transform.forward * moveZ;
            
            transform.Translate(movement * (speed * Time.deltaTime), Space.World);
        }
    }
}
