using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Movement>() != null)
        {
            foreach (Tetromino tetro in Resources.FindObjectsOfTypeAll(typeof(Tetromino)))
                tetro.fastForward();
        }
    }
}
