using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiNet {
    public class MovementSystem : MonoBehaviour
    {
        const float ENTITY_LIST_UPDATE_INTERVAL = 1f;
        const float VERY_SMALL_VAL = 0.01f;

        const float ACCEPTABLE_POSIBITON_BIAS = 0.5f;

        // MovementSystem will create and update the MovementEnity
        // The real-time position caltucated by MovementEntity
        private class MovementEntity {
            public SyncEntity entity;
            public SyncTransform syncTransform;

            private float lastUpdateTime = -1f;
            private Vector3 lastPos;

            private Vector3 curVelocity;

            public bool isValid { get { return entity != null && syncTransform != null; } }

            public MovementEntity(SyncEntity entity, SyncTransform transform) {
                this.entity = entity;
                this.syncTransform = transform;
            }

            // TBD: now the way calculation position is a placeholder
            // MUST be called every frame!
            public void UpdateMovement() {
                if (!isValid)
                    return;

                if (lastUpdateTime < 0)
                {
                    InitAsFirstState();
                }
                else
                {
                    if (isTransformUpdated)
                        UpdateToLastestState();
                    else
                        UpdateBasedOnCurrentState();
                }
            }

            private void InitAsFirstState() {
                lastUpdateTime = syncTransform.timeStamp;

                entity.transform.position = syncTransform.position;
                entity.transform.rotation = syncTransform.rotation;

                curVelocity = Vector3.zero;
            }

            private void UpdateToLastestState() {
                var currentTheoryPosition = PredictCurrentPosition(
                    lastPos, lastUpdateTime,
                    syncTransform.position, syncTransform.timeStamp,
                    ServerTime.current
                    );
                var predictedVelocity = (syncTransform.position - lastPos) / (syncTransform.timeStamp - lastUpdateTime);
                var postionBias = (currentTheoryPosition - entity.transform.position);

                if (postionBias.magnitude > ACCEPTABLE_POSIBITON_BIAS)
                {
                    // current position so different from the predictable value (for the latency or networking condition)
                    // in this case, correct the valure directly
                    entity.transform.position = syncTransform.position;
                    entity.transform.rotation = syncTransform.rotation;

                    curVelocity = predictedVelocity;
                }
                else {
                    // in this case, everything goes well as the prediction
                    // so just follow the predicted velocity
                    // but need a little correstion based on the bias between current positon and predicted current postion
                    var prediectedStateUpdateInterval = syncTransform.timeStamp - lastUpdateTime;
                    var velocityCorrection = postionBias / prediectedStateUpdateInterval;
                    curVelocity = predictedVelocity + velocityCorrection;

                    entity.transform.position += curVelocity * Time.deltaTime;
                }

                lastPos = syncTransform.position;
                lastUpdateTime = syncTransform.timeStamp;
            }

            private void UpdateBasedOnCurrentState() {
                entity.transform.position += curVelocity * Time.deltaTime;
            }

            private Vector3 PredictCurrentPosition(Vector3 pos1,float time1,Vector3 pos2,float time2,float currentTime){
                var velocity = (pos2 - pos1) / (time2 - time1);
                return pos2 + velocity * (currentTime - time2);
            }

            private bool isTransformUpdated {
                get {
                    return Mathf.Abs(lastUpdateTime - syncTransform.timeStamp) > VERY_SMALL_VAL;
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