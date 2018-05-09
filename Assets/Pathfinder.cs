using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    Waypoint startPoint, endPoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> pathQueue = new Queue<Waypoint>();
    List<Waypoint> closedList = new List<Waypoint>();
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    Waypoint currentNode;
    bool isRunning;
    void Start () {
        LoadBlocks();
        //ColorStartAndEnd();
        //Pathfind();
        
    }

    public List<Waypoint> Pathfind(Vector2Int start, Vector2Int end)
    {
        List<Waypoint> path = new List<Waypoint>();
        try
        {
            startPoint = grid[start];
            endPoint = grid[end];
        }
        catch (KeyNotFoundException e)
        {
            return path;
        }
        isRunning = true;
        pathQueue.Clear();
        closedList.Clear();
        BreadthFirstSearch();

        return CreatePath(path);
    }

    private void BreadthFirstSearch()
    {
        startPoint.parent = startPoint;
        pathQueue.Enqueue(startPoint);
        while (pathQueue.Count > 0 && isRunning)
        {
            currentNode = pathQueue.Dequeue();
            closedList.Add(currentNode);
            HaltIfEndFound();
            ExploreNeighbours(currentNode);

        }
    }

    private List<Waypoint> CreatePath(List<Waypoint> path)
    {
        path.Add(endPoint);
        Waypoint previous = endPoint.parent;
        while (previous != startPoint)
        {
            path.Add(previous);
            previous = previous.parent;
        }
        path.Add(startPoint);
        path.Reverse();
        return path;
    }

    private void HaltIfEndFound()
    {
        if (currentNode == endPoint)
        {
            isRunning = false;
        }
    }

    private void ColorStartAndEnd()
    {
        startPoint.SetTopColor(Color.green);
        endPoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        Array waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if(isOverlapping)
            {
                Debug.Log("Overlapping Block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
            
        }
    }

    // Update is called once per frame
    void Update () {
        
	}

    private void ExploreNeighbours(Waypoint currentNode)
    {
        if(isRunning == false)
        {
            return;
        }

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbour = new Vector2Int(currentNode.GetGridPos().x + direction.x, currentNode.GetGridPos().y + direction.y);
            if (grid.ContainsKey(neighbour))
            {
                Waypoint waypoint = grid[neighbour];
                if (!closedList.Contains(waypoint) && !pathQueue.Contains(waypoint))
                {
                    waypoint.parent = currentNode;
                    //waypoint.SetTopColor(Color.yellow);
                    pathQueue.Enqueue(waypoint);
                }
            }
        }
    }
}
