using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionUtils : MonoBehaviour
{
    public static float maxDistance = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector2 getSnapVectorForPlayer(Rigidbody2D playerRigidbody) {
        Rigidbody2D player1Body = GameObject.FindGameObjectWithTag("Test1").GetComponent<Rigidbody2D>();
        Rigidbody2D player2Body = GameObject.FindGameObjectWithTag("Test2").GetComponent<Rigidbody2D>();

        Vector2 player1Pos = player1Body.transform.position;
        Vector2 player2Pos = player2Body.transform.position;

        Vector2 snapVector;

        if (playerRigidbody == player1Body) {
            snapVector = player2Pos - player1Pos;
        }
        else {
            snapVector = player1Pos - player2Pos;
        }

        snapVector.Normalize();
        return snapVector;
    }

    public static float getSnapMagnitude() {
        Rigidbody2D player1Body = GameObject.FindGameObjectWithTag("Test1").GetComponent<Rigidbody2D>();
        Rigidbody2D player2Body = GameObject.FindGameObjectWithTag("Test2").GetComponent<Rigidbody2D>();

        Vector2 player1Pos = player1Body.transform.position;
        Vector2 player2Pos = player2Body.transform.position;

        float distance = Vector2.Distance(player1Pos, player2Pos);

        if (distance > 7.0f) {
            return 30.0f;
        }
        else if (distance > 5.0f) {
            return 20.0f;
        }
        else if (distance > 4.0f) {
            return 10.0f;
        }

        return 0.0f;
    }
}
