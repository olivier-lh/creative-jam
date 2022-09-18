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
    [SerializeField] bool isMenuItem = true;
    [SerializeField] bool isLevelBlock = false;

    Tetromino menuParent;
    int currentAmountOfPlatforms;
    bool isDragged = false;

    private Sprite rock_0, rock_1, rock_2;

    bool growsRight = true;
    TextMeshProUGUI tmPro;

    // Start is called before the first frame update
    void Start()
    {
        rock_0 = Resources.Load<Sprite>("rock_0");
        rock_1 = Resources.Load<Sprite>("rock_1");
        rock_2 = Resources.Load<Sprite>("rock_2");
        tmPro = GetComponentInChildren<TextMeshProUGUI>();
        tmPro.text = isMenuItem ? amountOfPlatforms.ToString() : "";
        currentAmountOfPlatforms = amountOfPlatforms;
        if (isRock)
        {
            foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.sprite = rock_0;
            }
        }
        if (isTree)
        {
            PlaceTree();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragged)
        {
            Vector3 mousePositionWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            this.transform.position = new Vector3((float)Math.Floor(mousePositionWorldPoint.x) + 0.5f, (float)Math.Floor(mousePositionWorldPoint.y) + 0.5f, 0);
            if (!Input.GetMouseButton(0))
            {
                bool validPlacement = true;
                if (isTree)
                {
                    validPlacement = PlaceTree();
                }
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    GameObject childObject = this.transform.GetChild(i).gameObject;
                    SpriteRenderer sr = childObject.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        Collider2D[] intersectingCenter = Physics2D.OverlapCircleAll(new Vector3(childObject.transform.position.x, childObject.transform.position.y, 0), 0.01f);
                        if (intersectingCenter.Length > 2 || childObject.name == "SquareUp" && intersectingCenter.Length > 1)
                        {
                            Debug.Log(intersectingCenter.Length);
                            validPlacement = false;
                        }
                    }
                }
                
                if (/*mousePositionWorldPoint.y > -3.0f && */validPlacement)
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

    private bool PlaceTree()
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
            return false;
        }
        return true;
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
            SpriteRenderer sr = childObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                childObject.SetActive(true);
                if (isRock)
                    sr.sprite = rock_0;
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
            SpriteRenderer childSr = childObject.GetComponent<SpriteRenderer>();

            if (childObject.GetComponent<SpriteRenderer>() != null)
            {
                childSr.sprite = rock_1;
                yield return new WaitForSeconds(0.5f);
                childSr.sprite = rock_2;
                yield return new WaitForSeconds(0.5f);
                childObject.SetActive(false);
            }
            //if (childObject != null)
            //{
            //    yield return new WaitForSeconds(1);
            //}
        }
    }
}
