using UnityEngine;
using System.Collections;

public class MenuSelector : MonoBehaviour {

    public UIPanel[] panels;

    public void MakePanelActive(UIPanel panel)
    {
        if (panels != null)
        {
            foreach (UIPanel p in panels)
            {
                if (p != null && p != panel)
                {
                    p.gameObject.SetActiveRecursively(false);
                }
            }
        }

        panel.gameObject.SetActiveRecursively(true);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
