// 作成者：17CU0334 山嵜ジョニー
// 作成日：2019/10/01 09:21
// 概要：private変数をInspectorに表示しつつ編集不可にするもの
// 使用方法
// [SerializeField,NonEditable]
// private int m_iValue = 10;
// 参考：https://kandycodings.jp/2019/03/24/unidev-noneditable/
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class NonEditableAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NonEditableAttribute))]
public sealed class NonEditableAttributeDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}
#endif