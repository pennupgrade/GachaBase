using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelContentLoader : MonoBehaviour
{
    [System.Serializable]
    public struct PanelContent
    {
        public string Text;
        public Sprite Image;
    }

    public PanelContent[] PanelContentArray;
    public GameObject ContentPanelPrefab;
    public ScrollRect ScrollRectParent;

    void LoadContentIntoPanel()
    {
        for (int i = 0; i < PanelContentArray.Length; i++)
        {
            PanelContent PanelContent = PanelContentArray[i];
            GameObject ContentBox = Instantiate(ContentPanelPrefab, transform);
            GameObject IconImage = ContentBox.transform.Find("IconImage").gameObject;
            GameObject TextBox = ContentBox.transform.Find("TextBox").gameObject;

            IconImage.SetActive(PanelContent.Image != null); // Disable image box if none provided

            IconImage.GetComponent<Image>().sprite = PanelContent.Image;
            TextBox.GetComponent<TMPro.TextMeshProUGUI>().text = PanelContent.Text;
        }
    }

    private void Awake()
    {
        LoadContentIntoPanel();
    }
}
