using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private string[] textValue;
    [SerializeField] private float textSpeed = 0.1f;
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject continueButton;
    private int currentDisplayingText = 0;

    public void ActivateText()
    {
        if (currentDisplayingText == textValue.Length)
        {
            StopCoroutine(AnimateText());
            textBox.SetActive(false);
        }
        else
        {
            StartCoroutine(AnimateText());
        }
    }

    IEnumerator AnimateText()
    {
        //Debug.Log(textValue[currentDisplayingText]);
        for (int i = 0; i < textValue[currentDisplayingText].Length + 1; i++)
        {
            textObject.text = textValue[currentDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
        if (currentDisplayingText <= textValue.Length)
        {
            currentDisplayingText++;
        }
    }
    
}
