using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class MapUpdater : MonoBehaviour
{
    [SerializeField]
    TileBase tb;
    [SerializeField]
    Tilemap tilemap;

    [SerializeField]
    int width, height;

    static Hex[,] map;
    Transform canvas;

    public static Vector3Int[] evenVectors = { new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0),
                                                new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0) };
    public static Vector3Int[] oddVectors = { new Vector3Int(1, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0),
                                                new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0) };

    private void Awake()
    {
        canvas = GameObject.Find("WorldSpaceCanvas").transform;
        map = new Hex[width, height];
        //set camera to center of map (the right way would be 'mathf.Ceil(width / 2 - 1)' etc.)
        Camera.main.transform.position = tilemap.CellToWorld(new Vector3Int(width / 2, height / 2, -10));
    }

    void Start()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //random map generation (as test project)
                HexType HT = (HexType)Random.Range(0, (int)HexType.NumberOfTypes);
                map[i, j] = Hex.Hexes[HT];

                //Create text ui
                var temp = Instantiate(Resources.Load<GameObject>("Prefabs/Text (TMP)"), canvas);
                temp.transform.position = tilemap.CellToWorld(new Vector3Int(i, j, -5));
                temp.GetComponent<TextMeshProUGUI>().text = map[i, j].cost.ToString();
                
                //link TMPro with Hex
                map[i, j].textMesh = temp.GetComponent<TextMeshProUGUI>();
            }
        }

        //setting player base as owned
        CaptureTile(width / 2, height / 2, true);

        //Place player character on owned starter tile
        Instantiate(Resources.Load<GameObject>("Prefabs/PlayerChar"), tilemap.CellToWorld(new Vector3Int(width/2, height/2, 0)), Quaternion.identity);
        
        UpdateMap();
    }

    //full map update method
    public void UpdateMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), map[i, j].tile);
            }
        }
    }

    public void CaptureTile(int x, int y, bool free)
    {
        ref Hex hex = ref map[x, y];
        hex.captured = true;
        hex.textMesh.text = "+" + hex.income.ToString();
        hex.textMesh.color = Color.yellow;
        Income.AddHex(hex);
    }
    public void CaptureTile(int x, int y)
    {
        ref Hex hex = ref map[x, y];
        hex.captured = true;
        hex.textMesh.text = "+" + hex.income.ToString();
        hex.textMesh.color = Color.yellow;
        Income.AddHex(hex);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - hex.cost);
        UIhandler.UpdateMoney();
    }

    public bool CanCapture(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return false;

        ref Hex hex = ref map[x, y];
        if (PlayerPrefs.GetInt("Money") < hex.cost)
            return false;
        else if (hex.captured)
            return false;
        else if (!IsNeighbor(x, y))
            return false;
        else
            return true;
    }

    bool IsNeighbor(int x, int y)
    {
        if (y % 2 == 0)
        {
            foreach (Vector3Int v in MapUpdater.evenVectors)
            {
                Vector2Int neighbor = new Vector2Int(v.x + x, v.y + y);
                if (neighbor.x < 0 || neighbor.x >= width || neighbor.y < 0 || neighbor.y >= height)
                    continue;
                if (map[neighbor.x, neighbor.y].captured)
                    return true;
            }
            return false;
        }
        else
        {
            foreach (Vector3Int v in MapUpdater.oddVectors)
            {
                Vector2Int neighbor = new Vector2Int(v.x + x, v.y + y);
                if (neighbor.x < 0 || neighbor.x > width || neighbor.y < 0 || neighbor.y > height)
                    continue;
                if (map[neighbor.x, neighbor.y].captured)
                    return true;
            }
            return false;
        }
    }
}