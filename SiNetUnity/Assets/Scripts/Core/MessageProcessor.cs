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
                default:
                    Debug.LogError("cannot handle this type yet");
                    break;
            }
        }

		private static void ProcessSyncMessage(Message message) {
            try
            {
                var syncGOSnapshot = JsonUtility.FromJson<TransmitableSyncGOSnapshot>(message.body).ToOriginalObject() as SyncGOSnapshot;
                Debug.Log("[Message Stack] - sync messsage :" + syncGOSnapshot.sceneUID);

                // if entity cannot find, then instantiate it
                var entity = EntityManager.instance.FindEntity(syncGOSnapshot.sceneUID);
                if (entity == null)
                    entity = EntityManager.instance.InstantiateEntity(syncGOSnapshot.mirrorObjectID,syncGOSnapshot.sceneUID);
                if (entity == null) 
                    return;

                // sync or not depend on its authroity 
                if (entity.authorityType != SyncGameObject.AuthorityType.local)
                {
                    entity.SyncToSnapshot(syncGOSnapshot);
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
    }
}