using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airTime : MonoBehaviour
{
    float count = 0;
    float combo = 0;
    public bool on_air = true;
    public GameObject car;
    bool cap_piture = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(count > 1f)
        {
            combo++;
            count = 0;
            Debug.Log("Air: " + combo);
        }
        count += Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "wheel")
        {
            combo = 0;
            count = 0;
            on_air = false;
        }
        if (collision.collider.tag == "head")
        {
            car.GetComponent<car>().Take_picture();
            cap_piture = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "wheel")
        {
            combo = 0;
            count = 0;
            on_air = false;
        }
    }
}
