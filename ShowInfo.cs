using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    private Manager manager;
    private string displayInfo;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent < Manager>();
    }

    public void SetInfo(string info)
    {
        displayInfo = info;
        StartCoroutine("Display");
    }

    IEnumerator Display()
    {
        manager.DisplayInfo(displayInfo);
        Debug.Log("display");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("hide");
        manager.DisplayInfo("");
    }
}
