using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class ServerTime
    {
        private static bool inited = false;
        private static float localToServerBias;
        public static void InitServerTime(float currentServerTime) {
            inited = true;
            localToServerBias = currentServerTime - Time.time;
        }

        public static float current {
            get {
                if (inited)
                {
                    return Time.time + localToServerBias;
                }
                else {
                    return -1;
                }
            }
        }
    }
}
