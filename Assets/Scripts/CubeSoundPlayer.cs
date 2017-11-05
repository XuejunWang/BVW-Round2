using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSoundPlayer : MonoBehaviour {

    [SerializeField] private float m_playSoundAfter;
    private AudioSource m_audioSource;

	// Use this for initialization
	void Start () {
        m_audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundAfter());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PlaySoundAfter()
    {
        yield return new WaitForSeconds(m_playSoundAfter);
        m_audioSource.Play();
    }
}
