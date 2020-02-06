using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireModule : Module
{
    public Node[] NodeGroup;
    
    public Material wireMaterial;
    public float wireThickness = 1;

    bool Connecting = false;
    int CurrentConnectingIndex;
    GameObject DrawnWire;

    protected override void Start()
    {
        base.Start();

        foreach (Node n in NodeGroup)
        {
            n.GetComponent<SphereCollider>().enabled = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        foreach(Node n in NodeGroup)
        {
            if(n.isSelected() && Input.GetMouseButtonDown(0) && !Connecting)
            {
                StartConnecting(n);
                break;
            }
        }

        if (Connecting) // If the player is in the process of Connecting 2 nodes
        {
            //Draw Wire from connecting node to the mouse
            var v3 = Input.mousePosition;
            v3.z = 17.5f;
            UpdateDrawnWire(NodeGroup[CurrentConnectingIndex].transform.position, Camera.main.ScreenToWorldPoint(v3));

            // Check to see if player has clicked while connecting nodes

            if (Input.GetMouseButtonDown(0))
            {
                foreach (Node newNode in NodeGroup) // If they have clicked on a node, connect to the 2
                {
                    if (System.Array.IndexOf(NodeGroup, newNode) != CurrentConnectingIndex && newNode.isSelected())
                    {
                        Connect(NodeGroup[CurrentConnectingIndex], newNode);
                    }
                }
            }
        }
    }

    public override void SetActive()
    {
        base.SetActive();

        foreach(Node n in NodeGroup)
        {
            n.GetComponent<SphereCollider>().enabled = true;
        }
    }

    public override void SetInactive()
    {
        base.SetInactive();

        foreach (Node n in NodeGroup)
        {
            n.GetComponent<SphereCollider>().enabled = false;
        }
    }

    public void StartConnecting(Node n)
    {
        Connecting = true; // First Connecting is set to true to begin drawing a wire

        int a = System.Array.IndexOf(NodeGroup, n); // Then the index of the node is used and set to be the currentConnectingIndex
        CurrentConnectingIndex = a;

        DrawnWire = new GameObject(); // A new wire object is created
        DrawnWire.AddComponent<LineRenderer>();
        LineRenderer lr = DrawnWire.GetComponent<LineRenderer>();
        lr.material = wireMaterial;
        lr.SetWidth(wireThickness, wireThickness);
    }

    public void Connect(Node a, Node b)
    {
        a.ConnectTo(b);
        print("Connected " + a.name + ": " + b.name);
        b.ConnectTo(a);
        print("Connected " + b.name + ": " + a.name);
        Connecting = false;
    }
    
    void UpdateDrawnWire(Vector3 start, Vector3 end)
    {
        DrawnWire.GetComponent<LineRenderer>().SetPosition(0, start);
        DrawnWire.GetComponent<LineRenderer>().SetPosition(1, end);        
    }
}
