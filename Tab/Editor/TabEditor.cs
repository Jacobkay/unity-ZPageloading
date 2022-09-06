using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ZTools
{
    [CustomEditor(typeof(Tab))]
    public class TabEditor : Editor
    {
        private Tab tab;
        private SerializedObject test;//序列化
        private SerializedProperty isHoverImgActive, isHoverTxtColor, isHoverImgColor, isOnTabTxt, hoverImage, hoverImageColor, hoverTxtColor, hoverImg, isOnTxtColor, isOnImgActive, isOnImgColor, tabChangeColorImg, changeTxtColor, imgColor, tabTxt, tabImg, isOn, showPanel, tabName, tabcontroller, multipleChoice;//定义类型，变量a，变量b
        private void OnEnable()
        {
            test = new SerializedObject(target);
            isOnTxtColor = test.FindProperty("isOnTxtColor");
            isOnImgActive = test.FindProperty("isOnImgActive");
            isOnImgColor = test.FindProperty("isOnImgColor");
            tabChangeColorImg = test.FindProperty("tabChangeColorImg");
            changeTxtColor = test.FindProperty("changeTxtColor");
            imgColor = test.FindProperty("imgColor");
            isHoverImgActive = test.FindProperty("isHoverImgActive");
            isHoverImgColor = test.FindProperty("isHoverImgColor");
            hoverImage = test.FindProperty("hoverImage");
            isOnTabTxt = test.FindProperty("isOnTabTxt");
            hoverImageColor = test.FindProperty("hoverImageColor");
            tabTxt = test.FindProperty("tabTxt");
            tabImg = test.FindProperty("tabImg");
            isHoverTxtColor = test.FindProperty("isHoverTxtColor");
            hoverTxtColor = test.FindProperty("hoverTxtColor");
            isOn = test.FindProperty("isOn");
            showPanel = test.FindProperty("showPanel");
            tabName = test.FindProperty("tabName");
            hoverImg = test.FindProperty("hoverImg");
            tabcontroller = test.FindProperty("tabcontroller");
            multipleChoice = test.FindProperty("multipleChoice");
        }
        public override void OnInspectorGUI()
        {
            test.Update();
            // 开始监测修改
            EditorGUI.BeginChangeCheck();
            tab = (Tab)target;
            EditorGUILayout.PropertyField(multipleChoice);
            EditorGUILayout.PropertyField(isOn);
            // 鼠标移入状态设置
            EditorGUILayout.PropertyField(isHoverImgActive);
            if (isHoverImgActive.boolValue)
            {
                EditorGUILayout.PropertyField(hoverImg);
            }
            EditorGUILayout.PropertyField(isHoverTxtColor);
            if (isHoverTxtColor.boolValue)
            {
                EditorGUILayout.PropertyField(tabTxt);
                EditorGUILayout.PropertyField(hoverTxtColor);
            }
            EditorGUILayout.PropertyField(isHoverImgColor);
            if (isHoverImgColor.boolValue)
            {
                EditorGUILayout.PropertyField(hoverImage);
                EditorGUILayout.PropertyField(hoverImageColor);
            }
            EditorGUILayout.PropertyField(isOnImgActive);
            if (isOnImgActive.boolValue)
            {
                EditorGUILayout.PropertyField(tabImg);
            }
            EditorGUILayout.PropertyField(isOnTxtColor);
            if (isOnTxtColor.boolValue)
            {
                EditorGUILayout.PropertyField(isOnTabTxt);
                EditorGUILayout.PropertyField(changeTxtColor);
            }
            EditorGUILayout.PropertyField(isOnImgColor);
            if (isOnImgColor.boolValue)
            {
                EditorGUILayout.PropertyField(tabChangeColorImg);
                EditorGUILayout.PropertyField(imgColor);
            }
            if (isOnTxtColor.boolValue || isOnImgActive.boolValue || isOnImgColor.boolValue)
            {
                EditorGUILayout.PropertyField(showPanel);
            }
            EditorGUILayout.PropertyField(tabcontroller);
            EditorGUILayout.PropertyField(tabName);
            //结束监测，判断是否有修改
            if (EditorGUI.EndChangeCheck())
            {
                test.ApplyModifiedProperties();
            }
        }
    }
}
