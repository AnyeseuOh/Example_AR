using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform player;
    public float speed = 2.5f;
    public GameObject effect;
    public int hp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        transform.Translate(0, 0, speed * Time.deltaTime);
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < 0.3f)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
            speed = 0;
        }
    }

    public void DestroyMonster()
    {
        hp--;
        ScoreManager scoreManager = GetComponent<ScoreManager>();
        scoreManager.Damaged();
        if (hp < 0)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
