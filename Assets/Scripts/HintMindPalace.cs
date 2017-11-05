using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintMindPalace : MonoBehaviour {

    public GameObject[] itemsBoard;
    public GameObject[] pplBoard;
    public GameObject[] hintsBoard;
    public GameObject[] suspectForATime;
    public GameObject[] accuseBoards;
    public GameObject[] infoBoards;

    private AudioSource m_audioSource;
    public bool m_accuseMusicPlay = false;
    private bool m_allHintsShow = false;


    // Use this for initialization
    void Start () {
        m_audioSource = GameObject.Find("HoloLensCamera").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < itemsBoard.Length; i++)
        {
            for (int j = 0; j < pplBoard.Length; j++)
            {
                if (itemsBoard[i].activeSelf && pplBoard[j].activeSelf && suspectForATime[j].GetComponent<ItemInteraction>().HasInteractedForATime)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        hintsBoard[5 * j + z].SetActive(true);
                    }
                    //hintsBoard[i * pplBoard.Length + j].SetActive(true);
                }
            }
        }

        if (!m_allHintsShow)
        {
            bool initial = true;
            for (int i = 0; i < hintsBoard.Length; i++)
            {
                initial = initial && hintsBoard[i].activeSelf;
            }

            if (initial == true)
            {
                m_allHintsShow = true;
            }
        }
        else
        {
            if (!m_accuseMusicPlay)
            {
                Debug.Log("in play");
                m_audioSource.Play();

                for (int i = 0; i < suspectForATime.Length; i++)
                {
                    suspectForATime[i].GetComponent<ItemInteraction>().IsInteractable = false;
                    accuseBoards[i].SetActive(true);
                    infoBoards[i].SetActive(false);
                }

                m_audioSource.loop = true;
                m_accuseMusicPlay = true;
            }
        }
    }
}
