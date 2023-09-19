using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> spriteList;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Change() {
        int index = Random.Range(0, spriteList.Count);
        SetSprite(index);
        return index;
    }

    void SetSprite(int i)
    {
        spriteRenderer.sprite = spriteList[i]; 
    }
}
