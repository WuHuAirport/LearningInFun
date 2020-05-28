using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ballon : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] ballonSprites;
    private Sprite curSprite;
    public string wordString;
    public bool canRise=true;
    public float riseSpeed = 2.0f;

    public Text textPrefab;
    public Text wordText;
    //RectTransform rectTrans;//UI元素（如：血条等）
    private Vector2 offset=new Vector2(0,30.0f);//偏移量

    private void Awake()
    {
        //curSprite=transform.GetComponentInChildren<SpriteRenderer>().sprite;//初始化随机颜色
        int i = Random.Range(0, 2);
        //Debug.Log()
        transform.GetComponentInChildren<SpriteRenderer>().sprite = ballonSprites[i];
    }

    void Start()
    {
        wordText = (Text)Instantiate(textPrefab);
        wordText.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        wordText.text = wordString;
    }

    // Update is called once per frame
    void Update()
    {
        //两次上升
        if(canRise)
        {
            transform.Translate(0, riseSpeed*Time.deltaTime, 0);
        }

        //Text跟随
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log()
        wordText.rectTransform.position = screenPos +offset;
        //tempText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    
}
