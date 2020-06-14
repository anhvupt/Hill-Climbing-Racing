using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class car : MonoBehaviour
{
    // Start is called before the first frame update
    public WheelJoint2D rear;
    public WheelJoint2D front;
    public float speed;
    private float movement;
    private float rot_start;
    private float rot_available;
    private float rot_end;
    private float rot_start_t;
    private float rot_available_t;
    private float rot_end_t;
    private int all_gold;
    public Text gold_text;
    public Image fuel;
    public float all_fuel = 142;
    public float fuel_less;
    public Sprite sprite;
    public Texture2D texture;
    public bool live = true;
    void Start()
    {
        rot_available = transform.rotation.eulerAngles.z;
        rot_start = rot_available;
        rot_end = rot_available;
        rot_available_t = transform.rotation.eulerAngles.z;
        rot_start_t = rot_available_t;
        rot_end_t = rot_available_t;
    }

    // Update is called once per frame
    void Update()
    {
        float v=Input.GetAxis("Horizontal");
        movement = speed * v;
        tumble();
        somersault();
        RectTransform time = fuel.GetComponent<RectTransform>();
        time.sizeDelta = new Vector2(time.sizeDelta.x - Time.deltaTime*fuel_less, time.sizeDelta.y);
    }
    void FixedUpdate()
    {
        if(movement==0)
        {
            rear.useMotor = false;
            front.useMotor = false;
        }
        else
        {
            rear.useMotor = true;
            front.useMotor = true;
            JointMotor2D motore = new JointMotor2D();
            motore.motorSpeed = movement;
            motore.maxMotorTorque = 10000;
            front.motor = motore;
            rear.motor = motore;
        }
    }
    void tumble()
    {
        rot_available = transform.rotation.eulerAngles.z;
        if (rot_end < rot_available)
        {
            rot_start = rot_available;
        }
        else if (rot_end > rot_available && rot_end - rot_available > 100)
        {
            rot_start = rot_available;
        }
        if (rot_start - rot_available > 300)
        {
            Debug.Log("tumble");
            rot_start = rot_available;
            Take_picture();
        }
        rot_end = rot_available;
    }
    void somersault()
    {
        rot_available_t = transform.rotation.eulerAngles.z;
        if (rot_end_t > rot_available_t)
        {
            rot_start_t = rot_available_t;
        }
        else if (rot_end_t < rot_available_t && rot_available_t - rot_end_t > 100)
        {
            rot_start_t = rot_available_t;
        }
        if (rot_available_t - rot_start_t > 300)
        {
            Debug.Log("somersault");
            rot_start_t = rot_available_t;
            Take_picture();
        }
        rot_end_t = rot_available_t;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "gold")
        {
            int quantity = collision.GetComponent<gold>().quantity;
            all_gold += quantity;
            GameObject.Destroy(collision.gameObject);
            gold_text.text = all_gold.ToString();
        }
        if (collision.gameObject.tag == "fuel")
        {
            RectTransform time = fuel.GetComponent<RectTransform>();
            time.sizeDelta = new Vector2(all_fuel, time.sizeDelta.y);
            GameObject.Destroy(collision.gameObject);
        }
    }
    public void Take_picture()
    {
        Texture2D text = new Texture2D(Screen.width / 2, Screen.height / 2, TextureFormat.RGB24, false);
        texture = new Texture2D(Screen.width / 2, Screen.height / 2);
        text.ReadPixels(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), 0, 0);
        text.Apply();
        texture = text;
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        live = false;
    }
}
