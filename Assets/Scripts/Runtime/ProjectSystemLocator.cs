using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HVNT.Test.Runtime
{
    public class ProjectSystemLocator
    {
        public static ProjectSystemLocator Service { get; } = new();
        private List<Vector3> registeredPoints;
        private Vector3 spawnPoint;
        private List<GameObject> spawnedPointPrefabs;
        private GameObject pointPrefab;

        private BallController ball;
        private BallController ballPrefab;
        public Camera Cam { get; } = Camera.main;

        public event Action onReset;

        public bool HasStarted { get; private set; }

        public ProjectSystemLocator()
        {
            registeredPoints = new List<Vector3>();
            spawnedPointPrefabs = new List<GameObject>();
        }

        public void SetBallPrefab(BallController newBallPrefab)
        {
            ballPrefab = newBallPrefab;
        }

        public void SetPointPrefab(GameObject newPointPrefab)
        {
            pointPrefab = newPointPrefab;
        }

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

        public void RemoveLastPoint()
        {
            if (registeredPoints.Count <= 2)
                return;

            registeredPoints.RemoveAt(registeredPoints.Count - 1);
            MonoBehaviour.Destroy(spawnedPointPrefabs[spawnedPointPrefabs.Count - 1]);

            spawnedPointPrefabs.RemoveAt(spawnedPointPrefabs.Count - 1);
        }

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

        public void Start()
        {
            if (HasStarted)
                return;

            HasStarted = true;
            ball = MonoBehaviour.Instantiate(ballPrefab, spawnPoint, Quaternion.identity);
        }
    }
}

