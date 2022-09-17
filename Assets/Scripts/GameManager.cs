using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject respawnPoint;
    private bool AttemptIsStarted = false;
        
    // Start is called before the first frame update
    void Start()
    {
        RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Tetromino tetro in Resources.FindObjectsOfTypeAll(typeof(Tetromino)))
                tetro.fastForward();
        }
    }

    public void RespawnPlayer()
    {
        AttemptIsStarted = false;
        Debug.Log(respawnPoint.transform.position);
        Instantiate(player, respawnPoint.transform.position, Quaternion.identity);
        foreach (Tetromino tetro in Resources.FindObjectsOfTypeAll(typeof(Tetromino)))
            tetro.ResetTetro();
    }

    public void StartAttempt()
    {
        AttemptIsStarted = true;
    }

    public bool getAttemptIsStarted()
    {
        return AttemptIsStarted;
    }
}
