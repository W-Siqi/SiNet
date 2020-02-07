using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet{
    // TBD: upload message should belong to message collector
    public class NetworkManager : MonoBehaviour
    {
        const float CONNECTION_CHECK_INTERVAL = 1f;

        private EntityManager entityManager;
        private EventProcessor eventProcessor;

        [SerializeField]
        private NetworkConfig config;

        private ReceiveBuffer receiveBuffer;
        private ServerConnection serverConnection;

        [SerializeField]
        private bool online = false;

        IEnumerator Start()
        {
            eventProcessor = new EventProcessor();

            receiveBuffer = new ReceiveBuffer();
            serverConnection = new ServerConnection(config.hostIP, config.port,receiveBuffer);

            // wait other module to init
            yield return null;
            entityManager = EntityManager.instance;

            StartCoroutine(ConnectionCheckLoop());
            StartCoroutine(MessageCheckLoop());
            StartCoroutine(SyncLocalToServerLoop());
        }

        // TBD: potential bug: online and isConnected sync problem
        IEnumerator ConnectionCheckLoop()
        {
            while (true)
            {
                if (!serverConnection.isConnected && online)
                {
                    // first lost connection
                    online = false;
                    OnConnectLost();
                }
                else if (!serverConnection.isConnected && !online)
                {
                    Debug.Log("try connect...");
                    yield return new WaitForSeconds(1f);

                    if (serverConnection.isConnected)
                    {
                        online = true;
                        OnConnectSuccess();
                    }
                }

                yield return CONNECTION_CHECK_INTERVAL;
            }
        }

        IEnumerator MessageCheckLoop()
        {
            while (true)
            {
                var messages = receiveBuffer.ReadAllMessages();
                foreach (var m in messages)
                {
                    MessageProcessor.Process(m);
                }
                yield return null;
            }
        }

        IEnumerator SyncLocalToServerLoop()
        {
            while (true)
            {
                if (online)
                {

                    // check messager collecters
                    var messagesFromCollectors = MessageCollector.instance.ReadMessages();
                    foreach (var m in messagesFromCollectors) {
                        serverConnection.Send(m);
                    }

                    // check loacal obejcts
                    var localObjects = entityManager.localAuthorityGroup;
                    var assigned = new List<SyncEntity>();
                    var unassigned = new List<SyncEntity>();

                    foreach (var localObj in localObjects) {
                        if (localObj.sceneUID < 0)
                            unassigned.Add(localObj);
                        else
                            assigned.Add(localObj);
                    }

                    // sync assigned local object to server
                    foreach (var assginedObj in assigned) {
                        var snapshot = assginedObj.GetSnapshot();
                        var transmitObj = new TransmitableSyncGOSnapshot();
                        transmitObj.InitFromOriginalObejct(snapshot);
                        var body = transmitObj.Encode();

                        // Old version:
                        //var body = MessageBodyProtocal.EncodeSyncEntity(assginedObj);

                        var message = new Message(Message.Type.syncMessage, body);

                        serverConnection.Send(message);
                    }

                    // request ID to server
                    foreach (var unassignedObj in unassigned) {
                        unassignedObj.sceneUID = RPCPlaceHolder.TempAllocateSceneUID();
                    }
                }

                yield return new WaitForSeconds(1f / config.syncFrames);
            }
        }   

        private void OnConnectLost()
        {
            Debug.Log("connection lost!");
        }

        private void OnConnectSuccess() {
            Debug.Log("connect success!");
        }

        private void OnApplicationQuit()
        {
            if (serverConnection!=null)
                serverConnection.Abort();
        }
    }
}
