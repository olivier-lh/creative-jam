using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public Button suchButton;
    [SerializeField] GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        suchButton = gameObject.GetComponent<Button>();
        suchButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        foreach (Tetromino tetro in Resources.FindObjectsOfTypeAll(typeof(Tetromino)))
            tetro.ResetTetro();
        gm.RespawnPlayer();
    }
}
