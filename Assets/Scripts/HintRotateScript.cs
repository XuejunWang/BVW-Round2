using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintRotateScript : MonoBehaviour {

    public float speed = 3.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = transform.position - GameObject.Find("HoloLensCamera").transform.position;
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime * speed);
    }
}
