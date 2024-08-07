using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

internal enum ControlType
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
    private Image space;
    
    [SerializeField] private GameObject imageWasd;
    private Image wasd;
    
    [SerializeField] private GameObject imageMouse;
    private Image mouse;
    
    [SerializeField] private GameObject controlsText;
    private TMP_Text tmp;

    [SerializeField] [Min(0f)] private float slideInAnimationDuration;
    [SerializeField] [Min(0f)] private float opacityAnimationDuration;

    private void Awake() {
        space = imageSpace.GetComponent<Image>();
        wasd = imageWasd.GetComponent<Image>();
        mouse = imageMouse.GetComponent<Image>();
        
        imageSpace.SetActive(false);
        imageWasd.SetActive(false);
        imageMouse.SetActive(false);
        
        tmp = controlsText.GetComponent<TMP_Text>();
        tmp.color = Color.clear;

        overlay.anchoredPosition = new Vector2(-2100f, overlay.anchoredPosition.y);
        space.color = Color.clear;
        wasd.color = Color.clear;
        mouse.color = Color.clear;
    }

    private void Start() {
        switch (controlType) {
            case ControlType.Space:
                imageSpace.SetActive(true);
                break;
            
            case ControlType.Wasd:
                imageWasd.SetActive(true);
                break;
            
            case ControlType.Mouse:
                imageMouse.SetActive(true);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        StartCoroutine(AnimateOpacity());
    }

    private IEnumerator AnimateOpacity() {
        yield return StartCoroutine(AnimateSlideIn());

        var image = controlType switch {
            ControlType.Wasd => wasd,
            ControlType.Mouse => mouse,
            _ => space
        };
        
        var text = controlType switch {
            ControlType.Space => "spacebar",
            ControlType.Wasd => "WASD keys",
            ControlType.Mouse => "mouse",
            _ => throw new ArgumentOutOfRangeException()
        };
        tmp.text = $"Use the {text}\nto play this game!";

        var progress = 0f;
        while (progress <= opacityAnimationDuration) {
            progress += Time.deltaTime;
            
            var current = Color.Lerp(Color.clear, Color.white, progress / opacityAnimationDuration);
            image.color = current;
            tmp.color = current;

            yield return null;
        }
    }

    private IEnumerator AnimateSlideIn() {
        var before = new Vector2(-2100f, overlay.anchoredPosition.y);
        var after = new Vector2(0f, overlay.anchoredPosition.y);

        var progress = 0f;
        while (progress <= slideInAnimationDuration) {
            progress += Time.deltaTime;
            overlay.anchoredPosition = Vector2.Lerp(before, 
                                                    after, 
                                                    Easing.OutQuad(progress / slideInAnimationDuration));

            yield return null;
        }
    }
}
