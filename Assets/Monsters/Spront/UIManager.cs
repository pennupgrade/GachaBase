using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI intercepted;
    public TextMeshProUGUI caught;

    void Start()
    {
        DisableInterceptedText();
        DisableCaughtText();
    }

    public void EnableInterceptedText(float x, float y){
        intercepted.gameObject.SetActive(true);
        intercepted.gameObject.transform.position = new Vector3(x, y, 0);
    }

    public void DisableInterceptedText(){
        intercepted.gameObject.SetActive(false);
    }

    public void EnableCaughtText(float x, float y){
        caught.gameObject.SetActive(true);
        caught.gameObject.transform.position = new Vector3(x, y, 0);
    }

    public void DisableCaughtText(){
        caught.gameObject.SetActive(false);
    }
}
