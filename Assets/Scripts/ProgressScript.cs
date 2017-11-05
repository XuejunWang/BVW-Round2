using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScript : MonoBehaviour {

    public GameObject[] m_clues;
    public GameObject[] trainingItems;
    private bool[] m_trainingActive;
    private bool[] m_cluesActive;
    private int progress = 0;
    public Sprite[] m_progressHUD;
    public Sprite m_trainingHUD;

    private CrimeAlert m_alarmScript;
    private AudioSource audioSource;
    

    public bool trainingMode = true;

    private void Start()
    {
        m_alarmScript = GetComponent<CrimeAlert>();
        m_trainingActive = new bool[trainingItems.Length];
        m_cluesActive = new bool[m_clues.Length];
        audioSource = GetComponent<AudioSource>();

    }

    public void updateProgress()
    {
        if (trainingMode)
        {
            for (int i = 0; i < trainingItems.Length; i++)
            {
                if (trainingItems[i].activeSelf)
                {
                    if (!m_trainingActive[i])
                    {
                        m_trainingActive[i] = true;
                    }
                }
            }
            if (m_trainingActive[0])
            {
                GetComponent<SpriteRenderer>().sprite = m_trainingHUD;
                audioSource.Play();
                m_alarmScript.PlayCrimeAlert();

            }
        }
        else
        {
            for (int i = 0; i < m_clues.Length; i++)
            {
                if (m_clues[i].GetComponent<ItemInteraction>().m_isItemAnalyzed)
                {
                    if (!m_cluesActive[i])
                    {
                        m_cluesActive[i] = true;
                    }
                }
            }

            progress = 0;
            for (int i = 0; i < m_cluesActive.Length; i++)
            {
                if (m_cluesActive[i])
                {
                    progress++;
                    print(progress);
                }
            }

            Debug.Log("before update progress");
            GetComponent<SpriteRenderer>().sprite = m_progressHUD[progress];
            if(progress == 5)
            {
                AudioClip audioClip = Resources.Load<AudioClip>("complete_progress");
                audioSource.clip = audioClip;
                audioSource.Play();
                progress++;
            } else if (progress >= 1 && progress <=4)
            {
                audioSource.Play();
            }
            //print(progress);
        }
    }
}
