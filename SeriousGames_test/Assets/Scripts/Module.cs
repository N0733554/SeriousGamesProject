using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public bool Escapable = true;
    public bool Selected = false;
    public bool Active = false;
    public bool Complete = false;

    public Camera cam;
    public Vector3 camPos;
    public Color highlightColor;

    protected Color baseColor;
    
    protected virtual void Start()
    {
        baseColor = GetComponentInChildren<Renderer>().material.color;
    }

    protected virtual void Update()
    {
        if(Selected && Input.GetMouseButtonDown(0))
        {
            SetActive();            
        }   
        
        if(Active && Input.GetMouseButtonDown(1))
        {
            SetInactive();
        }
    }

    public void SelectItem()
    {
        Selected = true;
        GetComponentInChildren<Renderer>().material.color = highlightColor;
    }
    public void DeselectItem()
    {
        Selected = false;
        GetComponentInChildren<Renderer>().material.color = baseColor;        
    }
    public virtual void SetActive()
    {
        Active = true;
        cam.GetComponent<CameraScript>().MoveToPos(camPos);
        GetComponentInParent<Machine>().DisableModules();
    }
    public virtual void SetInactive()
    {
        if(Escapable)
        {
            Active = false;
            cam.GetComponent<CameraScript>().MoveToOrigin();
            GetComponentInParent<Machine>().EnableModules();
        }        
    }
}
