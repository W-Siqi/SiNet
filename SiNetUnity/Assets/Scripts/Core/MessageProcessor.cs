using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class MessageProcessor { 
        public static void Process(Message message) {
            switch (EnumProtocal.Decode(message.type)) {
                case Message.Type.syncMessage:
                    ProcessSyncMessage(message);
                    break;
                case Message.Type.RPC:
                    ProcessRPCMessage(message);
                    break;
                default:
                    Debug.LogError("cannot handle this type yet");
                    break;
            }
        }

		private static void ProcessSyncMessage(Message message) {
            try
            {
                var syncEntitySnapshot = JsonUtility.FromJson<TransmitableSyncGOSnapshot>(message.body).ToOriginalObject() as SyncEntitySnapshot;
                Debug.Log("[Message Stack] - sync messsage :" + syncEntitySnapshot.sceneUID);

                // if entity cannot find, then instantiate it
                var entity = EntityManager.instance.FindEntity(syncEntitySnapshot.sceneUID);
                if (entity == null)
                    entity = EntityManager.instance.InstantiateEntity(syncEntitySnapshot.mirrorObjectID, syncEntitySnapshot.sceneUID);
                if (entity == null) 
                    return;

                // sync or not depend on its authroity 
                if (entity.authorityType != SyncEntity.AuthorityType.local)
                {
                    entity.SyncToSnapshot(syncEntitySnapshot);
                    Debug.Log("[Message Stack] - recv remote sync state");
                }
                else {
                    Debug.Log("[Message Stack] - recv local's state");
                }
            }
			catch {
				Debug.Log("[Message Preceoss fail]:" + message.body);
			}
		}

        private static void ProcessRPCMessage(Message message) {
            try
            {
                var transmitable = JsonUtility.FromJson<TransmitableRPCPlaceHolderInfo>(message.body);
                var RPCsig = transmitable.ToOriginalObject() as RPCSignaturePlaceHolder;
                Debug.Log("[Message Stack] - RPC messsage :" + RPCsig.name);
                RPCReceiverPlaceHolder.ReceiveRPC(RPCsig);
            }
            catch(System.Exception e)
            {
                Debug.Log("[Message Preceoss fail]:" + message.body);
                Debug.Log(e.Message);
            }
        }
    }
}