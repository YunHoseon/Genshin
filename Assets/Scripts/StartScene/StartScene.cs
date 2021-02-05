using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SetSceneID(0);
    }
}
