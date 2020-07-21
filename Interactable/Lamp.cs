using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public Camera mainCamera;
    public Transform lamp;
    public Transform glass;

    public void PlaySound()
    {
        FindObjectOfType<SoundManager>().PlaySound("Glass");
    }
}
