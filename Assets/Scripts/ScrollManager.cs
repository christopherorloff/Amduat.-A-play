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

        //Variables for internal use:
        static float lastValue;
        static float lastValueVar;
        static Queue<float> lastValues = new Queue<float>();
        
        // Detect beginning of input - Event maybe?
            // Very hard to do

        // Bool - Input or not
        public static bool isScrolling()
        {
            bool status = false;
            status = (scrollValue() != 0) ? true : false;
            return status;
        }
        // Direction - Are we receiving input, and is it up or down

        // Input with acceleration (artificial) //

        // Input with acceleration (system based) //

        //The basis for all scroll values. Singular instances of zero value filtered
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
            return output;
        }

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
