using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public float damping;

    private const float ZOffset = -10f;
    private Vector3 _velocity = Vector3.zero;

    private void Start() {
        transform.position = player.transform.position;
    }

    private void Update() {
        var newPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref _velocity, damping);
        newPos.z = ZOffset;
        transform.position = newPos;
    }
}
