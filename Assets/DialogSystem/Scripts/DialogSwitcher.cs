using UnityEditor;
using UnityEngine;



namespace DialogSystem
{

#region Editor
#if UNITY_EDITOR
	using XNode;
	using XNodeEditor;
	//Dialogue Node Editor
	[CustomNodeEditor(typeof(DialogSwitcher))]
	public class DialogSwitcherEditor : NodeEditor
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

			var node = target as DialogSwitcher;

			NodeEditorGUILayout.AddPortField(node.GetInputPort("Input"));
			node.Input = node.GetInputPort("Input").Connection?.node;

			DialogGraph newDialogGraph = EditorGUILayout.ObjectField("Next Dialog Graph", node.NextDialogGraph, typeof(DialogGraph), false) as DialogGraph;
			if (newDialogGraph != node.NextDialogGraph)
			{
				node.NextDialogGraph = newDialogGraph;
				serializedObject.ApplyModifiedProperties();
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
#endregion

	/// <summary>
	/// This node is used to switch between different dialog graphs
	/// </summary>
	[NodeWidth(300), NodeTint("#ff0f0f")]
	public class DialogSwitcher : Node
	{
		/// <summary>
		/// The input node
		/// </summary>
		[Input] public Node Input;

		/// <summary>
		/// The next dialog graph
		/// </summary>
		public DialogGraph NextDialogGraph;
	}
}


