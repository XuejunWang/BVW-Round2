using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.Video;

public class ItemGazed: MonoBehaviour, IFocusable
{
    private VideoPlayer m_videoPlayer;
    public bool IsItemGazed = true;

    private void Start()
    {
        m_videoPlayer = GetComponent<VideoPlayer>();
    }


    public void OnFocusEnter()
    {
        IsItemGazed = true;
    }

    public void OnFocusExit()
    {
        IsItemGazed = false;
    }

    private void Update()
    {
        if (m_videoPlayer)
        {
            if (IsItemGazed)
            {
                m_videoPlayer.enabled = true;
                m_videoPlayer.Play();

            }
            else
            {
                m_videoPlayer.Stop();
                m_videoPlayer.enabled = false;
            }
        }
    }
}
