using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBubble : MonoBehaviour {

    [SerializeField]
    AudioClip audioClip;

    [SerializeField]
    Material[] bubbleMaterial;
    [SerializeField]
    Material[] particleMaterial;

    ParticleSystem particles;
    public enum COLOR { RED,BLUE,GREEN,YELLOW,SPARK};
    public COLOR color;
    private Color bubbleColor;
    private MeshRenderer meshRenderer;
    private ParticleSystem.MinMaxGradient particleColor;
    private AudioSource audioSource;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        particleColor = GetComponent<ParticleSystem>().main.startColor;
        meshRenderer = GetComponent<MeshRenderer>();
        particles = GetComponent<ParticleSystem>();
        bubbleColor = meshRenderer.material.color;
        if(bubbleColor.r == 1.0f && bubbleColor.g == 1.0f)
        {
            color = COLOR.YELLOW;
        }
        else if(bubbleColor.b == 1.0f)
        {
            color = COLOR.BLUE;
        }
        else if(bubbleColor.r == 1.0f)
        {
            color = COLOR.RED;
        }
        else if(bubbleColor.g == 1.0f)
        {
            color = COLOR.GREEN;
        }
	}

    public COLOR GetColor()
    {
        return color;
    }

    public void SetColor(COLOR thisColor)
    {
        switch (thisColor)
        {
            case COLOR.BLUE:
                {
                    meshRenderer.material = bubbleMaterial[0];
                    color = COLOR.BLUE;
                    GetComponent<ParticleSystemRenderer>().material = particleMaterial[0];
                }
                break;
            case COLOR.RED:
                {
                    meshRenderer.material = bubbleMaterial[2];
                    color = COLOR.RED;
                    GetComponent<ParticleSystemRenderer>().material = particleMaterial[2];
                }
                break;
            case COLOR.GREEN:
                {
                    meshRenderer.material = bubbleMaterial[1];
                    color = COLOR.GREEN;
                    GetComponent<ParticleSystemRenderer>().material = particleMaterial[1];
                }
                break;
            case COLOR.YELLOW:
                {
                    meshRenderer.material = bubbleMaterial[3];
                    color = COLOR.YELLOW;
                    GetComponent<ParticleSystemRenderer>().material = particleMaterial[3];
                }
                break;
            case COLOR.SPARK:
                {
                    GetComponent<ParticleSystemRenderer>().material = particleMaterial[4];
                }
                break;
        }

    }

    public void DestroySphere()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        meshRenderer.enabled = false;
        particles.Play();
        AudioSource.PlayClipAtPoint(audioClip, transform.position,0.15f);
        Destroy(transform.parent.gameObject, audioClip.length);
       
    }
}
