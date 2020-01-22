using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    public float speed;
    bool canPlayerMove = true;

    void FixedUpdate()
    {
        float moveHorizontal = -Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (canPlayerMove)
        {
            Vector3 movement = new Vector3(moveVertical, 0.0f, moveHorizontal);
            if (movement != Vector3.zero) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
                transform.Translate(movement * speed * Time.deltaTime, Space.World);
            }
        }
    }
}
