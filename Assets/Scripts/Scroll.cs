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

        static Queue<float> lastInputs = new Queue<float>();

        //Velocity output related
        static float drag = 0.9f;
        static float scrollVelocity = 0;

        //Platform related variables
        const float macMaxOutput = 25;
        const float winMaxOutput = 1;
        const float webGLScalar = 0.75f;

        //OnNewScroll
        static float maxVelocityCap = 0.05f;
        static bool maxVelocityCapReached = false;

        //Flags
        static bool deltaTimeInput = true;
        static bool inputLocked = false;

        //Events
        public delegate void OnNewScrollEvent();
        public static event OnNewScrollEvent OnScrollEnter;
        public static event OnNewScrollEvent OnScrollExit;

        //Event related
        void Update()
        {
            //Only process if any subscribers to either events
            if (OnScrollEnter != null || OnScrollExit != null)
            {
                float input = Mathf.Abs(Scroll.scrollValue());
                float scrollVelocity = Mathf.Clamp(input, 0, maxVelocityCap);

                if (scrollVelocity >= maxVelocityCap && !maxVelocityCapReached)
                {
                    OnScrollEnter?.Invoke();
                    maxVelocityCapReached = true;
                }
                else if (scrollVelocity < maxVelocityCap && maxVelocityCapReached)
                {
                    OnScrollExit?.Invoke();
                    maxVelocityCapReached = false;
                }
            }
        }

        // ------------------------------------------------------------------- \\
        // ------------------------ public functions ------------------------- \\
        // ------------------------------------------------------------------- \\

        public static void LockInput()
        {
            inputLocked = true;
            print("Input locked");
        }

        public static void UnlockInput()
        {
            inputLocked = false;
            print("Input unlocked");
        }

        public static bool InputLockState()
        {
            return inputLocked;
        }

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
            if (scrollValueMean(5) != 0)
            {
                if (scrollValueMean(5) > 0)
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

        private static bool onScrollEnter()
        {
            bool output = false;
            float input = Mathf.Abs(Scroll.scrollValueMean(10));
            float velocityX = Mathf.Clamp(input, 0, maxVelocityCap);

            if (velocityX >= maxVelocityCap && !maxVelocityCapReached)
            {
                output = true;
                maxVelocityCapReached = true;
            }
            else if (velocityX < maxVelocityCap && maxVelocityCapReached)
            {
                maxVelocityCapReached = false;
            }

            return output;
        }




        //The basis for all scroll values
        //Mac = 25
        //Windows = 3 or 2
        public static float scrollValue()
        {
            float newValue = Input.mouseScrollDelta.y;
            float output = newValue;

            output = arbitraryZeroFiltering(newValue);
            output = platformDependentConversion(output);
            //output = webGLInputScalar(output);

            lastInput = newValue;

            if (inputLocked)
            {
                return 0;
            }
            else
            {
                return output;
            }
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
            }
            else
            {
                if (Time.frameCount % Mathf.RoundToInt(2000 * Time.deltaTime) == 0)
                {
                    scrollVelocity *= drag;
                }
            }

            if (Mathf.Abs(scrollVelocity) < 0.0001f)
            {
                scrollVelocity = 0;
            }

            output = scrollVelocity;
            return output;
        }

        public static float scrollValueAccelerated(float customDrag)
        {
            float output = 0;
            float input = scrollValue();

            if (Mathf.Abs(input) > Mathf.Abs(scrollVelocity))
            {
                scrollVelocity = input;
            }
            else
            {
                if (Time.frameCount % Mathf.RoundToInt(2000 * Time.deltaTime) == 0)
                {
                    scrollVelocity *= customDrag;
                }
            }

            if (Mathf.Abs(scrollVelocity) < 0.0001f)
            {
                scrollVelocity = 0;
            }


            output = scrollVelocity;
            return output;
        }

        public static float scrollValueAcceleratedBoost(float customDrag)
        {
            float output = 0;
            float input = scrollValue();

            if (Mathf.Abs(input) > Mathf.Abs(scrollVelocity))
            {
                scrollVelocity = 1;
            }
            else
            {
                if (Time.frameCount % Mathf.RoundToInt(2000 * Time.deltaTime) == 0)
                {
                    scrollVelocity *= customDrag;
                }
            }

            if (Mathf.Abs(scrollVelocity) < 0.0001f)
            {
                scrollVelocity = 0;
            }


            output = scrollVelocity;
            return output;
        }

        // ---------------------------------------------------------------------------- \\
        // ----------------------------- Helper functions ----------------------------- \\
        // ---------------------------------------------------------------------------- \\

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


        internal static float platformDependentConversion(float input)
        {
            float output = input;
            float d = Time.deltaTime;

            if (deltaTimeInput)
            {
                output *= d;
                // Mapping for Mac output values
                if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
                {
                    output = Mathf.Clamp(output, -macMaxOutput * d, macMaxOutput * d);
                    output = map(output, -macMaxOutput * d, macMaxOutput * d, -1, 1);
                }
                // Mapping for Windows output values
                else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
                {
                    output = Mathf.Clamp(output, -winMaxOutput * d, winMaxOutput * d);
                    output = map(output, -winMaxOutput * d, winMaxOutput * d, -1, 1);
                }
            }
            else
            {
                // Mapping for Mac output values
                if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
                {
                    output = Mathf.Clamp(output, -macMaxOutput, macMaxOutput);
                    output /= macMaxOutput;

                }
                // Mapping for Windows output values
                else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
                {
                    output = Mathf.Clamp(output, -winMaxOutput, winMaxOutput);
                    output /= winMaxOutput;
                }
            }

            return output;
        }


        internal static float arbitraryZeroFiltering(float input)
        {
            float output = 0;

            if (input == 0)
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
                output = input;
            }

            return output;
        }


        internal static float webGLInputScalar(float input)
        {
            float output = input;

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                output *= webGLScalar;
            }

            return output;
        }
    }
}
