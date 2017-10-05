using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueTracker : MonoBehaviour {
  public int myValue;
  public float myMaskValue;
  int[] neighbors;
  public Sprite[] TileSet;

  [Serializable]
  public struct Dict {
    public int val;
    //public Sprite retVal;
    public int index;
  }

  public Dict[] SpriteTable;
  TileBitmask mask;

  // Use this for initialization
  void Start () {
    mask = GameObject.Find("Main Camera").GetComponent<TileBitmask>();
    //TileSet = mask.myTileSet;
    myValue = mask.MyMapGridValue((int)transform.position.x, (int) transform.position.y);
    myMaskValue = 0;
    neighbors = new int[8];
    transform.parent = mask.Holder;
    StartCoroutine(Stall());
	}

  int FindSprite(){
    for (int i = 0; i < SpriteTable.Length; i++)
    {
      if (SpriteTable[i].val == myMaskValue){
        return SpriteTable[i].index;
      }
    }
    return SpriteTable[3].index;
  }

  IEnumerator Stall(){
    yield return null;
    for (int i = 0; i < 8; i++)
    {
      neighbors[i] = 0;
    }
    int counter = 0;
    for (int i = -1; i <= 1; i++)
    {
      for (int j = -1; j <= 1; j++)
      {
        if ((int)transform.position.x + i < mask.myMap.width && (int)transform.position.x + i >= 0 && (int)transform.position.y + j < mask.myMap.height && (int)transform.position.y + j >= 0)
        {
          if (!(i == 0 && j == 0))
          {
            neighbors[counter] = (mask.MyMapGridValue((int)transform.position.x + i, (int)transform.position.y + j) == 1 ? 1 : 0);
            counter++;
          }
        }
        else
        {
          neighbors[counter] = 1;
          counter++;
        }
      }
    }
    for (int i = 0; i < neighbors.Length; i++)
    {
      //myMaskValue += neighbors[i];
      myMaskValue += (neighbors[i] > 0 ? 1 : 0) * Mathf.Pow(2, i);
    }

    if (myValue == 0)
    {
      myMaskValue = -1;
      GetComponent<SpriteRenderer>().sprite = TileSet[FindSprite()];
    }
    else
    {
      GetComponent<SpriteRenderer>().sprite = TileSet[FindSprite()];
    }
  }
}
