using System.Collections.Generic;
using UnityEngine;

public class FireBubbleHolder : MonoBehaviour {

    private float MAX_RAY_HIT_DISTANCE = 1.0f ;

    private HashSet<DestroyBubble> collidedBubbles;
    private enum DIRECTIONS  {FRONT,BACK,LEFT,RIGHT,UP,DOWN};
    private RaycastHit hit;
    private DestroyBubble temp;
    private DestroyBubble firstHitBubble;
    private DestroyBubble.COLOR rainbowBubbleColor;
    private DestroyBubble desBubble;
    private bool isColliding = false;
    public bool isFired = false;
    private bool isBubbleBursted = false;

    public Vector3 forwardPos;

    private enum BUBBLE_TYPE { BUBBLE,BOMB,RAINBOW};

    int count = 1;

    private Vector3 direction;
    private Sounds BubbleAudios;
    private Sounds Speaker;

    private void Start()
    {
        MAX_RAY_HIT_DISTANCE *= transform.localScale.x;
        collidedBubbles = new HashSet<DestroyBubble>();
        desBubble = GetComponentInChildren<DestroyBubble>();
        BubbleAudios = GameObject.FindObjectOfType<Sounds>();
        //Speaker = BubbleAudios.GetComponent<Sounds>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //To prevent multiple call on tirgger enter, this may be caused for circular objects due to multiple contact points
        if (isColliding)
            return;
        //isColliding = true;

        //Play bubble hit sound on collision
        AudioSource.PlayClipAtPoint(BubbleAudios.SoundClips[2], transform.position, 0.4f);

        collidedBubbles.Clear();

        direction = GetComponentInChildren<BubbleLauncher>().direction;
        //stop moving bubble by translate to prevent passing through the bubble set
        //GetComponentInChildren<BubbleLauncher>().enabled = false;

        //set force in child rigidbody to create realistic collision
        //GetComponentsInChildren<Rigidbody>()[1].velocity = direction * 2.5f;

        if (other.GetComponent<BombBubble>())
        {
            BurstBomb(other.gameObject);
        }
        if (GetComponent<RainbowBubble>())
        {
            if (GetComponent<RainbowBubble>().isAlreadyCalled)
                return;
            GetComponent<RainbowBubble>().isAlreadyCalled = true;
            ChangeBubbleColor(other.gameObject);
        }
        else if (desBubble.GetColor().Equals(other.GetComponentInChildren<DestroyBubble>().GetColor()))
        {
            BurstBubbles(other.gameObject);
        }
        else {
            ConvertToOrdinaryBubble();
        }

    }

    private void ConvertToOrdinaryBubble()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SphereHolder>().isAlive = true;
        //GetComponentsInChildren<Rigidbody>()[1].mass = GetComponent<Rigidbody>().mass;

