using UnityEngine;
using System.Collections;

public class OpenSubMenu : MonoBehaviour {

    public MenuSelector selector;

    public UIPanel Target;

    public void OnClick()
    {
        selector.MakePanelActive(Target);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
