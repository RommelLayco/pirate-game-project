using UnityEngine;
using System.Collections;

public class Room {

    public GameObject room;
    public int x;
    public int y;

    public Room(GameObject _room, int _x, int _y)
    {
        room = _room;
        x = _x;
        y = _y;
    }

}
