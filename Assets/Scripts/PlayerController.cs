using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 startPos;
    float timer = 0;
    float maxTime = 10;
    public float distanceTraveled;
    bool _hasStarted = false;

    KeyCode keyPressed;
    [SerializeField] private float _force = 10;
    private Rigidbody2D rigidBody;

    public GameObject highScoreBody;
    public Text scoreText;
    public Text highScoreText;


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
            timer += Time.deltaTime;
        }
        
        if (timer >= maxTime)
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
         timer = 0;
         distanceTraveled = 0;
        keyPressed = KeyCode.None;
        _hasStarted = false;
        highScoreBody.SetActive(false);
    }
    public void SetHighScore(float tempScore)
    {
        if (tempScore > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", tempScore);
        }
    }
    public void ShowHighscore()
    {
        distanceTraveled = Vector3.Distance(startPos, transform.position);
        SetHighScore(distanceTraveled);
        highScoreBody.SetActive(true);
        scoreText.text = distanceTraveled.ToString();
        highScoreText.text = $"Highscore {PlayerPrefs.GetFloat("HighScore")}";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (Vector3)direction * _force, 0.1f);
    }
}
