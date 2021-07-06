using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public struct Hex
{
    public TileBase tile;
    //dynamic value
    public TextMeshProUGUI textMesh;

    public int cost;
    public int income;
    public bool captured;

    public Hex(TileBase tile, int cost, int income)
    {
        this.tile = tile;
        this.cost = cost;
        this.income = income;
        captured = false;
        textMesh = null;
    }

    public Hex(TileBase tile, int cost, int income, bool captured)
    {
        this.tile = tile;
        this.cost = cost;
        this.income = income;
        this.captured = captured;
        textMesh = null;
    }

    //list of all types of hexagon tiles
    public static Dictionary<HexType, Hex> Hexes = new Dictionary<HexType, Hex>()
    {
        { HexType.dirt, new Hex(Resources.Load<TileBase>("Tiles/Terrain/Dirt/dirt_06"), 3, 1) },
        { HexType.grass, new Hex(Resources.Load<TileBase>("Tiles/Terrain/Grass/grass_05"), 5, 2) },
        { HexType.sand, new Hex(Resources.Load<TileBase>("Tiles/Terrain/Sand/sand_07"), 2, 1) },
        { HexType.forest, new Hex(Resources.Load<TileBase>("Tiles/Terrain/Grass/grass_12"), 12, 3) },
        { HexType.cactus, new Hex(Resources.Load<TileBase>("Tiles/Terrain/Sand/sand_14"), 8, 2) },
        { HexType.dirtRock, new Hex(Resources.Load<TileBase>("Tiles/Terrain/Dirt/dirt_16"), 15, 4) },
        { HexType.dirtRockForest, new Hex(Resources.Load<TileBase>("Tiles/Terrain/Dirt/dirt_18"), 22, 6) },
    };
}

public enum HexType
{ 
    dirt,
    grass,
    forest,
    sand,
    cactus,
    dirtRock,
    dirtRockForest,
    NumberOfTypes
}