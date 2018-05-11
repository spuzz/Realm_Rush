using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour {

    TextMesh textMesh;
    Waypoint waypoint;
    // Update is called once per frame
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }
    void Update ()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new  Vector3( waypoint.GetGridPos().x * gridSize, 0f, waypoint.GetGridPos().y * gridSize);
    }

    private void UpdateLabel()
    {
        string labelText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        gameObject.name = labelText;
        textMesh.text = labelText;
    }
}
