using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pathfinding_Script : MonoBehaviour {

    [SerializeField]
    int tarX, tarY, mapWidth = 50, mapHeight = 50;
    [SerializeField]
    float pathDelayTimer = 0f;
    float timer = 0;
    bool findNewPath = false;

    [SerializeField]
    float pathDelay;
    [SerializeField]
    List<Transform> myPath;
    Node[,] tileSet;

    [SerializeField]
    Node Current;

    [SerializeField]
    Node TargetNode;
    List<Vector3> positions;
    // Use this for initialization
    void Awake () {
        transform.SetPositionAndRotation(new Vector3((int)Random.Range(0, mapWidth), (int)Random.Range(0, mapHeight), transform.position.z), Quaternion.identity);
        tarX = (int) Random.Range(0, mapWidth);
        tarY = (int)Random.Range(0, mapHeight);
        positions = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Current == null)
        {
            Debug.DrawRay(transform.position, Vector3.forward);
            RaycastHit info;
            if (Physics.Raycast(new Ray(transform.position, Vector3.forward), out info) && info.transform.Find("Anchor_Node"))
            {
                Current = info.transform.Find("Anchor_Node").GetComponent<Node>();
                Current.SetPathable(true);
                Current.transform.parent.GetComponent<MeshRenderer>().material.color = Color.grey;
            }
        }

        
        RaycastHit screenRayInfo;
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out screenRayInfo) && screenRayInfo.transform.Find("Anchor_Node").GetComponent<Node>().GetPathable())
        {
            RaycastHit info;
            if (Physics.Raycast(new Ray(transform.position, Vector3.forward), out info) && info.transform.Find("Anchor_Node"))
            {
                Current = info.transform.Find("Anchor_Node").GetComponent<Node>();
                Current.SetPathable(true);
                Current.transform.parent.GetComponent<MeshRenderer>().material.color = Color.grey;
            }

            tarX = (int) screenRayInfo.point.x;
            tarY = (int) screenRayInfo.point.y;
            myPath = FindPath(tarX, tarY);

            foreach (Transform ctx in myPath)
            {
                ctx.position = new Vector3(ctx.position.x, ctx.position.y, transform.position.z);
            }
            myPath.Reverse();

            foreach (Transform ctx in myPath)
            {
                ctx.parent.GetComponent<MeshRenderer>().material.color = Color.cyan;
                ctx.position.Scale(new Vector3(1, 1, 0));
                 positions.Add(ctx.position);
            }
        }
        /*
        if (myPath.Count > 0 && timer > .25f)
        {
            transform.SetPositionAndRotation(myPath[0].position, Quaternion.identity);
            myPath[0].parent.GetComponent<MeshRenderer>().material.color = Color.gray;
            myPath.RemoveAt(0);
            timer = 0;
        }*/
        transform.position = Bezier(timer / 10, positions);

        timer +=  Time.deltaTime;

    }

    List<Transform> FindPath(int targetX, int targetY)
    {
        if (!Current.GetPathable())
            return null;
        //Resetting Graph
        tileSet = GameObject.Find("Tile_Manager").GetComponent<Map_Generator>().GetTiles();
        foreach (Node ctx in tileSet)
        {
            ctx.SetVisited(false);
            ctx.SetBackNode(null);
        }
        
        //Setting up for iteration
        List<Transform> path = new List<Transform>();
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(Current);
        Current.SetVisited(true);
        
        TargetNode = null;
        RaycastHit info;
        if (Physics.Raycast(new Ray(new Vector3(targetX, targetY, -1), Vector3.forward), out info) && info.transform.Find("Anchor_Node")){
            TargetNode = info.transform.Find("Anchor_Node").GetComponent<Node>();
        }
        //Iterating
        while (queue.Count > 0)
        {
            Node temp = queue.Peek();
            queue.Dequeue();

            if (temp == TargetNode)
            {
                while (temp != null)
                {
                    path.Add(temp.transform);
                    temp = temp.GetBackNode();
                }
                return path;
            }
            
            for (int i = 0; i < temp.GetNeighbors().Count; i++)
            {
                if (!temp.GetNeighbors()[i].transform.Find("Anchor_Node").GetComponent<Node>().GetVisited())
                {
                    temp.GetNeighbors()[i].transform.Find("Anchor_Node").GetComponent<Node>().SetVisited(true);
                    temp.GetNeighbors()[i].transform.Find("Anchor_Node").GetComponent<Node>().SetBackNode(temp);
                    queue.Enqueue(temp.GetNeighbors()[i].transform.Find("Anchor_Node").GetComponent<Node>());
                }
            }
        }

        return path;
    }

    Vector3 Bezier (float u, List<Vector3> tList)
    {
        Vector3 result = Vector3.zero;
        if (tList.Count == 1)
        {
            return tList[0];
        }
        List<Vector3> ListR = tList.GetRange(1, tList.Count - 1);
        List<Vector3> ListL = tList.GetRange(0, tList.Count - 1);
        result = Lerp(Bezier(u, ListL), Bezier(u, ListR), u);
        return result;
    }

    Vector3 Lerp(Vector3 from, Vector3 to, float u)
    {
        Vector3 res = (1 - u) * from + u * to;
        return res;
    }
}
