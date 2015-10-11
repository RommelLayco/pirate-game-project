using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {

    public GameObject room;
    public int x;
    public int y;
    public List<Vector3> gridPositions;
    public List<GameObject> tiles;
    public Vector3 shift;

    public Room(GameObject _room, int _x, int _y, List<Vector3> _gridPositions, List<GameObject> _tiles)
    {
        room = _room;
        x = _x;
        y = _y;
        gridPositions = _gridPositions;
        tiles = _tiles;
        shift = new Vector3(-1, -1, -1f);
    }



}
