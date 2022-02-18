using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    [SerializeField] Text Score;
    [SerializeField]  float coinRotate;

    
    private void Update()
    {
        transform.Rotate(new Vector3(0f,coinRotate));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject.Find("LevelManager").GetComponent<LevelManager>().ScoreAdds(50);
        }
    }
}
