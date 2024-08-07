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
    
    [SerializeField] private GameObject overlay;
    private RectTransform overlayRect;
    private Image overlayImage;
    
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

    private float duration;
    private bool showInput;
    private bool shownPopup;
    
    private void Awake() {
        space = imageSpace.GetComponent<Image>();
        wasd = imageWasd.GetComponent<Image>();
        mouse = imageMouse.GetComponent<Image>();
        
        imageSpace.SetActive(false);
        imageWasd.SetActive(false);
        imageMouse.SetActive(false);
        
        tmp = controlsText.GetComponent<TMP_Text>();
        tmp.color = Color.clear;

        overlayRect = overlay.GetComponent<RectTransform>();
        overlayImage = overlay.GetComponent<Image>();
        overlayRect.anchoredPosition = new Vector2(-2100f, overlayRect.anchoredPosition.y);
        
        space.color = Color.clear;
        wasd.color = Color.clear;
        mouse.color = Color.clear;

        duration = 0f;
        showInput = true;
        shownPopup = false;
    }

    private void Update() {
        duration += Time.deltaTime;

        if (duration <= 3f) {
            if (CheckControlInput()) {
                showInput = false;
            }
        }
        else {
            if (showInput) {
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
                showInput = false;
                shownPopup = true;
            }
        }

        if (shownPopup && CheckControlInput()) {
            StartCoroutine(AnimateFadeOut());
            shownPopup = false;
        }
    }

    private bool CheckControlInput() {
        return controlType switch {
            ControlType.Space => Input.GetKeyDown(KeyCode.Space),
            
            ControlType.Wasd => Input.GetKeyDown(KeyCode.W) || 
                                Input.GetKeyDown(KeyCode.A) ||
                                Input.GetKeyDown(KeyCode.S) || 
                                Input.GetKeyDown(KeyCode.D),
            
            ControlType.Mouse => Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1),
            
            _ => throw new ArgumentOutOfRangeException()
        };
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

    private IEnumerator AnimateFadeOut() {
        var progress = 0f;
        const float CURR_DURATION = 0.3f;

        var beginOverlayColor = overlayImage.color;
        
        while (progress <= CURR_DURATION) {
            progress += Time.deltaTime;
            var t = progress / CURR_DURATION;

            overlayImage.color = Color.Lerp(beginOverlayColor, Color.clear, t);
            space.color = Color.Lerp(Color.white, Color.clear, t);
            wasd.color = Color.Lerp(Color.white, Color.clear, t);
            mouse.color = Color.Lerp(Color.white, Color.clear, t);
            tmp.color = Color.Lerp(Color.white, Color.clear, t);
            
            yield return null;
        }
        
        overlay.SetActive(false);
    }

    private IEnumerator AnimateSlideIn() {
        var before = new Vector2(-2100f, overlayRect.anchoredPosition.y);
        var after = new Vector2(0f, overlayRect.anchoredPosition.y);

        var progress = 0f;
        while (progress <= slideInAnimationDuration) {
            progress += Time.deltaTime;
            overlayRect.anchoredPosition = Vector2.Lerp(before, 
                                                        after, 
                                                        Easing.OutQuad(progress / slideInAnimationDuration));

            yield return null;
        }
    }
}
