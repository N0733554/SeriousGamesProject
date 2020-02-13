using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    bool Selected = false;
    bool Connected = false;
    Node Partner = null;
    private WireModule wireModule;

    public Color highlightColor;
    public Color baseColor;
    private void Start()
    {
       wireModule= GetComponentInParent<WireModule>();
    }
    public void SelectItem()
    {
        Selected = true;
        wireModule.selectedNode = this;

        // Colour change 
        GetComponentInChildren<Renderer>().material.color = highlightColor;
    }
    public void DeselectItem()
    {
        Selected = false;
        wireModule.selectedNode = null;

        // Colour change back
        GetComponentInChildren<Renderer>().material.color = baseColor;
    }
    public bool isSelected()
    {
        return Selected;
    }
    public bool isConnected()
    {
        return Connected;
    }

    public void setPartner(Node n)
    {
        Partner = n;
    }
    public Node getPartner()
    {
        return Partner;
    }

    public void Connect()
    {
        Connected = true;
    }

}
