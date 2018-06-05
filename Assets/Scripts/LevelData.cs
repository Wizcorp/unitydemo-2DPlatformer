using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    public int scoreLock;

    public string nameLevel;

    [HideInInspector]
    public bool locked = true;
    [HideInInspector]
    public string lockedBy = "";
    [HideInInspector]
    public int lockedHighScoreBy = 0;

    public Transform[] buttonLevelToUnlock;
    public GameObject levelTextObject;
    public GameObject lockedObject;
    public GameObject highScoreObject;
}
