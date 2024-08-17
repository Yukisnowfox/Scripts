using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PathFindingManager : MonoBehaviour
{
    public static PathFindingManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one PathFinderManager in scene!");
            return;
        }
        instance = this;
    }
    
    public GameObject _startPoint;
    public GameObject _endPoint;

    public Vector3 startPoint;
    public Vector3 endPoint;

    private NavMeshObstacle navMeshObstacle;

    void Start ()
    {
        if (_startPoint != null)
        {
            startPoint = _startPoint.transform.position;
            Debug.Log("Start Point set to: " + startPoint);
        }
        else
        {
            Debug.LogError("_startPoint not assigned.");
        }
        
        // You can now use startPoint in pathfinding calculations
        CalculatePath();
    }

    void CalculatePath()
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(startPoint, endPoint, NavMesh.AllAreas, path);
        // Handle the path (e.g., display it or use it for an agent)
    }
}