using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GachaCardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool mouse_over = false;
    private Quaternion initialRotation;


    // Start is called before the first frame update
    void Start()
    {
        initialRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse_over)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(
                    - 0.05f * (transform.position.y - Input.mousePosition.y),
                    0.1f * (transform.position.x - Input.mousePosition.x),
                    0
                ),
                0.05f
            );
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, 0.01f); 
        }
        //Debug.Log(Input.mousePosition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("Mouse exit");
    }
}
