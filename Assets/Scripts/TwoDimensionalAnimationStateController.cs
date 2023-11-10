using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    private Animator animator;
    float velocityX = 0f;
    float velocityZ = 0f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2f;

    int velocityXHash;
    int velocityZHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocityXHash = Animator.StringToHash("VelocityX");
        velocityZHash = Animator.StringToHash("VelocityZ");
    }

    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, float currentMaxVelocity)
    {
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!forwardPressed && velocityZ > 0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && velocityZ < 0f)
        {
            velocityZ = 0f;
        }
        if (!leftPressed && velocityX < 0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        if (!leftPressed && !rightPressed && velocityX != 0f && velocityX < 0.05f && velocityX > -0.05f)
        {
            velocityX = 0f;
        }

        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxVelocity && velocityX > -(currentMaxVelocity + 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < -(currentMaxVelocity - 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.RightShift);

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        changeVelocity(forwardPressed, leftPressed, rightPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        animator.SetFloat(velocityXHash, velocityX);
        animator.SetFloat(velocityZHash, velocityZ);
    }
}
