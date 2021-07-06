using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap, ui;
    [SerializeField]
    TileBase uiHover;
    [SerializeField]
    MapUpdater mapUpdater;

    Vector3Int lastTile = new Vector3Int(0, 0, 0);

    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        if (ui.WorldToCell(pos) != lastTile)
        {
            ui.SetTile(lastTile, null);
            ui.SetTile(ui.WorldToCell(pos), uiHover);
            lastTile = ui.WorldToCell(pos);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var cell = tilemap.WorldToCell(pos);
            if (mapUpdater.CanCapture(cell.x, cell.y))
            {
                mapUpdater.CaptureTile(cell.x, cell.y);
            }
        }
    }
}
