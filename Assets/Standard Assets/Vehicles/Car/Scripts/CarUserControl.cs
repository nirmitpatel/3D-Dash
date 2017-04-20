using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car, m_Car2, player1, player2; // the car controller we want to use
       




        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            m_Car2 = GetComponent<CarController>();
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
            player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<CarController>();

        }


        private void FixedUpdate()
        {
            float h = 0.0f;
            float v = 0.0f;

            float handbrake = 0.0f;

            // pass the input to the car!
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                v = CrossPlatformInputManager.GetAxis("Vertical");

                handbrake = CrossPlatformInputManager.GetAxis("Jump");

           
            }
            m_Car.Move(h, v, v, handbrake);

        }
    }
}
