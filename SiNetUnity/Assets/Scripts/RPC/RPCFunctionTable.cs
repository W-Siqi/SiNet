﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class RPCFunctionTable
    {
        public const string GET_SERVER_TIME = "GetServerTime";
        public const string BOARDCAST_EVENT = "BoardcastEvent";

		public static RPCReturnHandle GetServerTime() {
            return RPCStub.instance.Call(GET_SERVER_TIME, new RPCVariable());
        }
    }
}