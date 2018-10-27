using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ChinnieUtilities
{
    public struct TouchLogPosition
    {
        public Vector2 touchDownPos;
        public Vector2 touchUpPos;

        public override string ToString()
        {
            return "TouchDown : " + touchDownPos.ToString() + " " + "TouchUp : " + touchUpPos.ToString();
        }
    }

    public class Logger : MonoBehaviour
    {
        [Range(1, 100)] public int maxLogCount = 60;
        private List<string> logs = new List<string>();
        private StringBuilder stringBuilder = new StringBuilder();
        private TouchLogPosition touchLog;
        private bool isTrigger;


        void OnEnable()
        {
            defaultFontSize = Convert.ToInt32(40 * 1080 / Convert.ToSingle(Screen.width));
            Application.logMessageReceivedThreaded += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceivedThreaded -= HandleLog;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                touchLog = new TouchLogPosition();
                touchLog.touchDownPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                touchLog.touchUpPos = Input.mousePosition;
                CheckToggle();
            }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            TouchPhase phase = Input.GetTouch(0).phase;
            if (phase == TouchPhase.Began)
            {
                touchLog = new TouchLogPosition();
                touchLog.touchDownPos = Input.GetTouch(0).position;
            }
            else if (phase == TouchPhase.Ended)
            {
                touchLog.touchUpPos = Input.GetTouch(0).position;
                CheckToggle();  
            }
        }
#endif
        }

        private void CheckToggle()
        {
            float w = Convert.ToSingle(Screen.width);
            float h = Convert.ToSingle(Screen.height);

            bool passMinX = touchLog.touchUpPos.x < 0.15 * w;
            bool passMinY = touchLog.touchUpPos.y < 0.15 * h;

            bool passMaxX = touchLog.touchDownPos.x > 0.85 * w;
            bool passMaxY = touchLog.touchDownPos.y > 0.85 * h;

            if (passMinX && passMinY && passMaxX && passMaxY)
            {
                isTrigger = !isTrigger;
            }
        }

        void HandleLog(string message, string stackTrace, LogType type)
        {
            stringBuilder.Length = 0;
            if (logs.Count >= maxLogCount)
                logs.RemoveAt(0);

            if (type == LogType.Log)
                logs.Add(type + " : " + message);
            else
                logs.Add(stackTrace + " : " + message);

            for (int i = 0; i < logs.Count; i++)
                stringBuilder.AppendLine(logs[i]);
        }

        #region GUI  
        private GUIStyle borderStyle;
        private Vector2 guiScrollPos;
        private Rect addRect;
        private Rect subRect;
        private bool resetTrigger;
        private bool isInitialGui;
        private int defaultFontSize;
        void OnGUI()
        {
            if (!isTrigger)
            {
                resetTrigger = true;
                return;
            }
            else
            {
                if (resetTrigger)
                {
                    if (!isInitialGui)
                    {
                        InitialGUI();
                        isInitialGui = true;
                    }
                    ResetGUI();
                    resetTrigger = false;
                }
            }

            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height), borderStyle);
            GUILayout.Space(15);
            guiScrollPos = GUILayout.BeginScrollView(guiScrollPos, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
            GUILayout.Label(stringBuilder.ToString());
            GUILayout.EndScrollView();
            GUILayout.EndArea();
            if (GUI.Button(subRect, "-"))
                GUI.skin.label.fontSize = GUI.skin.label.fontSize <= defaultFontSize / 2 ? defaultFontSize / 2 : GUI.skin.label.fontSize - 5;
            if (GUI.Button(addRect, "+"))
                GUI.skin.label.fontSize = GUI.skin.label.fontSize >= defaultFontSize * 2 ? defaultFontSize * 2 : GUI.skin.label.fontSize + 5;
        }

        private Texture2D MakeTexture(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        private void InitialGUI()
        {
            borderStyle = new GUIStyle(GUI.skin.box);
            borderStyle.normal.background = MakeTexture(2, 2, new Color(0f, 0f, 0f, 0.65f));
            GUI.skin.label.fontSize = defaultFontSize;
            float size = Screen.width > Screen.height ? Convert.ToSingle(Screen.height) * 0.1f : Convert.ToSingle(Screen.width) * 0.1f;
            subRect = new Rect(Screen.width - size, 5, size, size);
            addRect = new Rect(Screen.width - (size * 2) - 5f, 5, size, size);
        }

        private void ResetGUI()
        {
            guiScrollPos.y = float.MaxValue;
        }

        #endregion
    }
}