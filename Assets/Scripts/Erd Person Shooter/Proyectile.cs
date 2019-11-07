using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour {


	// Use this for initialization
	void Start () {
        transform.localRotation = Quaternion.Euler(0, -90, 0);
    }
	
	// Update is called once per frame
	void Update () {
        float distanceToCamera = Vector3.Distance(transform.position, CameraController.instance.transform.position);
        if (distanceToCamera > 100)
        {
            Destroy(gameObject);
        }
    }
}
