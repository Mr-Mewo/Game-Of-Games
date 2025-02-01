using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts {
    public class CameraMovement : MonoBehaviour {
        
        [FormerlySerializedAs("cameraSensitivity")] [Tooltip("The camera's sensitivity")]
        public float mouseSensitivity = 100f;
        
        [Tooltip("The reference to the player.")]
        public Transform playerTransform;
        
        [Tooltip("Camera's offset in relation to the player.")]
        public Vector3 offset;
        
        private Vector2 _rotation = Vector2.zero;
        // private float _verticalRotation = 0f;
        
        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() {
            transform.position = Vector3.Slerp( transform.position, playerTransform.position + offset, 0.9f ); 
            
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            
            Console.Out.WriteLine($"Mouse X: {mouseX}, Mouse Y: {mouseY}");
            
            // transform.Rotate(Vector3.up, mouseX);
            
            _rotation.x += mouseX;
            _rotation.y -= mouseY;
            _rotation.y = Mathf.Clamp(_rotation.y, -90f, 90f);
            
            // _verticalRotation -= mouseY;
            // _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);

            var rotation = transform.localRotation;
            rotation.eulerAngles = new Vector3(_rotation.y, _rotation.x, 0);
            transform.localRotation = rotation;
            
            // transform.Rotate(Vector3.up * mouseX);
        }
    }
}
