using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GachaCardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool mouse_over = false;
    private Quaternion initialRotation;

    public float xTiltFactor = 50f;

    public float yTiltFactor = 25f;

    public float targetScaleFactor;
    private Vector3 targetScale;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = gameObject.transform.rotation;
        Debug.Log(transform.position.y);
        targetScale = transform.localScale * targetScaleFactor;

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.05f);

        if (mouse_over)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(
                    - yTiltFactor * (0.5f - Input.mousePosition.y / Screen.height),
                    xTiltFactor * (0.5f - Input.mousePosition.x / Screen.width),
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
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
}
