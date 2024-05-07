using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HVNT.Test.Runtime
{
    public class ProjectSystemLocator
    {
        /// <summary>
        /// Service Pattern
        /// </summary>
        public static ProjectSystemLocator Service { get; } = new();
        private List<Vector3> registeredPoints;
        private Vector3 spawnPoint;
        private List<GameObject> spawnedPointPrefabs;
        private GameObject pointPrefab;

        private BallController ball;
        private BallController ballPrefab;

        /// <summary>
        /// Fetches the main camera.
        /// </summary>
        public Camera Cam { get; } = Camera.main;

        /// <summary>
        /// Callback that gets called when <see cref="ResetAll"/> gets called!
        /// </summary>
        public event Action onReset;

        public bool HasStarted { get; private set; }

        public ProjectSystemLocator()
        {
            registeredPoints = new List<Vector3>();
            spawnedPointPrefabs = new List<GameObject>();
        }
        /// <summary>
        /// Sets the ball prefab to be used for runtime
        /// </summary>
        /// <param name="newBallPrefab"></param>
        public void SetBallPrefab(BallController newBallPrefab)
        {
            ballPrefab = newBallPrefab;
        }

        /// <summary>
        /// Sets the point prefab to be used for runtime
        /// </summary>
        /// <param name="newPointPrefab"></param>
        public void SetPointPrefab(GameObject newPointPrefab)
        {
            pointPrefab = newPointPrefab;
        }

        /// <summary>
        /// Determines the spawn point for when the ball prefab spawns.
        /// </summary>
        /// <param name="newSpawnPoint"></param>
        public void SetSpawnPoint(Vector3 newSpawnPoint)
        {
            spawnPoint = newSpawnPoint;
        }

        public void AddPoint(Vector3 position)
        {
            registeredPoints.Add(position);
            spawnedPointPrefabs.Add(MonoBehaviour.Instantiate(pointPrefab, position, Quaternion.identity));
        }

        public Vector3 GetPoint(int indx)
            => registeredPoints[indx];

        public int PointAmount()
            => registeredPoints.Count;

        /// <summary>
        /// Removes the last placed point. Does not remove any point if the remainder is equal to 2.
        /// </summary>
        public void RemoveLastPoint()
        {
            if (registeredPoints.Count <= 2)
                return;

            registeredPoints.RemoveAt(registeredPoints.Count - 1);
            MonoBehaviour.Destroy(spawnedPointPrefabs[spawnedPointPrefabs.Count - 1]);

            spawnedPointPrefabs.RemoveAt(spawnedPointPrefabs.Count - 1);
        }

        /// <summary>
        /// Resets the entire runtime state.
        /// </summary>
        public void ResetAll()
        {
            registeredPoints.Clear();
            foreach (var obj in spawnedPointPrefabs)
            {
                MonoBehaviour.Destroy(obj);
            }
            spawnedPointPrefabs.Clear();

            MonoBehaviour.Destroy(ball.gameObject);
            ball = null;

            onReset?.Invoke();

            HasStarted = false;
        }

        /// <summary>
        /// Starts the runtime state.
        /// </summary>
        public void Start()
        {
            if (HasStarted)
                return;

            HasStarted = true;
            ball = MonoBehaviour.Instantiate(ballPrefab, spawnPoint, Quaternion.identity);
        }
    }
}

