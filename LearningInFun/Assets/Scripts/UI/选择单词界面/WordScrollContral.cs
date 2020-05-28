using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordScrollContral : MonoBehaviour
{

    private int[] leftDir = new int[3] { 30, 205, 385 };
    private int height = 60;
    private int nextHeight = 60;
    private int heightPuls = 40;
    private int width = 160;
    private int lineCount = 3;
    private float originHeight;
    int wordCount = 0;
    public bool isClearing = false;
    public GameObject button;


    private void Start()
    {
        originHeight = GetComponent<RectTransform>().rect.height;
        nextHeight = height;
    }

    public void BuildWordButton(string word)
    {

        GameObject newWord = GameObject.Instantiate(button, transform);
        newWord.GetComponentInChildren<Text>().text = word;
        RectTransform tf = newWord.GetComponent<RectTransform>();
        tf.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, leftDir[wordCount % lineCount], width);
        tf.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, nextHeight, 30);
        wordCount++;
        if (wordCount % lineCount == 0)
            nextHeight += heightPuls;
        if (nextHeight >= GetComponent<RectTransform>().rect.height)
        {
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                GetComponent<RectTransform>().rect.height + heightPuls);
        }

    }

    private void Update()
    {
        Button deleteTarget = GetComponentInChildren<Button>();
        if (!deleteTarget)
            isClearing = false;
        if (isClearing)
        {

            GameObject button = deleteTarget.gameObject;
            Object.Destroy(button);
            wordCount--;
            if (wordCount <= 0)
            {
                isClearing = false;
                GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Vertical, originHeight);
            }
        }
    }

    public void ClearWordButton()
    {

        isClearing = true;
        nextHeight = height;
    }
}
