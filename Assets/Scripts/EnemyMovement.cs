using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    bool foundTarget = false;
    [SerializeField] Waypoint targetWayPoint;
	// Use this for initialization
	void Start () {
        if(targetWayPoint)
        {
            foundTarget = true;
        }
    }

    public void SetTarget(Waypoint target)
    {
        targetWayPoint = target;
        foundTarget = true;

    }
	// Update is called once per frame
	void Update () {
         
        if(foundTarget == true)
        {
            StopAllCoroutines();
            StartCoroutine(Pathfind());
            foundTarget = false;
        }
	}

    private IEnumerator Pathfind()
    {
        List<Waypoint> path = FindObjectOfType<Pathfinder>().Pathfind(new Vector2Int(Mathf.RoundToInt(gameObject.transform.position.x / 10), Mathf.RoundToInt(gameObject.transform.position.z / 10)), targetWayPoint.GetGridPos());
        foreach (Waypoint waypoint in path)
        {
            gameObject.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
        
    }
}
