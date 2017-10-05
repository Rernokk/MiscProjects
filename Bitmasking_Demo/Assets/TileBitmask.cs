using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBitmask : MonoBehaviour
{
  [Serializable]
  public struct LayerCombo {
    public Sprite[] TileSet;
    public Color matchingColor;
    public GameObject Object;
    public int Value;
  }

  public Texture2D myMap;
  public GameObject tile;
  public Sprite[] myTileSet;
  public Transform Holder;
  public List<LayerCombo> TileCombos;
  int[,] mapGrid;
  Color[,] colorGrid;

  void Start()
  {
    Holder = new GameObject().transform;
    mapGrid = new int[myMap.height, myMap.width];
    colorGrid = new Color[myMap.height, myMap.width];
    for (int i = 0; i < myMap.width; i++)
    {
      for (int j = 0; j < myMap.height; j++)
      {
        colorGrid[j, i] = myMap.GetPixel(j, i);
        mapGrid[j, i] = GetValue(colorGrid[j,i]);
      }
    }

    for (int i = 0; i < myMap.width; i++)
    {
      for (int j = 0; j < myMap.height; j++) {
        GameObject temp = GetTileObject(colorGrid[j, i]);
        if (temp != null) {
          temp = Instantiate(temp, new Vector2(j, i), Quaternion.identity);
          if (temp.GetComponent<ValueTracker>())
          {
            temp.GetComponent<ValueTracker>().myValue = GetValue(colorGrid[j, i]);
          }
        }
      }
    }
  }

  public int MyMapGridValue(int objX, int objY){
    return mapGrid[objX, objY];
  }

  public GameObject GetTileObject(Color col){
    foreach (LayerCombo c in TileCombos){
      if (c.matchingColor == col){
        return c.Object;
      }
    }
    return null;
  }

  public int GetValue(Color col){
    foreach(LayerCombo c in TileCombos){
      if (c.matchingColor == col){
        return c.Value;
      }
    }
    return 0;
  }

  public Sprite[] FetchTileSet(Color col){
    foreach (LayerCombo c in TileCombos){
      if (c.matchingColor == col){
        return c.TileSet;
      }
    }
    return null;
  }
}
