using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static GameObject selectedObject;
    private Color pColor;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.transform.gameObject;

            SelectObject(hitObject);
        }
        else
        {
            ClearSelection();
        }

    }

    void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            ClearSelection();
        }

        //if(obj.tag == "Selectable")
        //{
        //    selectedObject = obj;
        //    selectedObject.GetComponentInParent<Module>().SelectItem();
        //}
        switch (obj.tag)
        {
            case "Module":
                selectedObject = obj;
                selectedObject.GetComponentInParent<Module>().SelectItem();
                break;
            case "Node":
                selectedObject = obj;
                selectedObject.GetComponent<Node>().SelectItem();
                break;
            default:
                break;
        }

    }

    void ClearSelection()
    {
        if (selectedObject == null)
            return;
        
        //selectedObject.GetComponentInParent<Module>().DeselectItem();

        switch (selectedObject.tag)
        {
            case "Module":
                selectedObject.GetComponentInParent<Module>().DeselectItem();
                break;
            case "Node":
                selectedObject.GetComponent<Node>().DeselectItem();
                break;
            default:
                break;
        }

        selectedObject = null;
    }
}
