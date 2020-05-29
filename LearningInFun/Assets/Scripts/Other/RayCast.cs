using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.canHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    //Debug.Log(hit.transform.position);
                    //HitObj是你需要检测的物体的标签
                    if (hit.collider.tag == "Ballon")
                    {
                        Debug.Log("click make");
                        GameManager.Instance.canHit = false;
                        if(hit.transform.gameObject.GetComponent<Ballon>().wordString==
                            GameManager.Instance.curWordString)
                        {
                            GameManager.Instance.AfterHitUpdate(true,hit.transform.gameObject);
                            Instantiate(GameManager.Instance.rightParticle, hit.transform.position, Quaternion.identity);
                            Destroy(hit.transform.gameObject.GetComponent<Ballon>().wordText);
                            Destroy(hit.transform.gameObject);

                        }
                        else
                        {
                            GameManager.Instance.AfterHitUpdate(false, hit.transform.gameObject);
                            Instantiate(GameManager.Instance.errorParticle, hit.transform.position, Quaternion.identity);
                            Destroy(hit.transform.gameObject.GetComponent<Ballon>().wordText);
                            Destroy(hit.transform.gameObject);
                        }
                        

                        //Debug.Log("hit one");
                    }
                }
            }
        }

    }
}
