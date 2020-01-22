using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SiNet {
    public class EntityManager : MonoBehaviour
    {
        public static EntityManager instance = null;

        [System.Serializable]
        public class EntityPrefab {
            public int id;
            public GameObject prefab;
        }

        [SerializeField]
        private List<EntityPrefab> entityTable;


        public List<SyncGameObject> remoteAuthorityGroup;
        public List<SyncGameObject> serverAuthorityGroup;
        public List<SyncGameObject> localAuthorityGroup;

        public SyncGameObject FindEntity(int sceneUID) {
            foreach (var entity in remoteAuthorityGroup)
                if (entity.sceneUID == sceneUID)
                    return entity;

            foreach (var entity in serverAuthorityGroup)
                if (entity.sceneUID == sceneUID)
                    return entity;

            foreach (var entity in localAuthorityGroup)
                if (entity.sceneUID == sceneUID)
                    return entity;

            return null;
        }

        // the default authoriy type is: remove authoriy
        public SyncGameObject InstantiateEntity(int prefabID,int sceneUID) {
            try {
                foreach (var entityPrefab in entityTable)
                {
                    if (entityPrefab.id == prefabID)
                    {
                        var entityGO = GameObject.Instantiate(entityPrefab.prefab);
                        var syncGO = entityGO.GetComponent<SyncGameObject>();
                        syncGO.mirrorObjectID = prefabID;
                        syncGO.sceneUID = sceneUID;

                        syncGO.authorityType = SyncGameObject.AuthorityType.remote;
                        remoteAuthorityGroup.Add(syncGO);

                        return syncGO;
                    }
                }

                return null;
            }
            catch {
                return null;
            }
        } 
        private void Start()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this);

            localAuthorityGroup = FindObjectsOfType<SyncGameObject>().OfType<SyncGameObject>().ToList();
            remoteAuthorityGroup = new List<SyncGameObject>();
            serverAuthorityGroup = new List<SyncGameObject>();
        }
    }
}