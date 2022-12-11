using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    [SerializeField]
    float scrollSpead;
    private Vector2 initialTextureOffset;

    void onStart()
    {
        initialTextureOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
    }

    public void onScroll(Vector2 position)
    {
       /* float y = Mathf.Repeat(Time.time * scrollSpead,1);
        if(position.y == 0)
        {
            y = -y;
        }
        Vector2 textureOffset = new Vector2(0, y);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex",textureOffset);*/
    }

    private void OnDisable()
    {
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", initialTextureOffset);
    }

    Vector2 scrollPosition = new Vector2(0,15000);
    Touch touch;
    // The string to display inside the scrollview. 2 buttons below add & clear this string.
    string longString = "This is a long-ish string";

    /*void OnGUI()
    {

        scrollPosition = GUI.BeginScrollView(new Rect(0, 0, Screen.width, Screen.height), scrollPosition, new Rect(0, 0, Screen.width, 15000), GUIStyle.none, GUIStyle.none);

        for (int i = 0,j = 500; i < 500; i++,j--)
        {
            GUI.Box(new Rect(0, i * 30, Screen.width, 30), "xxxx_" + j);
        }
        GUI.EndScrollView();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDelta = new Vector2(0, Input.GetTouch(0).deltaPosition.y);
            //transform.Translate(0, touchDelta.y, 0);
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", touchDelta);
        }
    }*/
}
