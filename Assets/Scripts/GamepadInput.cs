using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInput : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad =  Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return;
        }

        if (gamepad.aButton.wasPressedThisFrame)
        {
            Debug.Log("A button pressed!");
        }
        if (gamepad.bButton.wasPressedThisFrame)
        {
            Debug.Log("B button pressed!");
        }
        if (gamepad.xButton.wasPressedThisFrame)
        {
            Debug.Log("X button pressed!");
        }
        if (gamepad.yButton.wasPressedThisFrame)
        {
            Debug.Log("Y button pressed!");
        }
        if (gamepad.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("North button pressed!");
        }
        if (gamepad.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("South button pressed!");
        }
        if (gamepad.buttonEast.wasPressedThisFrame)
        {
            Debug.Log("East button pressed!");
        }
        if (gamepad.buttonWest.wasPressedThisFrame)
        {
            Debug.Log("West button pressed!");
        }
        if (gamepad.startButton.wasPressedThisFrame)
        {
            Debug.Log("Start button pressed!");
        }
        if (gamepad.circleButton.wasPressedThisFrame)
        {
            Debug.Log("Circle button pressed!");
        }
        if (gamepad.crossButton.wasPressedThisFrame)
        {
            Debug.Log("Cross button pressed!");
        }
        if (gamepad.squareButton.wasPressedThisFrame)
        {
            Debug.Log("Square button pressed!");
        }
        if (gamepad.triangleButton.wasPressedThisFrame)
        {
            Debug.Log("Triangle button pressed!");
        }
        if (gamepad.selectButton.wasPressedThisFrame)
        {
            Debug.Log("Select button pressed!");
        }

        //dpad vector (also can do up,down,left,right as buttons)
        Vector2 dpad = gamepad.dpad.value;
        if (dpad.magnitude > 0)
            Debug.Log("Dpad Input " + dpad);

        //left stick
        Vector2 stickInputL = gamepad.leftStick.ReadValue();
        if (stickInputL.magnitude > 0)
            Debug.Log("Left Stick Input: " + stickInputL);

        if (gamepad.leftStickButton.wasPressedThisFrame)
        {
            Debug.Log("Left stick button pressed!");
        }

        //right stick
        Vector2 stickInputR = gamepad.rightStick.ReadValue();
        if(stickInputR.magnitude > 0)
            Debug.Log("Right Stick Input: " + stickInputR);
        
        if (gamepad.rightStickButton.wasPressedThisFrame)
        {
            Debug.Log("right stick button pressed!");
        }


        //right trigger is held down
        if (gamepad.rightTrigger.isPressed)
        {
            Debug.Log("Right Trigger held down.");
        }

        //right trigger is held down
        if (gamepad.leftTrigger.isPressed)
        {
            Debug.Log("Left Trigger held down.");
        }

        //Left Shoulder is held down
        if (gamepad.leftShoulder.isPressed)
        {
            Debug.Log("Left Shoulder held down.");
        }

        //Right Shoulder is held down
        if (gamepad.rightShoulder.isPressed)
        {
            Debug.Log("Right Shoulder held down.");
        }
            
    }
}
