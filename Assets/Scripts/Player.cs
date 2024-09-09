using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private float strength = 5f;
    private float gravity = -9.81f;
    private float tilt = 5f;
    private float limitHeight = 5f;
    
    private Vector3 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || 
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;

        if (transform.position.y > limitHeight)
        {
            transform.position = new Vector3(transform.position.x, limitHeight);
        }
    }

    private void AnimateSprite()
    {
        spriteIndex = spriteIndex >= sprites.Length -1 ? 0 : ++spriteIndex;

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacles")) {
            GameManager.Instance.GameOver();
        } else if (other.gameObject.CompareTag("ScoreLine")) {
            GameManager.Instance.IncreaseScore();
        }
    }

}
