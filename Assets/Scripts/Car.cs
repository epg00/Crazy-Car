using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{

    [SerializeField] private float speed = 10f; 
    [SerializeField] private float speedGainPerSecond = 0.3f;  
    [SerializeField] private float turnSpeed = 200f;


    private int steerValue; 
    
    void Update()
    {
        speed += speedGainPerSecond * Time.deltaTime;

        transform.Rotate(0f,steerValue * turnSpeed * Time.deltaTime,0f);

        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Obstacle")){
            SceneManager.LoadScene(0);
        }
    }

    public void Steer(int value){
        steerValue = value;
    }
}
