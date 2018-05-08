using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class EditorSnap : MonoBehaviour {

    [SerializeField][Range(1f,20f)] float gridSize = 10f;
    TextMesh textMesh;
    // Update is called once per frame
    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }
    void Update () {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        snapPos.y = 0f;
        transform.position = snapPos;
        textMesh.text = snapPos.x / gridSize + "," + snapPos.z / gridSize;
    }
}
