using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInterface : MonoBehaviour {

    public float distance;
    public float speed;
    public Transform cam;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = cam.position + cam.forward * distance;

        Vector3 dir = transform.position - GameObject.Find("HoloLensCamera").transform.position;
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime * speed);
    }
}
