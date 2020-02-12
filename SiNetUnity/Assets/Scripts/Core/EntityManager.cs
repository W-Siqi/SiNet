﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SiNet {
    // [deprecated soon]
    // entity manager should belong to 'system', other than this special case 'entity manager'
    // for 'system', maintaing the target entites is the core function
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

        public List<SyncEntity> remoteAuthorityGroup;
        public List<SyncEntity> serverAuthorityGroup;
        public List<SyncEntity> localAuthorityGroup;

        public SyncEntity FindEntity(int sceneUID) {
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
        public SyncEntity InstantiateEntity(int prefabID,int sceneUID) {
            try {
                foreach (var entityPrefab in entityTable)
                {
                    if (entityPrefab.id == prefabID)
                    {
                        var entityGO = GameObject.Instantiate(entityPrefab.prefab);
                        var syncGO = entityGO.GetComponent<SyncEntity>();
                        syncGO.mirrorObjectID = prefabID;
                        syncGO.sceneUID = sceneUID;

                        syncGO.authorityType = SyncEntity.AuthorityType.remote;
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

            localAuthorityGroup = new List<SyncEntity>();
            remoteAuthorityGroup = new List<SyncEntity>();
            serverAuthorityGroup = new List<SyncEntity>();

            StartCoroutine(EntityGroupRefreshing());
        }

        IEnumerator EntityGroupRefreshing() {
            while (true) {
                RefreshEntityGroup();
                yield return new WaitForSeconds(1f);
            }
        }

        private void RefreshEntityGroup() {
            foreach (var entity in FindObjectsOfType<SyncEntity>())
            {
                if (entity.authorityType == SyncEntity.AuthorityType.local 
                    && localAuthorityGroup.IndexOf(entity) < 0)
                    localAuthorityGroup.Add(entity);
                else if (entity.authorityType == SyncEntity.AuthorityType.remote
                    && localAuthorityGroup.IndexOf(entity) < 0)
                    remoteAuthorityGroup.Add(entity);
            }
        }
    }
}