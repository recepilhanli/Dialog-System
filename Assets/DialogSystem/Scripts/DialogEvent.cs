using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using XNode;
using XNodeEditor;

namespace DialogSystem
{
	/// <summary>
	/// 
	/// </summary>
	
	[NodeWidth(350), NodeTint("#ff7f50")]
	public class DialogEvent : Node
	{
		[Input] public Node Input;

		[Output] public Node Output;

		public UnityEvent Event = new UnityEvent();

		public void InvokeEvent()
		{
			Event?.Invoke();
		}



	}


	[CustomNodeEditor(typeof(DialogEvent))]
	public class DialogEventEditor : NodeEditor
	{
		public override void OnHeaderGUI()
		{
			GUI.color = Color.white;
			string title = target.name;
			GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			var node = target as DialogEvent;

			NodeEditorGUILayout.AddPortField(node.GetInputPort("Input"));
			node.Input = node.GetInputPort("Input").Connection?.node;

			NodeEditorGUILayout.AddPortField(node.GetOutputPort("Output"));
			node.Output = node.GetOutputPort("Output").Connection?.node;

			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Event"), true);

			serializedObject.ApplyModifiedProperties();
		}
	}
}