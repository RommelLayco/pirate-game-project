using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {

    public GameObject room;
    public int width;
    public int height;
    public List<Vector3> gridPositions;
    public List<GameObject> _walltiles; // wall tiles
    public Vector3 shift;

    public Room(GameObject _room, int _x, int _y, List<Vector3> _gridPositions, List<GameObject> _tiles)
    {
        room = _room;
        width = _x;
        height = _y;
        gridPositions = _gridPositions;
        _walltiles = _tiles;
        shift = new Vector3(-1, -1, -1f);
    }


    public int[] getGridPos(int size)
    {
        int[] res = new int[2];
        res[0] = (int)this.shift.x / size;
        res[1] = (int)this.shift.y / size;

        return res;

    }

    public Rect getBounds()
    {
        float xx = room.transform.position.x;
        float yy = room.transform.position.y;
        return new Rect(xx, yy, width, height);
    }

}
