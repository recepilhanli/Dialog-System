using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace DialogSystem
{

	#region Editor
#if UNITY_EDITOR
	using UnityEditor;
	using XNodeEditor;

	//Dialogue Event Node Editor
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
#endif
	#endregion


	/// <summary>
	/// This node is used to invoke a UnityEvent
	/// </summary>
	[NodeWidth(350), NodeTint("#ff7f50")]
	public class DialogEvent : Node
	{
		/// <summary>
		/// The input node
		/// </summary>
		[Input] public Node Input;

		/// <summary>
		/// The output node
		/// </summary>
		[Output] public Node Output;

		/// <summary>
		/// The event that will be invoked
		/// </summary>
		public UnityEvent Event = new UnityEvent();

		/// <summary>
		/// This method is used to invoke the event
		/// </summary>
		public void InvokeEvent()
		{
			Event?.Invoke();
		}



	}

}