using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lara {
    public class CameraFollow : MonoBehaviour {
        public GameObject player;

        float        zOffset = -10f;
        public float damping;
        Vector3      vel = Vector3.zero;

        // Start is called before the first frame update
        void Start() { }

        // Update is called once per frame
        void Update() {
            transform.position = player.transform.position;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref vel, damping);

            newPos.z = zOffset;
            transform.position = newPos;
        }
    }
}