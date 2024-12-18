using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private GameObject pipe;
    [SerializeField] private FlappyController Bird;

    private float timer;

    void Start() {
        //SpawnPipe();
    }

    void Update() {
        if (!Bird.started) {
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }
            return;
        }

        if (timer > maxTime) {
            SpawnPipe();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    void SpawnPipe() {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        GameObject obj = Instantiate(pipe, spawnPos, Quaternion.identity, transform);

        Destroy(obj, 10f);
    }
}
