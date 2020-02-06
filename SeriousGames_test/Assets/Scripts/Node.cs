using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    bool Selected = false;
    bool Connected = false;
    Node Partner = null;

    public Color highlightColor;
    public Color baseColor;
    private void Update()
    {

    }

    public void SelectItem()
    {
        Selected = true;
        // Colour change 
        GetComponentInChildren<Renderer>().material.color = highlightColor;
    }
    public void DeselectItem()
    {
        Selected = false;
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
    public Node getPartner()
    {
        return Partner;
    }

    public void ConnectTo(Node p)
    {
        Connected = true;
        Partner = p;
    }

}
