using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public Module[] moduleList;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void DisableModules()
    {
        foreach (Module m in moduleList)
        {
            m.GetComponentInChildren<BoxCollider>().enabled = false;
        }
    }

    public void EnableModules()
    {
        foreach (Module m in moduleList)
        {
            m.GetComponentInChildren<BoxCollider>().enabled = true;
        }
    }
}
