using System.Collections;
using System.Collections;
using UnityEngine;

public class GhostPlatform : MonoBehaviour, IControllableProp
{
    [Header("Platform Settings")]
    [SerializeField] private Collider2D platformCollider;
    [SerializeField] private Animator animator;

    [Header("Animation Settings")]
    [SerializeField] private string appearTrigger = "Appear";      // 開啟動畫
    [SerializeField] private string disappearTrigger = "Disappear"; // 關閉動畫
    [SerializeField] private float appearDuration = 1f;            // 動畫時間 (秒)
    [SerializeField] private float disappearDuration = 1f;

    private enum PlatformState { Idle, Activating, Active, Deactivating }
    private PlatformState _currentState = PlatformState.Idle;

    private bool isOn = false;  // 記錄平台目前是否開啟中

    private void Start()
    {
        // 一開始平台是關閉狀態
        platformCollider.enabled = false;
        animator.Play("Idle", 0, 0);
    }

    public void Switch()
    {
        if (_currentState == PlatformState.Activating || _currentState == PlatformState.Deactivating)
            return; // 避免在動畫進行中重複觸發

        if (!isOn)
            StartCoroutine(ActivatePlatform());
        else
            StartCoroutine(DeactivatePlatform());
    }

    private IEnumerator ActivatePlatform()
    {
        _currentState = PlatformState.Activating;
        isOn = true;

        // 播放啟動動畫
        animator.SetTrigger(appearTrigger);

        // 等待動畫完成
        yield return new WaitForSeconds(appearDuration);

        // 啟用碰撞
        platformCollider.enabled = true;

        _currentState = PlatformState.Active;
    }

    private IEnumerator DeactivatePlatform()
    {
        _currentState = PlatformState.Deactivating;
        isOn = false;

        // 播放關閉動畫
        animator.SetTrigger(disappearTrigger);

        // 保留碰撞直到動畫結束
        yield return new WaitForSeconds(disappearDuration);

        // 關閉碰撞
        platformCollider.enabled = false;

        _currentState = PlatformState.Idle;
    }
}

