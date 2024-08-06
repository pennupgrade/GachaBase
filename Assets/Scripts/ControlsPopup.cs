using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

enum ControlType
{
    Space,
    Wasd,
    Mouse
}

public class ControlsPopup : MonoBehaviour
{
    [SerializeField] private ControlType controlType;

    [SerializeField] private RectTransform overlay;
    [SerializeField] private GameObject imageSpace;
    [SerializeField] private GameObject imageWasd;
    [SerializeField] private GameObject imageMouse;
    [SerializeField] private GameObject controlsText;
    private TMP_Text tmp;

    [SerializeField] [Min(0f)] private float slideInAnimationDuration;

    private void Awake() {
        imageSpace.SetActive(false);
        imageWasd.SetActive(false);
        imageMouse.SetActive(false);
        
        controlsText.SetActive(false);
        tmp = controlsText.GetComponent<TMP_Text>();

        overlay.anchoredPosition = new Vector2(-2100f, overlay.anchoredPosition.y);
    }

    private void Start() {
        StartCoroutine(AnimateSlideIn());
    }

    private IEnumerator AnimateSlideIn() {
        yield return new WaitForSeconds(1f);
        
        var progress = 0f;
        var before = new Vector2(-2100f, overlay.anchoredPosition.y);
        var after = new Vector2(0f, overlay.anchoredPosition.y);

        while (progress <= slideInAnimationDuration) {
            progress += Time.deltaTime;
            overlay.anchoredPosition = Vector2.Lerp(before, 
                                                    after, 
                                                    Easing.OutQuad(progress / slideInAnimationDuration));

            yield return null;
        }
    }
}
