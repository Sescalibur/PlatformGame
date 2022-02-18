using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowControl : MonoBehaviour
{
    [SerializeField] GameObject Effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            if (collision.gameObject.CompareTag("Monster"))
            {
                Instantiate(Effect, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                GameObject.Find("LevelManager").GetComponent<LevelManager>().ScoreAdds(100);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
