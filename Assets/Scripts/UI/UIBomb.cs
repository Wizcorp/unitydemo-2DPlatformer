using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBomb : MonoBehaviour
{
    private GUITexture      m_BombIcon;
    private BombContainer   m_BombContainer;

    void Awake()
    {
        m_BombIcon = GetComponent<GUITexture>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_BombContainer = player.GetComponentInChildren<BombContainer>();
        if (!m_BombContainer)
            m_BombIcon.enabled = false;
    }
	
	void Update ()
    {
        if (m_BombContainer)
            m_BombIcon.enabled = m_BombContainer.HasBombs();
    }
}
