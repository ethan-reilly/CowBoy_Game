using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField]
    public float speed = 10f;
    void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xMov + transform.forward * zMov;
        controller.Move(move * speed * Time.deltaTime);
    }
}
