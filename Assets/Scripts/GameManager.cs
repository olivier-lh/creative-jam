using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject respawnPoint;
    private bool AttemptIsStarted = false;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        AttemptIsStarted = false;
        //Instantiate(player, respawnPoint.transform);
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
