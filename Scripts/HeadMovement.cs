using UnityEngine;
using System.Collections;

public class HeadMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    GameObject foodPrefab;

    Transform lastNode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        lastNode = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f)
        {
            transform.Rotate (0f, 0f, -180f * Time.deltaTime * Input.GetAxis("Horizontal"));
            rb.velocity = transform.up * speed;
        }   
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Food"))
        {
            if (lastNode == transform)
                col.gameObject.tag = "Head";
            else
                StartCoroutine(SetTagWithDelay(col.transform));
            col.transform.GetComponent<Node>().Init(lastNode);
            lastNode = col.transform;
            GenerateFood();
        }

        if (col.gameObject.CompareTag("Border") || col.gameObject.CompareTag("Tale"))
        {
            Debug.Log ("Crashed on " + col.gameObject.tag );
            Time.timeScale = 0.0f;
        }
    }

    IEnumerator SetTagWithDelay(Transform tr)
    {
        yield return new WaitForSeconds(0.5f);
        tr.gameObject.tag = "Tale";
        yield return null;
    }

    void GenerateFood()
    {
        Vector3 randomPosition = new Vector3 ( Random.Range (-3.5f, 3.5f), Random.Range(-1.8f, 1.8f), 0f);
        Instantiate(foodPrefab, randomPosition, Quaternion.identity);
    }
}
