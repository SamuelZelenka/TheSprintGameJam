using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    Vector3 startPos;
    float timer = 0;
    float maxTime = 10;
    public float distanceTraveled;
    public bool hasStarted = false;
    string playerName;

    KeyCode keyPressed;
    [SerializeField] private float _force = 10;
    private Rigidbody2D rigidBody;

    public GameObject highScoreBody;
    public Text scoreText;
    public Text highScoreText;

    public UnityEvent OnRestart;

    public InputField inputfield;

    [SerializeField] private Vector2 addedForce = new Vector2(); 
    [SerializeField] private Vector2 direction = new Vector2(1,1);

    // Update is called once per frame
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        if (hasStarted && name != "")
        {
            timer += Time.deltaTime;


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
                }
                if (Input.GetKeyDown(KeyCode.A) && keyPressed != KeyCode.A)
                {
                    addedForce = direction * _force;
                    keyPressed = KeyCode.A;
                }
                else if (Input.GetKeyDown(KeyCode.D) && keyPressed != KeyCode.D)
                {
                    addedForce = direction * _force;
                    keyPressed = KeyCode.D;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(addedForce, ForceMode2D.Impulse);
        addedForce = Vector2.zero;
    }
    public void SetName()
    {
        playerName = inputfield.text;
    }
    public void StartGame()
    {
        hasStarted = true;
    }

    public void RestartGame()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        timer = 0;
        distanceTraveled = 0;
        keyPressed = KeyCode.None;
        hasStarted = false;
        highScoreBody.SetActive(false);
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        OnRestart.Invoke();
    }
    public void SetHighScore(float tempScore)
    {
        if (tempScore > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", tempScore);
            DiscordMessageSender.UploadMessage(PlayerPrefs.GetFloat("HighScore").ToString(), playerName);
        }
    }
    public void ShowHighscore()
    {
        rigidBody.bodyType = RigidbodyType2D.Static;
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
