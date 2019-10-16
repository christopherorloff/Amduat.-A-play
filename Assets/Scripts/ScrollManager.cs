using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollManager
{
    public class Scroll : MonoBehaviour
    {
        //Notes
        //Remember to have a variable to invert direction depending on scroll direction
        //Windows scalar
        //Try different mice

        //Variables for internal use:
        static float lastInput;
        static float lastOutput;
        static Queue<float> lastInputs = new Queue<float>();

        static float drag = 0.9f;
        static float scrollVelocity = 0;

        const float macMaxOutput = 40;
        const float winMaxOutput = 3;

        // Bool - Input or not
        public static bool isScrolling()
        {
            bool output = false;
            output = (scrollValue() != 0) ? true : false;
            return output;
        }

        // Direction - Are we receiving input, and is it up or down
        public static string scrollDirection()
        {
            string output = "None";
            if (scrollValue() != 0)
            {
                if (scrollValue() > 0)
                {
                    output = "Up";
                }
                else
                {
                    output = "Down";
                }
            }
            return output;
        }
        

        //The basis for all scroll values. Singular instances of zero value filtered out
        //Mac = 25
        //Windows = 3 or 2
        public static float scrollValue()
        {
            float d = Time.deltaTime;
            float newValue = Input.mouseScrollDelta.y * d;
            float output = 0;


            // Filtering arbitrary zeros
            if (newValue == 0)
            {
                if (lastInput == 0)
                {
                    output = 0;
                }
                else
                {
                    output = lastInput;
                }
            }
            else
            {
                output = newValue;
            }

            // Mapping for Mac output values
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
            {
                output = Mathf.Clamp(output, -macMaxOutput * d, macMaxOutput * d);
                //output /= macMaxOutput;
                output = map(output, -macMaxOutput * d, macMaxOutput * d, -1, 1);

            } 
            // Mapping for Windows output values
            else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
            {
                output = Mathf.Clamp(output, -winMaxOutput * d, winMaxOutput * d);
                //output /= winMaxOutput * d;
                output = map(output, -macMaxOutput * d, macMaxOutput * d, -1, 1);
            }

            
            lastInput = newValue;
            lastOutput = output;
            return output;
        }

        // Scroll value that takes the previous n number of values into account
        public static float scrollValueMean(int n)
        {
            float output = 0;
            lastInputs.Enqueue(scrollValue());

            if (lastInputs.Count > n)
            {
                lastInputs.Dequeue();
            }
            output = GetMeanOfQueue(n, lastInputs);
            return output;
        }

        public static float scrollValueAccelerated()
        {
            float output = 0;
            float input = scrollValue();

            if (Mathf.Abs(input) > Mathf.Abs(scrollVelocity))
            {
                scrollVelocity = input;
            } else
            {
                scrollVelocity *= drag;
            } 
            
            if(Mathf.Abs(scrollVelocity) < 0.0001f)
            {
                scrollVelocity = 0;
            }
            

            output = scrollVelocity;
            return output;
        }


        // ----------------------------- Helper functions ----------------------------- \\
        private static float GetMeanOfQueue(int n, Queue<float> queue)
        {
            float mean = 0;

            foreach (var item in queue)
            {
                mean += item;
            }
            mean /= n;
            return mean;
        }

        public static float map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
        {
            return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
        }
    }
}
