using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class PDUParser
    {
        public static Message[] ParsePDU(string data) {
            var messages = new List<Message>();
            int cur = 0;
            while (cur < data.Length) {
                if (data[cur] != '%')
                    break;

                // skip %
                cur++;

                // read number
                string numStr = "";
                while (cur < data.Length) {
                    if (data[cur] >= '0' && data[cur] <= '9')
                        numStr += data[cur++];
                    else
                        break;
                }

                int len = StringToInt(numStr);

                try
                {
                    var PDU = data.Substring(cur, len);
                    var msg = JsonUtility.FromJson<Message>(PDU);
                    messages.Add(msg);

                }
                catch (System.Exception e) {
                    Debug.Log(e.Message);
                }

                cur += (len + 1);
            }

            return messages.ToArray();
        }

        private static int StringToInt(string str) {
            int res = 0;
            for (int i = 0; i < str.Length; i++) {
                res += (str[i] - '0') * Pow10( str.Length - i -1);
            }
            return res;
        }

        private static int Pow10(int times) {
            int res = 1;
            for (int i = 0; i < times; i++)
                res *= 10;
            return res;
        }
    }
}
