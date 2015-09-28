using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeBuilder : MonoBehaviour {


    public int rooms_Horizontal = 1;
    public int rooms_Vertical = 1;

    public int room_Columns = 5;
    public int room_Rows = 5;

    private RoomBuilder room;
    private EnemyRoom enemyRoom;

	// Use this for initialization
	void Awake () {
        //Get a component reference to the attached BoardManager script
        room = GetComponent<RoomBuilder>();
        enemyRoom = GetComponent<EnemyRoom>();

        //build the room
        InitMaze();
    }
	
	// Update is called once per frame
	void InitMaze () {
        room.BuildRoom(0,0);

        enemyRoom.BuildRoom(0, 7);
        
    }

}
