using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float moveSpeed;
    Vector3 mousePosition;
    Vector2 position = new Vector2(0f, 0f);
    Vector2 contourPos;

    Vector2 playerPos = new Vector2(0f, 0f);
    private Detection detection;

    float lastY = 0;
    float lastX;


    


    // Start is called before the first frame update
    void Start()
    {
        detection = (Detection)FindObjectOfType(typeof(Detection));

        playerRb = GetComponent<Rigidbody2D>();

        contourPos = new Vector2(FindObjectOfType<CountorFinder>().contourX, FindObjectOfType<CountorFinder>().contourY);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
        // Handles Player´s Movement Direction

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        
        playerPos = Vector2.Lerp(transform.position, contourPos, moveSpeed);
        

        /*

        float step = moveSpeed * Time.deltaTime;

        
         float normX = Mathf.Clamp(detection.faceX - lastX, 1, -1);
         float normY = Mathf.Clamp(detection.faceY - lastY, 1, -1);

         lastY = detection.faceY;
         lastX = detection.faceX;
         transform.position = Vector3.MoveTowards(new Vector3(transform.position.x + normX, transform.position.y +normY, transform.position.z), new Vector3(transform.position.x+ normX, transform.position.y+ normY, transform.position.z), step);
        
        */
    }

    private void FixedUpdate()
    {

       playerRb.MovePosition(contourPos);

        

    }
}
