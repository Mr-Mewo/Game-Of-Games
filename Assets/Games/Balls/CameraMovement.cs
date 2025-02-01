using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject bola;
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(bola.transform.position.x, bola.transform.position.y, bola.transform.position.z - 15f);

        Camera.transform.position = Vector3.Lerp(Camera.transform.position, targetPosition, Time.deltaTime * 5f);
    }
}
