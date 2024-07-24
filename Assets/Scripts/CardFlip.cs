using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardFlip : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;
    public float flipDuration = 1.0f;
    public float hoverDelay = 0.5f; // Delay before starting the flip animation
    public float unhoverDelay = 0.5f; // Delay before starting the unflip animation

    private bool isMouseOver = false;
    private bool isFlipped = false;
    private Coroutine hoverCoroutine;
    private Coroutine unhoverCoroutine;

    private void Start()
    {
        // Ensure the front image is active and the back image is inactive at the start
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        if (unhoverCoroutine != null)
        {
            StopCoroutine(unhoverCoroutine);
        }
        hoverCoroutine = StartCoroutine(WaitAndFlipCard(frontImage, backImage, hoverDelay));
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }
        unhoverCoroutine = StartCoroutine(WaitAndFlipCard(backImage, frontImage, unhoverDelay));
    }

    private IEnumerator WaitAndFlipCard(Image fromImage, Image toImage, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Only flip the card if the mouse is still over it (for enter) or not over it (for exit)
        if ((isMouseOver && fromImage == frontImage) || (!isMouseOver && fromImage == backImage && isFlipped))
        {
            StartCoroutine(FlipCard(fromImage, toImage));
        }
    }

    private IEnumerator FlipCard(Image fromImage, Image toImage)
    {
        float elapsedTime = 0f;
        Vector3 fromScale = fromImage.transform.localScale;
        Vector3 toScale = new Vector3(0, fromImage.transform.localScale.y, fromImage.transform.localScale.z);

        // First half of the flip
        while (elapsedTime < flipDuration / 2)
        {
            fromImage.transform.localScale = Vector3.Lerp(fromScale, toScale, (elapsedTime / (flipDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fromImage.transform.localScale = toScale;
        fromImage.gameObject.SetActive(false);
        toImage.gameObject.SetActive(true);

        elapsedTime = 0f;
        fromScale = toImage.transform.localScale;
        toScale = new Vector3(0.49f, toImage.transform.localScale.y, toImage.transform.localScale.z);

        // Second half of the flip
        while (elapsedTime < flipDuration / 2)
        {
            toImage.transform.localScale = Vector3.Lerp(new Vector3(0, toImage.transform.localScale.y, toImage.transform.localScale.z), toScale, (elapsedTime / (flipDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        toImage.transform.localScale = toScale;

        if (fromImage == frontImage)
            isFlipped = true;
        else
            isFlipped = false;
    }
}
