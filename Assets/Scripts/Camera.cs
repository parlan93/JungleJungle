using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        camera.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
