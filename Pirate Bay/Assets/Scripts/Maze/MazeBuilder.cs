using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MazeBuilder : MonoBehaviour {

    public int max_number_of_rooms = 2;
    public int min_x_room_size = 5;
    public int max_x_room_size = 10;
    public int min_y_room_size = 5;
    public int max_y_room_size = 10;
    public LayerMask floor;

    private Vector3 shift = new Vector3(10,10,0f);

    public int room_Columns = 5;
    public int room_Rows = 5;

    private RoomBuilder roombuilder;
    private EnemyRoom enemyRoom;

    private List<Room> rooms;

	// Use this for initialization
	void Awake () {
        //Get a component reference to the attached BoardManager script
        roombuilder = GetComponent<RoomBuilder>();
        enemyRoom = GetComponent<EnemyRoom>();

        rooms = new List<Room>();

        //build the room
        InitMaze();
    }
	
	// Update is called once per frame
	void InitMaze () {

        for(int i = 0; i < max_number_of_rooms; i++)
        {
            int x = Random.Range(min_x_room_size, max_x_room_size + 1);
            int y = Random.Range(min_y_room_size, max_y_room_size + 1);
      
            Room roomInfo = roombuilder.BuildRoom(x, y);

            if(i == 1)
            {
                Room r1 = rooms[0];


               // OverlapArea(V, Vector2 pointB, floor);

            }

            rooms.Add(roomInfo);
        }
        

        


        
        
    }

}
