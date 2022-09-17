using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Tetromino : MonoBehaviour
{
    [SerializeField] GameObject tetrominoPrefab;
    [SerializeField] int amountOfPlatforms;
    [SerializeField] bool isTree;
    [SerializeField] GameObject squarePrefab;

    Tetromino menuParent;
    int currentAmountOfPlatforms;
    bool isDragged = false;
    bool isMenuItem = true;
    bool growsRight = true;
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
                bool validPlacement = true;
                if (isTree)
                {
                    Collider2D[] intersectingRight = Physics2D.OverlapCircleAll(new Vector3(transform.position.x + 1f, transform.position.y, 0), 0.01f);
                    Collider2D[] intersectingLeft = Physics2D.OverlapCircleAll(new Vector3(transform.position.x - 1f, transform.position.y, 0), 0.01f);
                    
                    if (intersectingRight.Length != 0)
                    {
                        growsRight = false;
                    }
                    else if (intersectingLeft.Length != 0)
                    {
                        growsRight = true;
                    }
                    else
                    {
                        Debug.Log("Invalid tree placement");
                        validPlacement = false;
                    }
                }
                if (mousePositionWorldPoint.y > -3.0f && validPlacement) 
                {
                    isDragged = false;

                    foreach (PlatformEffector2D pe in GetComponentsInChildren<PlatformEffector2D>())
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

    public void fastForward()
    {
        if(isTree && !isMenuItem)
        {
            for(int i = 0; i < 3; i++)
            {
                int direction = growsRight ? 1 : -1;
                Instantiate(squarePrefab, this.transform.position + new Vector3(direction * (i + 1), 0, 0), Quaternion.identity);
            }
        }
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
