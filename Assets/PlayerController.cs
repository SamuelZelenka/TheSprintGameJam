using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 startPos;
    float startTime = 0;
    public float distanceTraveled;
    bool _hasStarted = false;

    KeyCode keyPressed;
    [SerializeField] private float _force = 10;
    private Rigidbody2D rigidBody;

    public GameObject highScoreBody;
    public Text scoreText;


    [SerializeField] private Vector2 direction = new Vector2(1,1);

    // Update is called once per frame
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        if (_hasStarted)
        {
            startTime += Time.deltaTime;
        }
        
        if (startTime >= 10)
        {
            if (rigidBody.velocity.x < 0.01f)
            {
                ShowHighscore();
            }
        }
        else
        {
            if (keyPressed == KeyCode.None)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    keyPressed = KeyCode.A;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    keyPressed = KeyCode.D;
                }
                _hasStarted = true;
            }
            if (Input.GetKeyDown(KeyCode.A) && keyPressed != KeyCode.A)
            {
                AddForce();
                keyPressed = KeyCode.A;
            }
            else if (Input.GetKeyDown(KeyCode.D) && keyPressed != KeyCode.D)
            {
                AddForce();
                keyPressed = KeyCode.D;
            }
        }
    }

    public void AddForce()
    {
        rigidBody.AddForce(direction.normalized * _force * Time.deltaTime, ForceMode2D.Impulse);
    }
    public void RestartGame()
    {
         transform.position = startPos;
         startTime = 0;
         distanceTraveled = 0;
        _hasStarted = false;
        highScoreBody.SetActive(false);
    }
    public void ShowHighscore()
    {
        highScoreBody.SetActive(true);
        scoreText.text = Vector3.Distance(startPos, transform.position).ToString(); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (Vector3)direction * _force, 0.1f);
    }
}
