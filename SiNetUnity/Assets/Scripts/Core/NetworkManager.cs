using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet{
    // TBD: upload message should belong to message collector
    public class NetworkManager : MonoBehaviour
    {
        const float CONNECTION_CHECK_INTERVAL = 1f;

        private EntityManager entityManager;

        [SerializeField]
        private NetworkConfig config;

        private ReceiveBuffer receiveBuffer;
        private ServerConnection serverConnection;

        [SerializeField]
        private bool online = false;

        IEnumerator Start()
        {
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
                else if (serverConnection.isConnected && !online)
                {
                    // first find connected
                    // init
                    Debug.Log("[SiNet] connect success, init...");

                    var returnHandle = RPCStub.instance.Call(
                        RPCStub.CallableFunction.getServerTime,
                        new RPCVariable());

                    // the RPC message will not be sent automatically
                    // because the network hasn't been inited, so we need to send there
                    var sendMessages = MessageCollector.instance.ReadMessages();
                    foreach(var m in sendMessages)
                        serverConnection.Send(m);

                    // wait when server time is ready
                    while (true)
                    {
                        if (returnHandle.isReady)
                        {
                            ServerTime.InitServerTime(returnHandle.returnValue.floatValues[0]);
                            break;
                        }
                     
                        yield return null;
                    }

                    online = true;
                    OnConnectSuccess();            
                }

                if (!online) {
                    Debug.LogWarning("[SiNet] connecting...");
                }

                yield return new WaitForSeconds(CONNECTION_CHECK_INTERVAL);
            }
        }

        IEnumerator MessageCheckLoop()
        {
            while (true)
            {
                try
                {
                    var messages = receiveBuffer.ReadAllMessages();
                    foreach (var m in messages)
                    {
                        MessageProcessor.Process(m);
                    }
                }
                catch(System.Exception e) {
                    Debug.Log(e.Message);
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
            Debug.Log("connect success at server time: " + ServerTime.current);
        }

        private void OnApplicationQuit()
        {
            if (serverConnection!=null)
                serverConnection.Abort();
        }
    }
}
