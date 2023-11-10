using MyInputManager;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MyInputManager
{

    public class AnimationStateController : MonoBehaviour
    {
        private Animator animator;
        private int velocityHash;
        private float velocity = 0f;
        private float acceleration = 0.1f;
        private float deceleration = 0.5f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        // Start is called before the first frame update
        void Start()
        {
            velocityHash = Animator.StringToHash("Velocity");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W) && velocity < 1f)
            {
                velocity += Time.deltaTime * acceleration;
            }
            else
            {
                if(velocity > 0f) 
                {
                    velocity -= Time.deltaTime * deceleration;
                }
                else
                {
                    velocity = 0f;
                }
            }
            animator.SetFloat(velocityHash, velocity);
        }
    }
}
