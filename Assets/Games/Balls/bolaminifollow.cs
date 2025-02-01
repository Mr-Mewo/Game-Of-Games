using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaminifollow : MonoBehaviour
{
    public GameObject bola;
    public GameObject bolamini;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(bola.transform.position.x - 2f, bola.transform.position.y, bola.transform.position.z + 0.5f);

        bolamini.transform.position = Vector3.Lerp(bolamini.transform.position, targetPosition, Time.deltaTime * 5f);
    }
}
