using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1. 這是最重要的一行！沒有這行程式碼就不認識 Light2D
using UnityEngine.Rendering.Universal;

public class LightFlicker2D : MonoBehaviour
{
    // 2. 把變數型態從 Light 改成 Light2D
    private Light2D _myLight2D;

    [Header("基礎亮度設定")]
    public float baseIntensity = 1f;    // 燈光基礎亮度

    [Header("閃爍效果")]
    [Tooltip("閃爍的強弱幅度")]
    public float flickerAmount = 0.5f;  // 數值越大，忽明忽暗越明顯
    [Tooltip("閃爍的速度")]
    public float flickerSpeed = 5f;     // 數值越大，閃得越快

    void Start()
    {
        // 3. 抓取 Light2D 組件
        _myLight2D = GetComponent<Light2D>();

        // 如果你沒有在 Inspector 設定基礎亮度，就自動抓取當前燈光的亮度
        if (baseIntensity <= 0) baseIntensity = _myLight2D.intensity;
    }

    void Update()
    {
        // 使用 Perlin Noise 產生平滑隨機數 (跟 3D 版邏輯一樣)
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);

        // 4. 修改 Light2D 的 intensity 屬性
        // 這裡做了一點優化：讓 noise 影響正負值，這樣亮度會圍繞著 baseIntensity 上下波動
        _myLight2D.intensity = baseIntensity + (noise * flickerAmount);
    }
}