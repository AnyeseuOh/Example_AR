using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class AR_Example : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public ARRaycastManager arRaycastManager;

    public GameObject playerPrefab;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public bool isStarted = false;
    public GameObject monsterPrefab;
    public Transform playerPos;

    public float curTime=0;
    public float coolTime=2f;

    public Camera mainCam;
    public ScoreManager scoreManager;
    void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>(); //AR 바닥 매니징
        arRaycastManager = GetComponent<ARRaycastManager>(); //AR Raycast 매니징
        scoreManager = GetComponent<ScoreManager>();
    }

    
    void Update()
    {
        if (Input.touchCount != 0 && isStarted == false)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                GameObject player = Instantiate(playerPrefab, hitPose.position + new Vector3(0, playerPrefab.transform.localScale.y, 0), hitPose.rotation);
                playerPos = player.transform;
                isStarted = true;
                DisablePlane();
            }
        }

        if (isStarted)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                GameObject monster = Instantiate(monsterPrefab);
                float rndX = Random.Range(playerPos.position.x - 5f, playerPos.position.x + 5f);
                float rndZ = Random.Range(playerPos.position.z - 5f, playerPos.position.z + 5f);
                monster.transform.position = new Vector3(rndX, playerPos.position.y, rndZ);
                curTime = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Monster"))
                {
                    //에너미 공격
                    scoreManager.score += 1;
                    hit.collider.GetComponent<MonsterController>().DestroyMonster();
                    Debug.Log($"터치한 대상 : {hit.collider.name}");
                }
            }
        }
    }

    public void DisablePlane()
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
