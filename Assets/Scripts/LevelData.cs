using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    public int scoreLock;                       // Number of score to unlock level.

    public string nameLevel;                    // Name of level.

    [HideInInspector]
    public bool locked = true;                  // Whether or not the level is lo locked.
    [HideInInspector]
    public string lockedBy = "";                // The name of the level that blocks it.
    [HideInInspector]
    public int lockedHighScoreBy = 0;           // And it high score.

    public Transform[] buttonLevelToUnlock;     // Button level of the level to unlock

    public GameObject levelTextObject;          // Text object of the actual button
    public GameObject lockedObject;             // Locked object of the actual button
    public GameObject highScoreObject;          // High score text object of the actual button
}
