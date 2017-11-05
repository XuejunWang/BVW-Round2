using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimeAlert : MonoBehaviour {

    private SpriteRenderer m_spriteRenderer;
    private Sprite m_sprite;
    [SerializeField] private float m_timeWaitBeforeAlert = 10f;
    [SerializeField] private Sprite[] m_alartSprite;
    [SerializeField] private float m_flashTime = 0.2f;
    [SerializeField] private int m_alertTimes = 15;
    private float m_timer = 0;
    private ProgressScript progressScript;

    private AudioSource audioSource;
    private AudioClip audioClip;

    // Use this for initialization
    void Start () {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_sprite = m_spriteRenderer.sprite;
        progressScript = GetComponent<ProgressScript>();
        audioSource = GameObject.Find("HoloLensCamera").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator CrimeAlertCoroutine()
    {
        audioClip = Resources.Load<AudioClip>("alert");
        audioSource.clip = audioClip;
        yield return new WaitForSeconds(m_timeWaitBeforeAlert);
        audioSource.Play();
        audioSource.volume = 0.5f;
        for (int i = 0; i < m_alartSprite.Length * m_alertTimes; i++)
        {
            if (!progressScript.trainingMode)
            {
                break;
            }
            m_spriteRenderer.sprite = m_alartSprite[i % m_alartSprite.Length];
            yield return new WaitForSeconds(m_flashTime);
            
        }
        if (progressScript.trainingMode)
        {
            m_spriteRenderer.sprite = m_sprite;
        }
        //else
        //{
            //m_spriteRenderer.sprite = null;
        //}
    }

    public void PlayCrimeAlert()
    {
        StartCoroutine(CrimeAlertCoroutine());
    }
}