        Destroy(this);
    }

    private void BurstBubbles(GameObject other)
    {
        collidedBubbles.Add(desBubble);
        firstHitBubble = desBubble.GetComponentInChildren<DestroyBubble>();
        if (firstHitBubble)
        {
            FindSimilarColorBubbles(firstHitBubble);
        }
        if (collidedBubbles.Count > 2)
        {
            //Speaker.PlayBubbleBurst();
            foreach (DestroyBubble bubble in collidedBubbles)
            {
                bubble.DestroySphere();
            }
        }
        else
        {
            ConvertToOrdinaryBubble();
        }
    }

    private void FindSimilarColorBubbles(DestroyBubble bubbleObject)
    {
        Collider[] bubbles = Physics.OverlapBox(bubbleObject.transform.position, Vector3.one * 0.05f);
        foreach (Collider bubble in bubbles)
        {
            if (bubble.GetComponent<DestroyBubble>() && bubble.GetComponent<DestroyBubble>().GetColor().Equals(bubbleObject.GetColor()) && !collidedBubbles.Contains(bubble.GetComponent<DestroyBubble>()))
            {
                collidedBubbles.Add(bubble.GetComponent<DestroyBubble>());
                FindSimilarColorBubbles(bubble.GetComponent<DestroyBubble>());
            }
        }
    }

    private void BurstBomb(GameObject other)
    {
        DestroyBubble destroyTemp = other.GetComponentInChildren<DestroyBubble>();
        destroyTemp.DestroySphere();
        desBubble.DestroySphere();
        temp = getBubbleAt(DIRECTIONS.UP, destroyTemp);
        DestroyBubble(temp);
        temp = getBubbleAt(DIRECTIONS.DOWN, destroyTemp);
        DestroyBubble(temp);
        temp = getBubbleAt(DIRECTIONS.RIGHT, destroyTemp);
        DestroyBubble(temp);
        temp = getBubbleAt(DIRECTIONS.LEFT, destroyTemp);
        DestroyBubble(temp);
    }

    private void DestroyBubble(DestroyBubble destroyBubble)
    {
        if (temp)
        {
            temp.SetColor(global::DestroyBubble.COLOR.SPARK);
            temp.DestroySphere();
        }
    }

    private void ChangeBubbleColor(GameObject other)
    {
        rainbowBubbleColor = desBubble.GetColor();
        desBubble.DestroySphere();
        HashSet<DestroyBubble> colorChangeBubbleSet = new HashSet<DestroyBubble>();
        if (other.GetComponent<DestroyBubble>())
        {
            FindNeighbourBubblesForRainbowBubble(colorChangeBubbleSet, other.GetComponent<DestroyBubble>());
        }
        foreach(DestroyBubble destroyBubble in colorChangeBubbleSet)
        {
            Debug.Log("inside color change: "+destroyBubble.GetColor()+" rainbow bubble color:"+desBubble.GetColor());
            destroyBubble.SetColor(rainbowBubbleColor);
        }
    }

    private void FindNeighbourBubblesForRainbowBubble(HashSet<DestroyBubble> colorChagngeBubble,DestroyBubble bubbleObject)
    {
        DestroyBubble rightUp;
        DestroyBubble leftUp;

        colorChagngeBubble.Add(bubbleObject);
        temp = getBubbleAt(DIRECTIONS.UP, bubbleObject);
        if (temp)
        {
            Debug.Log(temp.GetColor());
            AddBubblesToSet(colorChagngeBubble, temp);
        }
        temp = getBubbleAt(DIRECTIONS.DOWN, bubbleObject);
        if (temp)
        {
            Debug.Log(temp.GetColor());
            AddBubblesToSet(colorChagngeBubble, temp);
        }
        rightUp = getBubbleAt(DIRECTIONS.RIGHT, bubbleObject);
        if (rightUp)
        {
            AddBubblesToSet(colorChagngeBubble, rightUp);
            temp = getBubbleAt(DIRECTIONS.UP, rightUp);
            if (temp)
            {
                AddBubblesToSet(colorChagngeBubble, temp);
            }
            temp = getBubbleAt(DIRECTIONS.DOWN, rightUp);
            if (temp)
            {
                AddBubblesToSet(colorChagngeBubble, temp);
            }
        }
        
        leftUp = getBubbleAt(DIRECTIONS.LEFT, bubbleObject);
        if (leftUp)
        {
            AddBubblesToSet(colorChagngeBubble, leftUp);
            temp = getBubbleAt(DIRECTIONS.UP, leftUp);
            if (temp)
            {
                AddBubblesToSet(colorChagngeBubble, temp);
            }
            temp = getBubbleAt(DIRECTIONS.DOWN, leftUp);
            if (temp)
            {
                AddBubblesToSet(colorChagngeBubble, temp);
            }
        }
        

    }

    private void AddBubblesToSet( HashSet<DestroyBubble> colorChagngeBubble,DestroyBubble temp)
    {
        if (temp.GetColor().
            Equals(desBubble.GetColor()))
        {
            //temp.DestroySphere();
        }
        else
        {
            Debug.Log("color change");
            colorChagngeBubble.Add(temp);
        }
    }

    /*private void FindSimilarColorBubbles(DestroyBubble bubbleObject)
    {
        foreach(DIRECTIONS dir in System.Enum.GetValues(typeof(DIRECTIONS)))
        {
            temp = getBubbleAt(dir, bubbleObject);
            CollectBubbles(bubbleObject);
        }
    }*/

   

    private void CollectBubbles(DestroyBubble bubbleObject)
    {
        if (temp && 
            !collidedBubbles.Contains(temp) && temp.GetColor().Equals(bubbleObject.GetColor()))
        {
            collidedBubbles.Add(temp);
            FindSimilarColorBubbles(temp);
        }
    }

    private DestroyBubble getBubbleAt(DIRECTIONS dir,DestroyBubble bubble)
    {
        switch (dir)
        {
            case DIRECTIONS.FRONT:
                {
                    Physics.Raycast(bubble.transform.position, Vector3.forward, out hit, MAX_RAY_HIT_DISTANCE);
                    if(hit.collider)
                        return hit.collider.GetComponentInChildren<DestroyBubble>();
                }break;
            case DIRECTIONS.BACK:
                {
                    Physics.Raycast(bubble.transform.position, Vector3.back, out hit, MAX_RAY_HIT_DISTANCE);
                    if (hit.collider)
                        return hit.collider.GetComponentInChildren<DestroyBubble>();
                }
                break;
            case DIRECTIONS.RIGHT:
                {
                    Physics.Raycast(bubble.transform.position, Vector3.right, out hit, MAX_RAY_HIT_DISTANCE);
                    if (hit.collider)
                        return hit.collider.GetComponentInChildren<DestroyBubble>();
                }
                break;
            case DIRECTIONS.LEFT:
                {
                    Physics.Raycast(bubble.transform.position, Vector3.left, out hit, MAX_RAY_HIT_DISTANCE);
                    if (hit.collider)
                        return hit.collider.GetComponentInChildren<DestroyBubble>();
                }
                break;
            case DIRECTIONS.UP:
                {
                    Physics.Raycast(bubble.transform.position, Vector3.up, out hit, MAX_RAY_HIT_DISTANCE);
                    if (hit.collider)
                        return hit.collider.GetComponentInChildren<DestroyBubble>();
                }
                break;
            case DIRECTIONS.DOWN:
                {
                    Physics.Raycast(bubble.transform.position, Vector3.down, out hit, MAX_RAY_HIT_DISTANCE);
                    if (hit.collider)
                        return hit.collider.GetComponentInChildren<DestroyBubble>();
                }
                break;
        }
        return null;
    }
}
