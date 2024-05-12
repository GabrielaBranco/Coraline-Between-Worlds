using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject catPlayer;
    [SerializeField] private ObjectFollow follow;
    private bool cat;

    void Start()
    {
        // Disable one of the players at the start of the game
        player.GetComponent<Player>().enabled = true;
        catPlayer.GetComponent<Player>().enabled = false;
        follow.ChangeTarget(player.transform);
        cat = false;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (cat)
            {
                cat = false;
                player.GetComponent<Player>().enabled = true;
                catPlayer.GetComponent<Player>().enabled = false;
                follow.ChangeTarget(player.transform);
            }
            else if (!cat)
            {
                cat = true;
                player.GetComponent<Player>().enabled = false;
                catPlayer.GetComponent<Player>().enabled = true;
                follow.ChangeTarget(catPlayer.transform);
            }
        }
    }
}
