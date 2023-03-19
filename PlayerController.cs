using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5;
    public TMP_Text countText;
    public TMP_Text winText;
    public int i = 0;
    

    private Rigidbody rb;
    private int count;

    private GameObject[] pickups;

    private float movementX;
    private float movementY;

// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        pickups = GameObject.FindGameObjectsWithTag("Pick Up");

    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void ResetLevel() 
    {
        winText.text = "Wins: " + i;
        Vector3 objectScale = transform.localScale;

        transform.localScale = new Vector3(objectScale.x = 1.5f, objectScale.y = 1.5f, objectScale.z = 1.5f);
        transform.position = new Vector3(0, 0, 0);
        velocidad = 5;
            foreach (GameObject pickup in pickups)
            {
                pickup.SetActive(true);
            }

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        pickups = GameObject.FindGameObjectsWithTag("Pick Up");

    }

    void LateUpdate()
    {
        if (count >= 5)
        {
            velocidad = 10;

            Vector3 objectScale = transform.localScale;

            transform.localScale = new Vector3(objectScale.x = 0.5f, objectScale.y = 0.5f, objectScale.z = 0.5f);

        }
    }

    
    


        void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement*velocidad);

        
    }


        void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        

        if (count >= 12)
        {
            i++;
            ResetLevel();
        }
        
        
    }
}

