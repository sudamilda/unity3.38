using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed;
    public float force;
    public float rotationSpeed;
    private Rigidbody2D body;
    private float rotationZ;
    private bool crashed;

    void Start()
    {
        crashed = false;
        body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.right * speed;
    }

    void Update()
    {
        GetPlayerInput();
        RotateBird();

        // 맵 위로 올라가면 끝
        if(!crashed && transform.position.y > 13)
        {
            crashed = true;
            LevelManager.instance.OpenEndMenu();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 장애물에 닿으면 끝
        crashed = true;
        LevelManager.instance.OpenEndMenu();
    }

    private void GetPlayerInput()
    {
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                ImpulseBird();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
        {
            ImpulseBird();
        }
    }
    private void ImpulseBird()
    {
        body.AddForce(Vector2.up * force);
    }

    private void RotateBird()
    {
        if(body.velocity.y>0)
        {
            rotationZ += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotationZ -= Time.deltaTime * rotationSpeed*0.8f;
        }
        if (Mathf.Abs(rotationZ)>30)
        {
            rotationZ = rotationZ > 0 ? 30 : -30;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}




