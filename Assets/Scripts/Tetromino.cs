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
    [SerializeField] bool isRock;
    [SerializeField] GameObject squarePrefab;
    [SerializeField] Sprite bigTreeSprite;
    [SerializeField] Sprite rockSprite;
    [SerializeField] bool isMenuItem = true;
    [SerializeField] bool isLevelBlock = false;

    Tetromino menuParent;
    int currentAmountOfPlatforms;
    bool isDragged = false;
    
    bool growsRight = true;
    TextMeshProUGUI tmPro;

    // Start is called before the first frame update
    void Start()
    {
        tmPro = GetComponentInChildren<TextMeshProUGUI>();
        tmPro.text = isMenuItem ? amountOfPlatforms.ToString() : "";
        currentAmountOfPlatforms = amountOfPlatforms;
        if(isRock)
        {
            foreach(SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.sprite = rockSprite;
            }
        }
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
                        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = !growsRight;
                    }
                    else if (intersectingLeft.Length != 0)
                    {
                        growsRight = true;
                        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = !growsRight;
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
        tmPro.text = isMenuItem && !isLevelBlock ? currentAmountOfPlatforms.ToString() : "";
    }

    public void fastForward()
    {
        if(isTree && !isMenuItem)
        {
            for(int i = 0; i < 3; i++)
            {
                GameObject block = transform.GetChild(1).gameObject;
                
                int direction = growsRight ? 1 : -1;
                block.transform.localScale = new Vector3(3, 1, 1);
                block.transform.position = new Vector3(this.transform.position.x + direction, this.transform.position.y, this.transform.position.z);
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = bigTreeSprite;
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = !growsRight;
            }
        }
        else if(isRock && !isMenuItem)
        {
            StartCoroutine(ErodeRocks());
        }
    }
    public void ResetTetro()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject childObject = this.transform.GetChild(i).gameObject;

            if (childObject.GetComponent<SpriteRenderer>() != null)
            {
                childObject.SetActive(true);
            }
        }
        if (isMenuItem || isLevelBlock)
        {
            currentAmountOfPlatforms = amountOfPlatforms;
            if (tmPro != null)
                updateText();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ErodeRocks()
    {
        int numOfChildren = this.transform.childCount;
        for (int i = 0; i < numOfChildren; i++)
        {
            GameObject childObject = this.transform.GetChild(i).gameObject;

            if (childObject.GetComponent<SpriteRenderer>() != null)
            {
                childObject.SetActive(false);
            }
            if (childObject != null)
            {
                yield return new WaitForSeconds(1);
            }
        }
    }
}
