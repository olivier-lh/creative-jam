using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    GameObject parent;
    //bool callOnce = false;
    // Start is called before the first frame update
    bool ready = true;
    int stayCount = 0;
    int maxStayCount = 5;
    void Start()
    {
        parent = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter " + ready.ToString());
        if (other.CompareTag("Ground") && ready)
        {
            Debug.Log("Triggered");
            parent.GetComponent<Movement>().Invert();
            ready = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out " + ready.ToString());
        if (other.CompareTag("Ground"))
        {
            ready = true;
        }
    }
}
