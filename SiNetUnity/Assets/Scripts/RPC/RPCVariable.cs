using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    [System.Serializable]
    public class RPCVariable
    {
        [SerializeField]
        private List<KeyValuePair<string, float>> floats;
        [SerializeField]
        private List<KeyValuePair<string, string>> strings;

        public RPCVariable() {
            floats = new List<KeyValuePair<string, float>>();
            strings = new List<KeyValuePair<string, string>>();
        }

        public void SetFloat(string key, float value) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// return the first on, if key == ""
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetFloat(string key = "")
        {
            throw new System.NotImplementedException();
        }

        public void SetString(string key, float value)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// return the first on, if key == ""
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key = "")
        {
            throw new System.NotImplementedException();
        }
    }
}