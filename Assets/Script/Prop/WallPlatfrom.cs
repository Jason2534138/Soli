using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatfrom : MonoBehaviour, IControllableProp
{
    [Header("路徑設定")]
    [SerializeField] private Transform[] _path;
    private int current = 0;

    [Header("偵測設定")]
    [SerializeField] private Transform[] _detect;
    [SerializeField] private LayerMask _layerMask;
    private int _detectCurrent = 0;

    [Header("移動參數")]
    [Tooltip("最高移動速度")]
    [SerializeField] private float _maxSpeed = 10f; // 原本的 _speed，現在代表最高速限

    [Tooltip("平滑時間：數值越小越快，越大越慢 (建議 0.2 ~ 0.5)")]
    [SerializeField] private float _smoothTime = 0.3f;

    // SmoothDamp 專用的速度參考變數 (不需要手動修改)
    private Vector2 _currentVelocity = Vector2.zero;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // 我先把這行註解掉，避免遊戲開始時平台瞬間移動 (如果你想要瞬移可以解開)
        // this.transform.position = _path[current].position;
    }

    private void Update()
    {
        // 1. 計算目標位置
        Vector2 targetPos = _path[current].position;

        // 2. 檢查前方是否有障礙物
        bool isBlocked = Physics2D.OverlapCircle(_detect[_detectCurrent].position, 0.1f, _layerMask);

        // 3. 判斷是否還沒到達目標 (距離 > 0.05) 且 沒有被擋住
        if (Vector2.Distance(this.transform.position, targetPos) > 0.05f && !isBlocked)
        {
            // --- 核心修改：使用 SmoothDamp 實現緩入緩出 ---
            this.transform.position = Vector2.SmoothDamp(
                this.transform.position, // 目前位置
                targetPos,               // 目標位置
                ref _currentVelocity,    // 當前速度 (會被函式自動修改)
                _smoothTime,             // 平滑時間 (緩衝感)
                _maxSpeed                // 最高速限
            );
        }
        else
        {
            // 到達目標 或 被擋住時，重置速度，確保下次啟動不會爆衝
            _currentVelocity = Vector2.zero;
            _rb.velocity = Vector2.zero;
        }
    }

    private void ChangePath()
    {
        current += 1;
        if (current >= _path.Length) current = 0;

        _detectCurrent = current + 1;
        if (_detectCurrent >= _detect.Length) _detectCurrent = 0;
    }

    public void Switch()
    {
        ChangePath();
    }
}