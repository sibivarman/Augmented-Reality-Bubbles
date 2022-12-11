using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour {

    private int STARR_COUNT = 3;

    [SerializeField]
    Sprite StarEnabled;
    [SerializeField]
    Sprite StarDisabled;
    [SerializeField]
    AudioClip[] StarAudio ;

    private GameObject[] Stars;
    private int StarCount;

    public int star_no;

    // Use this for initialization
    void Start () {
        Stars = new GameObject[STARR_COUNT];
        int childIndex = 0;
		foreach(Transform child in gameObject.transform)
        {
            Stars[childIndex++] = child.gameObject;
        }
        SetStars(star_no);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetStars(int NoOfStars)
    {
        //edge value check
        if (NoOfStars < 0 || NoOfStars > 3)
            return;

        StarCount = NoOfStars;
        int index = 0;
        for(; NoOfStars > 0; NoOfStars--)
        {
            Stars[index++].GetComponent<Image>().sprite = StarEnabled;
        }
    }

    private void PlayStarAudio(int index)
    {
        if (index <= StarCount-1)
            AudioSource.PlayClipAtPoint(StarAudio[index], Vector3.zero,0.4f);
    }
}
