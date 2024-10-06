using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public static CameraBounds instance;

    private void Awake()
    {
        instance = this;
    }
}
