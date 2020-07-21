using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Canvas").Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
    }
}
