using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementCleanup : MonoBehaviour
{
    private GameObject player;

    public int placementOffsetX = 40;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DeleteGameObjectByPosition();
    }

    /// <summary>
    /// Destroy gameobject when it has past the player position plus offset
    /// </summary>
    private void DeleteGameObjectByPosition()
    {
            float posX = player.transform.position.x; //get current player pos
            float objectPosX = transform.position.x; //get gameobject pos

            if (posX > objectPosX + placementOffsetX)
            {
                Destroy(gameObject);
            }
    }
}
