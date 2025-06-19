using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform target; // 拱門目標
    private NavMeshAgent agent;

    void Start()
    {
        // 找到拱門位置
        GameObject door = GameObject.Find("door");
        if (door != null)
        {
            target = door.transform;
        }
        else
        {
            Debug.LogWarning("找不到名為 'door' 的物件！");
        }

        // 取得 NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // 設定初始目的地
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            HandleRay(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                HandleRay(Camera.main.ScreenPointToRay(touch.position));
            }
        }
#endif

        // 若拱門存在，持續更新目的地（可選）
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void HandleRay(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("bad"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
