using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class MovementSystem : MonoBehaviour
    {
        const float ENTITY_LIST_UPDATE_INTERVAL = 1f;

        // MovementSystem will create and update the MovementEnity
        // The real-time position caltucated by MovementEntity
        private class MovementEntity {
            public SyncEntity entity;
            public SyncTransform syncTransform;

            private float lastUpdateTime;
            private Vector3 lastPos;

            public bool isValid { get { return entity != null && syncTransform != null; } }

            public MovementEntity(SyncEntity entity, SyncTransform transform) {
                this.entity = entity;
                this.syncTransform = transform;
            }

            // TBD: now the way calculation position is a placeholder
            public void UpdateMovement() {
                if (isValid) {
                    entity.transform.position = syncTransform.position;
                    entity.transform.rotation = syncTransform.rotation;
                }
            }

            private MovementEntity() { }
        }

        private List<MovementEntity> targets= new List<MovementEntity>();

        private void Update()
        {
            UpdateMovement();
        }

        private void Start() {
            StartCoroutine(EntitiesUpdateLoop());
        }

        // if new entities added, it will be detected at next loop
        // it will also clear the invalid entities 
        IEnumerator EntitiesUpdateLoop() {
            while (true) {
                UpdateEntityList();
                yield return new WaitForSeconds(ENTITY_LIST_UPDATE_INTERVAL);
            }
        }

        private void UpdateEntityList() {
            // clear old entity
            var toRemove = new List<MovementEntity>();
            foreach (var tar in targets)
                if (!tar.isValid)
                    toRemove.Add(tar);
            foreach (var tar in toRemove)
                targets.Remove(tar);


            // check new entity
            foreach (var entity in GameObject.FindObjectsOfType<SyncEntity>())
            {
                if (IsMovementEntity(entity) && !IsInTargets(entity))
                {
                    var newTarget = new MovementEntity(entity, entity.GetComponent<SyncTransform>());
                    targets.Add(newTarget);
                }                   
            }
        }

        private bool IsInTargets(SyncEntity entity) {
            foreach(var tar in targets)
            {
                if (tar.entity == entity)
                    return true;
            }
            return false;
        }

        // defineation of movment entity is:
        // 1-have syncTransfrom component
        // 2-isn't local authority
        private bool IsMovementEntity(SyncEntity entity) {
            return entity.authorityType != SyncEntity.AuthorityType.local
                    && entity.GetComponent<SyncTransform>();
        }

        // envoke exsited movement entity to caltulate lastest position
        private void UpdateMovement() {
            foreach (var tar in targets) {
                if (tar.isValid)
                    tar.UpdateMovement();
            }
        }
    }
}