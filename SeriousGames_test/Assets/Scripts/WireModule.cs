using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireModule : Module
{
    public Node[] NodeGroup;
    int[] Connections;
    public Node selectedNode;

    public Material wireMaterial;
    public float wireThickness = 1;

    bool Connecting = false;
    Node CurrentConnectingNode;
    GameObject DrawnWire;

    protected override void Start()
    {
        base.Start();

        foreach (Node n in NodeGroup)
        {
            n.GetComponent<SphereCollider>().enabled = false;
        }

        GenerateConnections();
    }

    protected override void Update()
    {
        base.Update();

        if (!Complete)
        {
            if(ConnectionsCompleted())
            {
                Complete = true;
                print("COMPLETE");
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (!Connecting && selectedNode)
                    StartConnecting(selectedNode);                
            }

            if (Connecting)
            {
                UpdateDrawnWire();

                if (Input.GetMouseButtonDown(0))
                {
                    if (selectedNode)
                    {
                        if (isValidConnection(CurrentConnectingNode, selectedNode))
                        {
                            print("VALID CONNECTION");
                            Connect(CurrentConnectingNode, selectedNode);
                        }
                        else
                            print("INVALID CONNECTION");
                    }
                    StopConnecting();
                }
            }
        

            //if (Connecting) // If the player is in the process of Connecting 2 nodes
            //{
            //    //Draw Wire from connecting node to the mouse
            //    
            //    UpdateDrawnWire(CurrentConnectingNode.transform.position, Camera.main.ScreenToWorldPoint(v3));

            //    // Check to see if player has clicked while connecting nodes

            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        foreach (Node newNode in NodeGroup) // If they have clicked on a node, connect to the 2
            //        {   // IF the new node is not the same as the starting node, the new node is being hovered over & the new node is not already connected 
            //            if (newNode != CurrentConnectingNode && newNode.isSelected() && !newNode.isConnected())
            //            {   //Iterates through the connections array
            //                if (isValidConnection(CurrentConnectingNode, newNode))
            //                {
            //                    print("VALID CONNECTION");
            //                    Connect(CurrentConnectingNode, newNode);
            //                }
            //                else
            //                    print("INVALID CONNECTION");
            //            }
            //        }
            //        Connecting = false;
            //    }
            //}
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
        Escapable = false;

        CurrentConnectingNode = n;

        DrawnWire = new GameObject(); // A new wire object is created
        DrawnWire.AddComponent<LineRenderer>();
        LineRenderer lr = DrawnWire.GetComponent<LineRenderer>();
        lr.material = wireMaterial;
    }

    void StopConnecting()
    {
        Connecting = false;
        Escapable = true;
    }

    public void Connect(Node a, Node b)
    {
        a.Connect();
        print("Connected " + a.name + ": " + b.name);
        b.Connect();
        print("Connected " + b.name + ": " + a.name);
    }
    
    void UpdateDrawnWire()
    {
        var start = CurrentConnectingNode.transform.position;
        var end = Input.mousePosition;
        end.z = 17.7f;
        end = Camera.main.ScreenToWorldPoint(end);
        DrawnWire.GetComponent<LineRenderer>().SetPosition(0, start);
        DrawnWire.GetComponent<LineRenderer>().SetPosition(1, end);        
    }

    void GenerateConnections()
    {
        int numberOfConnections = Random.Range(1, 4);
        List<int> ConnectedNumbers = new List<int>();

        for (int i = 0; i < numberOfConnections; i++)
        {
            int x = Random.Range(0, 3);
            while (ConnectedNumbers.Contains(2 * x + 1))
            {
                x = Random.Range(0, 3);
            }
            int Odd = 2 * x + 1;

            x = Random.Range(0, 3);
            while (ConnectedNumbers.Contains(2 * x + 2))
            {
                x = Random.Range(0, 3);
            }
            int Even = 2 * x + 2;

            NodeGroup[Odd-1].setPartner(NodeGroup[Even-1]);
            NodeGroup[Even-1].setPartner(NodeGroup[Odd-1]);

            ConnectedNumbers.Add(Even);
            ConnectedNumbers.Add(Odd);

            print("Added Connection between " + Odd + " and " + Even);
        }
    }

    bool isValidConnection(Node a, Node b)
    {
        if (a.getPartner() == b)
            return true;
        else
            return false;
    }

    bool ConnectionsCompleted()
    {
        int Required = 0;
        int Completed = 0;
        for(int n = 0; n < NodeGroup.Length; n += 2)
        {
            if(NodeGroup[n].getPartner())
            {
                Required++;
                if(NodeGroup[n].isConnected())
                {
                    Completed++;
                }
            }
        }
        if (Required == Completed)
            return true;
        else
            return false;
    }

    void CompleteModule()
    {
        foreach (Node n in NodeGroup)
        {
            n.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
