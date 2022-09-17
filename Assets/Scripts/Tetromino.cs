using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Tetromino : MonoBehaviour
{
    [SerializeField] GameObject tetrominoPrefab;
    [SerializeField] int amountOfPlatforms;
    Tetromino menuParent;
    int currentAmountOfPlatforms;
    bool isDragged = false;
    bool isMenuItem = true;
    TextMeshProUGUI tmPro;

    // Start is called before the first frame update
    void Start()
    {
        tmPro = GetComponentInChildren<TextMeshProUGUI>();
        tmPro.text = isMenuItem ? amountOfPlatforms.ToString() : "";
        currentAmountOfPlatforms = amountOfPlatforms;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragged)
        {
            Vector3 mousePositionWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            this.transform.position = new Vector3((float)Math.Floor(mousePositionWorldPoint.x) + 0.5f, (float)Math.Floor(mousePositionWorldPoint.y) + 0.5f, 0);
            if(!Input.GetMouseButton(0))
            {
                if(mousePositionWorldPoint.y > -3.0f) 
                {
                    Debug.Log("placed");
                    isDragged = false;

                    foreach(PlatformEffector2D pe in GetComponentsInChildren<PlatformEffector2D>())
                        pe.colliderMask = 1 << 0;
                }
                else
                {
                    Destroy(gameObject);
                    menuParent.currentAmountOfPlatforms++;
                    menuParent.updateText();
                }
            }
        }
        if(currentAmountOfPlatforms == 0 && isMenuItem)
        {
            gameObject.SetActive(false);
        }
    }

    public void DragNew()
    {
        if(isMenuItem && currentAmountOfPlatforms > 0)
        {
            GameObject instantiatedTetromino = Instantiate(tetrominoPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instantiatedTetromino.GetComponent<Tetromino>().isDragged = true;
            instantiatedTetromino.GetComponent<Tetromino>().isMenuItem = false;
            instantiatedTetromino.GetComponent<Tetromino>().menuParent = this;
            currentAmountOfPlatforms--;
            updateText();
        }
    }

    public void updateText()
    {
        tmPro.text = currentAmountOfPlatforms.ToString();
    }

    public void Reset()
    {
        if (isMenuItem)
        {
            currentAmountOfPlatforms = amountOfPlatforms;
            gameObject.SetActive(true);
            if (tmPro != null)
                updateText();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
