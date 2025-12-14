using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyHighlighter
{
    static HierarchyHighlighter()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int selectionID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(selectionID) as GameObject;
        if (obj == null) return;

        string name = obj.name;

        // 只有當名字開頭是 # 才會執行變色邏輯
        if (name.StartsWith("#"))
        {
            // 1. 預設顏色 (深灰色)
            Color bkColor = new Color(0.3f, 0.3f, 0.3f);
            Color textColor = Color.white;
            string textToShow = name.Replace("#", "").ToUpper(); // 預設只去掉 #

            // 2. 判斷顏色代碼 (檢查開頭兩個字)
            // 格式： #r名字, #g名字, #b名字
            if (name.StartsWith("#r")) // Red 紅色
            {
                bkColor = new Color(0.8f, 0.2f, 0.2f); // 紅色
                textToShow = name.Substring(2).ToUpper(); // 去掉前兩個字 (#r)
            }
            else if (name.StartsWith("#g")) // Green 綠色
            {
                bkColor = new Color(0.2f, 0.6f, 0.2f); // 綠色
                textToShow = name.Substring(2).ToUpper();
            }
            else if (name.StartsWith("#b")) // Blue 藍色
            {
                bkColor = new Color(0.2f, 0.4f, 0.8f); // 藍色
                textToShow = name.Substring(2).ToUpper();
            }
            else if (name.StartsWith("#y")) // Yellow 黃色
            {
                bkColor = new Color(0.8f, 0.8f, 0.2f); // 黃色
                textColor = Color.white; // 黃底配黑字比較清楚
                textToShow = name.Substring(2).ToUpper();
            }
            else if (name.StartsWith("#p")) // Purple 紫色
            {
                bkColor = new Color(0.6f, 0.2f, 0.8f); // 紫色
                textToShow = name.Substring(2).ToUpper();
            }

            // 3. 繪製背景
            // 這裡把 Rect 的 x 和 width 調整一下，讓它填滿整行，不留縮排
            Rect fullRect = new Rect(selectionRect);
            fullRect.x = 0;
            fullRect.width += 50; // 稍微加寬確保蓋住右邊

            EditorGUI.DrawRect(fullRect, bkColor);

            // 4. 繪製文字
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = textColor;

            EditorGUI.DropShadowLabel(selectionRect, textToShow, style);
        }
    }
}