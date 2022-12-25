using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FontSwitcher : MonoBehaviour {

    public Font Arial;
    public Font ArialItalic;
    public Font ArialBold;
    public Font TahobaBold;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    [ContextMenu("Change All System Fonts")]
    public void SwapAllTexts() {
#if UNITY_EDITOR
        Text[] alltexts = (Text[])FindObjectsOfTypeAll(typeof(Text));
        foreach (Text item in alltexts) {
            Debug.Log(item.gameObject.activeInHierarchy);
            if(item.font.name == "Arial") {
                Debug.Log(item.font.name);
                if (item.fontStyle == FontStyle.Normal) {
                    item.font = Arial;
                }
                if (item.fontStyle == FontStyle.Bold) {
                    item.font = ArialBold;
                }
                if (item.fontStyle == FontStyle.Italic) {
                    item.font = ArialItalic;
                }
                Debug.Log("System Arial is here");
            }
            if(item.font.name == "TAHOMA" && item.fontStyle == FontStyle.Bold) {
                item.font = TahobaBold;
            }
        }
#endif
    }
}
