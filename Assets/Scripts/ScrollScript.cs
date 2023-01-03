using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 curPos = gameObject.transform.position;
        curPos = new Vector3(0, -25.0f, 0);
        gameObject.transform.position = curPos;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = gameObject.transform.position;
        curPos = new Vector3(curPos.x, curPos.y + scrollSpeed, curPos.z);

        if (curPos.y > 25.0)
        {
            curPos.y = -25.0f;
        }

        gameObject.transform.position = curPos;
    }
}
