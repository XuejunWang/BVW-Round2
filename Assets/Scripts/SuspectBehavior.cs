using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectBehavior : MonoBehaviour {

    public ItemInteraction[] Clues;
    //[SerializeField] private GlowObjectCmd m_glow;
    public GameObject Glow;
    public GameObject[] Particles;
    public bool m_isAllCluesInteracted = false;
    private int m_clueInteractionCounter = 0;
    private ItemInteraction m_interaction;

    void Start () {
        //m_glow = GetComponent<GlowObjectCmd>();
        m_interaction = GetComponent<ItemInteraction>();
        m_interaction.IsInteractable = false;
        Glow.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (!m_isAllCluesInteracted)
        {
            m_isAllCluesInteracted = IsAllCluesInteracted();
            if (m_isAllCluesInteracted)
            {
                //m_glow.enabled = true;
                Glow.SetActive(true);
                m_isAllCluesInteracted = true;
                m_interaction.IsInteractable = true;
            }
        }
        else
        {
            Glow.SetActive(true);
            if (m_interaction.IsItemInteracted)
            {
                //Glow.SetActive(false);
                foreach(GameObject a in Particles)
                {
                    a.SetActive(false);
                }
            }
            //m_glow.enabled = true;
            //if (m_interaction.IsItemInteracted)
            //{
            //    m_glow.GlowColor = Color.black;
            //}
        }
    }

    private bool IsAllCluesInteracted()
    {
        m_clueInteractionCounter = 0;
        bool temp = false;
        for (int i = 0; i < Clues.Length; i++)
        {
            if (Clues[i].IsItemInteracted)
            {
                m_clueInteractionCounter++;
            }
        }
        if (m_clueInteractionCounter == Clues.Length)
        {
            temp = true;
        }
        else
        {
            temp = false;
        }
        return temp;
    } 
}
