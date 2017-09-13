using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Generator : MonoBehaviour {
    [SerializeField]
    GameObject baseTile;
    [SerializeField]
    int map_Width, map_Height;
    GameObject[,] map;
    Node[,] tiles;
    bool unlinked = true;
	// Use this for initialization
	void Start () {
        //Generating Map
        map = new GameObject[map_Width, map_Height];
        tiles = new Node[map_Width, map_Height];
		for (int i = 0; i < map_Width; i++)
        {
            for (int j = 0; j < map_Height; j++)
            {
                map[i, j] = Instantiate(baseTile, new Vector3(i, j, 0), Quaternion.identity);
                tiles[i, j] = map[i, j].transform.Find("Anchor_Node").GetComponent<Node>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (unlinked)
        {
            ////Setting up tiles.
            //for (int i = 0; i < map_Width; i++)
            //{
            //    for (int j = 0; j < map_Height; j++)
            //    {
            //        tiles[i, j] = map[i, j].GetComponent<Node>();
            //    }
            //}

            //Generating Graph by "Pathable" tiles
            for (int i = 0; i < map_Width; i++)
            {
                for (int j = 0; j < map_Height; j++)
                {
                    /*
                    for (int g = -1; g <= 1; g++)
                    {
                        for (int h = -1; h <= 1; h++)
                        {
                            if ((g != 0 || h != 0) && tiles[i + g, j + h].GetPathable())
                            {
                                tiles[i, j].AddNeighbor(map[i + g, j + h]);
                            }
                        }
                    }
                    */
                    for (int k = -1; k <= 1; k++)
                    {
                        if (i + k >= 0 && i + k < map_Width && k != 0 && tiles[i+k, j].GetPathable())
                        {
                            tiles[i, j].AddNeighbor(map[i + k, j]);
                        }
                    }

                    for (int k = -1; k <= 1; k++)
                    {
                        if (j + k >= 0 && j + k < map_Height && k != 0 && tiles[i, j+k].GetPathable())
                        {
                            tiles[i, j].AddNeighbor(map[i, j + k]);
                        }
                    }

                    if (i >= 1 && j >= 1 && j < map_Height-1 && i < map_Width-1)
                    {
                        if ((tiles[i-1, j].GetPathable() && tiles[i, j-1].GetPathable()) &&tiles[i - 1, j - 1].GetPathable())
                            tiles[i, j].AddNeighbor(map[i - 1, j - 1]);

                        if ((tiles[i + 1, j].GetPathable() && tiles[i, j - 1].GetPathable()) && tiles[i + 1, j - 1].GetPathable())
                            tiles[i, j].AddNeighbor(map[i + 1, j - 1]);

                        if ((tiles[i - 1, j].GetPathable() && tiles[i, j + 1].GetPathable()) && tiles[i - 1, j + 1].GetPathable())
                            tiles[i, j].AddNeighbor(map[i - 1, j + 1]);

                        if ((tiles[i + 1, j].GetPathable() && tiles[i, j + 1].GetPathable()) && tiles[i + 1, j + 1].GetPathable())
                            tiles[i, j].AddNeighbor(map[i + 1, j + 1]);
                    }
                    if (i == 0)
                    {
                        if (j == 0 && tiles[i + 1, j + 1].GetPathable())
                        {
                            tiles[i, j].AddNeighbor(map[i + 1, j + 1]);
                        }
                        if (j == map_Height && tiles[i+1, j-1].GetPathable())
                        {
                            tiles[i, j].AddNeighbor(map[i + 1, j - 1]);
                        }
                    } else if (i == map_Width - 1)
                    {
                        if (j == 0 && tiles[i-1,j+1].GetPathable())
                        {
                            tiles[i, j].AddNeighbor(map[i - 1, j + 1]);
                        }
                        if (j == map_Height && tiles[i - 1, j - 1].GetPathable())
                        {
                            tiles[i, j].AddNeighbor(map[i - 1, j - 1]);
                        }
                    }
                }
            }
            unlinked = false;
        }
    }

    public Node[,] GetTiles()
    {
        return tiles;
    }
}
