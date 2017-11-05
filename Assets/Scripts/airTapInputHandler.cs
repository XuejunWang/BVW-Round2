using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class airTapInputHandler : MonoBehaviour, IInputClickHandler{

    private AudioSource m_audioSource;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        m_audioSource.Play();
    }

    // Use this for initialization
    void Start () {
        m_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
