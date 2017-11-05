using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour, IInputClickHandler, IFocusable
{
    public GameObject ObjectToPop;
    private bool m_itemPopped;
    public bool IsItemInteracted = false;
    private float m_distance;
    public bool IsInteractable = true;
    private bool m_itemBeenOut = true;
    public bool HasInteractedForATime = false;
    private float m_timer = 0;

    public bool IsItemClicked = false;
    public bool IsItemGazed = false;
    private AudioSource audioSource;
    public Sprite itemHUD;
    private Sprite originalHUD;
    [SerializeField]private Sprite itemClickHUD;

    [SerializeField] private float m_thresholdInteractionTime = 8f;
    [SerializeField] private float m_distanceToPop = 1.5f;
    [SerializeField] private float m_popInFrom = 0f;
    [SerializeField] private float m_popInTo = 1f;
    [SerializeField] private float m_popInDistance = 0.6f;
    [SerializeField] private Vector3 m_popInDirection;
    [SerializeField] private float m_popInTime = 0.6f;
    [SerializeField] private float clickHUDTime = 1.0f;
    //[SerializeField] Transform m_playerTransform;

    private AudioSource m_audioSource;
    private ItemGazed m_itemGazed;
    private GlowObjectCmd m_glow;
    private Color m_color;
    public bool m_isItemAnalyzed = false;

    private ProgressScript progrssScript;


    // Use this for initialization
    void Start () {
        m_audioSource = GetComponent<AudioSource>();
        //m_itemGazed = GetComponent<ItemGazed>();
        m_glow = GetComponent<GlowObjectCmd>();
        m_color = m_glow.GlowColor;
        m_glow.GlowColor = Color.black;
        audioSource = GetComponent<AudioSource>();
        progrssScript = GameObject.Find("HUD").GetComponent<ProgressScript>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (IsInteractable)
        //{
        //    m_distance = Vector3.Distance(m_playerTransform.position, transform.position);
        //    if (m_distance <= m_distanceToPop)
        //    {
        //        if (!HasInteractedForATime)
        //        {
        //            m_timer += Time.deltaTime;
        //            if (m_timer > m_thresholdInteractionTime)
        //            {
        //                HasInteractedForATime = true;
        //            }
        //        }
        //        ObjectToPop.SetActive(true);
        //        ObjectToPop.GetComponent<PopAnimationScript>().PopIn(m_popInFrom, m_popInTo, m_popInDistance, m_popInDirection, m_popInTime);
        //        if (m_itemBeenOut)
        //        {
        //            if (!m_audioSource.isPlaying)
        //            {
        //                m_audioSource.Play();
        //            }
        //        }
        //        m_itemBeenOut = false;
        //        IsItemInteracted = true;
        //    }

        //    else
        //    {
        //        m_timer = 0;
        //        ObjectToPop.GetComponent<PopAnimationScript>().PopOut(m_popInTo, m_popInFrom, -m_popInDistance, m_popInDirection, m_popInTime);
        //        m_itemBeenOut = true;
        //    }
        //}
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        IsItemClicked = true;
        if (IsInteractable)
        {
            if (IsItemGazed)
            {
                if (!m_isItemAnalyzed)
                {
                    AudioClip audioClipPop = Resources.Load<AudioClip>("billboard_select1");
                    m_audioSource.clip = audioClipPop;
                    m_isItemAnalyzed = true;
                    if (!m_itemPopped)
                    {
                        ObjectToPop.GetComponent<ClueAnalyzingAnim>().PlayClueAnalyzingAnim();
                        StartCoroutine(itemAnalyzing());
                        //progrssScript.updateProgress();
                        ObjectToPop.GetComponent<PopAnimationScript>().PopIn(m_popInFrom, m_popInTo, m_popInDistance, m_popInDirection, m_popInTime);
                        //originalHUD = GameObject.Find("HUD").GetComponent<SpriteRenderer>().sprite;
                        if (!m_audioSource.isPlaying)
                        {
                            Debug.Log("play sound");
                            m_audioSource.Play();
                        }
                    }
                    else
                    {
                        ObjectToPop.GetComponent<PopAnimationScript>().PopOut(m_popInTo, m_popInFrom, -m_popInDistance, m_popInDirection, m_popInTime);
                        m_itemPopped = false;
                        m_audioSource.Stop();
                    }
                }
                else
                {
                    if (!m_itemPopped)
                    {
                        ObjectToPop.GetComponent<PopAnimationScript>().PopIn(m_popInFrom, m_popInTo, m_popInDistance, m_popInDirection, m_popInTime);
                        m_itemPopped = true;
                        if (!m_audioSource.isPlaying)
                        {
                            m_audioSource.Play();
                        }
                    }
                    else
                    {
                        ObjectToPop.GetComponent<PopAnimationScript>().PopOut(m_popInTo, m_popInFrom, -m_popInDistance, m_popInDirection, m_popInTime);
                        m_itemPopped = false;
                        m_audioSource.Stop();
                    }
                }
            }
        }
    }

    public void OnFocusEnter()
    {
        originalHUD = GameObject.Find("HUD").GetComponent<SpriteRenderer>().sprite;
        IsItemGazed = true;
        m_glow.GlowColor = m_color;
        GameObject.Find("HUD").GetComponent<SpriteRenderer>().sprite = itemHUD;
        AudioClip audioClipGaze = Resources.Load<AudioClip>("object_gaze");
        audioSource.clip = audioClipGaze;
        audioSource.Play();
    }

    public void OnFocusExit()
    {
        IsItemGazed = false;
        m_glow.GlowColor = Color.black;
        GameObject.Find("HUD").GetComponent<SpriteRenderer>().sprite = originalHUD;
        //if (m_itemPopped)
        //{
        //    if (!ObjectToPop.GetComponent<ItemGazed>().IsItemGazed)
        //    {
        //        ObjectToPop.GetComponent<PopAnimationScript>().PopOut(m_popInTo, m_popInFrom, -m_popInDistance, m_popInDirection, m_popInTime);
        //        m_itemPopped = false;
        //    }
        //}
    }

    IEnumerator itemAnalyzing()
    {
        GameObject.Find("HUD").GetComponent<SpriteRenderer>().sprite = itemClickHUD;
        yield return new WaitForSeconds(clickHUDTime);
        progrssScript.updateProgress();
        originalHUD = GameObject.Find("HUD").GetComponent<SpriteRenderer>().sprite;
    }
}
