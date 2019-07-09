using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5;
    private float objectiveCount = 0;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

      float moveHorizontal = Input.GetAxis ("Horizontal");
      float moveVertical = Input.GetAxis ("Vertical");

      Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

      rb.AddForce(movement * speed);

    }

    void OnTriggerEnter (Collider other)
    {
      if (other.gameObject.CompareTag ("target"))
      {
        other.gameObject.SetActive (false);
        objectiveCount+= 1;
        Debug.Log(objectiveCount);
        if (objectiveCount == 4)
        {
          SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
      }
    }


    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape)) {
          Application.Quit();
      }
    }
}
