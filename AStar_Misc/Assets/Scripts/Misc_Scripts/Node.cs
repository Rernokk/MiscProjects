using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    bool pathable = true;
    [SerializeField]
    List<GameObject> neighbors;
    bool visited = false;
    Node backNode = null;

    public void SetPathable(bool ctx)
    {
        pathable = ctx;
    }

    public void AddNeighbor(GameObject ctx)
    {
        neighbors.Add(ctx);
    }

    public bool GetPathable()
    {
        return pathable;
    }

    public List<GameObject> GetNeighbors()
    {
        return neighbors;
    }

    private void Start()
    {
        if (pathable)
        {
            transform.parent.GetComponent<MeshRenderer>().material.color = Color.grey;
        } else
        {
            transform.parent.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        neighbors = new List<GameObject>();
    }

    public void SetVisited(bool visit)
    {
        visited = visit;
    }

    public bool GetVisited()
    {
        return visited;
    }

    public void SetBackNode(Node node)
    {
        backNode = node;
    }

    public Node GetBackNode()
    {
        return backNode;
    }
}
