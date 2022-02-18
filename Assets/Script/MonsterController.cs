using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] bool onGround;
    [SerializeField] LayerMask engel;
    [SerializeField] float speed;
    private Rigidbody2D myBody;
    private float width;
    private static int totalMonster=0;
    // Start is called before the first frame update
    void Start()
    {
        totalMonster++;
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right)*width/2, Vector2.down,2f,engel);
        if (hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        CanavarHaraket();
    }

    private void OnDrawGizmos()
    {
        Vector3 Cizgi =  (transform.right) * width / 2;
        Gizmos.color=Color.red;
        Gizmos.DrawLine(Cizgi+ transform.position , transform.position+new Vector3(0,-2f,0) + Cizgi);
    }
    public void CanavarHaraket()
    {
        myBody.velocity = new Vector2(transform.right.x * speed, 0f);
        if (!onGround)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
    }
}
