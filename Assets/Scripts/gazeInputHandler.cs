using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class gazeInputHandler : MonoBehaviour, IFocusable{
    public float speed = 3.0f;
    Color temp;

    public void OnFocusEnter()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    public void OnFocusExit()
    {
        GetComponent<Renderer>().material.color = temp;
    }

    // Use this for initialization
    void Start () {
        temp = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = transform.position - GameObject.Find("HoloLensCamera").transform.position;
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime * speed);
    }
}
