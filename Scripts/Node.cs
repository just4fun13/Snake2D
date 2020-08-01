using UnityEngine;

public class Node : MonoBehaviour
{
    Transform parentNode;
    Vector3 destinationPoint;
    bool inited;

    public void Init(Transform tr)
    {
        parentNode = tr;
        destinationPoint = tr.position;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
        inited = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inited)
        {
            transform.position = transform.position + (destinationPoint - transform.position) * 5f * Time.fixedDeltaTime ;
            if ( (transform.position - destinationPoint).sqrMagnitude < 0.1f)
                destinationPoint = parentNode.position;
        }

    }
}
