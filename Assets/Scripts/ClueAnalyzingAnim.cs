using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ClueAnalyzingAnim : MonoBehaviour {
    [SerializeField] private Texture[] m_clueAnimTexture;
    [SerializeField] private int m_loops = 7;
    [SerializeField] private float m_loopTime = 0.2f;
    private MeshRenderer[] m_renderers;
    private MeshRenderer m_renderer;
    private Texture m_texture;
    private VideoPlayer videoPlayer;
    //private AudioSource m_audioSource;

    // Use this for initialization
    void Start () {
        m_renderers = GetComponents<MeshRenderer>();
        print(m_renderers.Length);
        m_renderer = GetComponent<MeshRenderer>();
        m_texture = m_renderer.material.mainTexture;
        videoPlayer = GetComponent<VideoPlayer>();
        //m_audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator ClueAnalyzingAnimCoroutine()
    {
        for(int i = 0; i < m_clueAnimTexture.Length * m_loops; i++)
        {
            m_renderer.material.mainTexture = m_clueAnimTexture[i % m_clueAnimTexture.Length];
            yield return new WaitForSeconds(m_loopTime);
        }
        m_renderer.material.mainTexture = m_texture;

        //if (m_audioSource)
        //{
        //    AudioClip audioClipPop = Resources.Load<AudioClip>("add_progress");
        //    m_audioSource.clip = audioClipPop;
        //    m_audioSource.Play();
        //}

        if (videoPlayer)
        {
            videoPlayer.Play();
        }
        
    }

    public void PlayClueAnalyzingAnim()
    {
        StartCoroutine(ClueAnalyzingAnimCoroutine());
    }
}
