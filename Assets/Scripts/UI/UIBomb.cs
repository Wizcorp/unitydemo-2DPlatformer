using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBomb : MonoBehaviour
{
    private GUITexture      bombIcon;
    private BombContainer   bombContainer;

    void Awake()
    {
        bombIcon = GetComponent<GUITexture>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        bombContainer = player.GetComponentInChildren<BombContainer>();
        if (!bombContainer)
            bombIcon.enabled = false;
    }
	
	void Update ()
    {
        if (bombContainer)
            bombIcon.enabled = bombContainer.HasBombs();
    }
}
