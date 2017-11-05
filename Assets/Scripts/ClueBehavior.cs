using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBehavior : MonoBehaviour {
    private GlowObjectCmd m_glow;
    private ItemInteraction m_clue;
    private Color m_color;
    // Use this for initialization
    void Start () {

        m_glow = GetComponent<GlowObjectCmd>();
        m_clue = GetComponent<ItemInteraction>();
    }

    // Update is called once per frame
    void Update () {
        if (m_clue.IsItemInteracted)
        {
            m_glow.GlowColor = Color.black;
        }
        else
        {
            m_glow.enabled = true;
        }
	}
}
