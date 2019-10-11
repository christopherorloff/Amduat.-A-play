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
        static float lastValue;
        static float lastValueVar;
        static Queue<float> lastValues = new Queue<float>();

        // Detect beginning of input - Event maybe?
            // Very hard to do

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
                }else
                {
                    output = "Down";
                }
            }
            return output;
        }
        // Input with acceleration (artificial) //
            //Time for how long the input goes on

        // Input with acceleration (system based) //

        //The basis for all scroll values. Singular instances of zero value filtered out
            //Mac pad = 25
            //Mac mouse = 30
        public static float scrollValue()
        {
            float newValue = Input.mouseScrollDelta.y;
            float output = 0;
            if (newValue == 0)
            {
                if (lastValueVar == 0)
                {
                    output = 0;
                }
                else
                {
                    output = lastValueVar;
                }
            }
            else
            {
                output = newValue;
            }

            lastValueVar = newValue;

            output = Mathf.Clamp(output,-25,25);
            output /= 25;

            return output;
        }

        // Scroll value that takes the previous n number of values into account
        public static float scrollValueMean(int n)
        {
            float output = 0;
            lastValues.Enqueue(scrollValue());
            
            if (lastValues.Count > n)
            {
                lastValues.Dequeue();
            }
            output = GetMeanOfQueue(n, lastValues);
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
    }
}
