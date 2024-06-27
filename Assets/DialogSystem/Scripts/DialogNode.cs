using UnityEditor;
using UnityEngine;




namespace DialogSystem
{

#region  Editor
#if UNITY_EDITOR
	using XNode;
	using XNodeEditor;
	//Dialogue Node Editor
	[CustomNodeEditor(typeof(DialogNode))]
	public class DialogNodeEditor : NodeEditor
	{
		public override void OnHeaderGUI()
		{
			GUI.color = Color.white;
			var dtarget = target as DialogNode;
			var headline = (dtarget.Headline != string.Empty) ? (" - " + dtarget.Headline) : "";
			string title = dtarget.name + headline;
			GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
			GUI.color = Color.white;
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DialogNode node = target as DialogNode;
			EditorGUILayout.LabelField("Headline", EditorStyles.boldLabel);
			node.Headline = EditorGUILayout.TextField(node.Headline);
			NodeEditorGUILayout.AddPortField(node.GetInputPort("Input"));
			if (!node.Hide)
			{
				//wide description box with automatic new line
				EditorGUILayout.LabelField("Description", EditorStyles.boldLabel);
				node.Description = EditorGUILayout.TextArea(node.Description, GUILayout.MinHeight(100));

				EditorGUILayout.LabelField("Option 1", EditorStyles.boldLabel);
				node.Option1 = EditorGUILayout.TextField(node.Option1);
				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option1Output"));

				var connectedNode = node.GetOutputPort("Option1Output").Connection?.node;
				node.Option1Output = connectedNode;

				EditorGUILayout.LabelField("Option 2", EditorStyles.boldLabel);
				node.Option2 = EditorGUILayout.TextField(node.Option2);
				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option2Output"));

				connectedNode = node.GetOutputPort("Option2Output").Connection?.node;
				node.Option2Output = connectedNode;

				EditorGUILayout.LabelField("Option 3", EditorStyles.boldLabel);
				node.Option3 = EditorGUILayout.TextField(node.Option3);
				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option3Output"));

				connectedNode = node.GetOutputPort("Option3Output").Connection?.node;
				node.Option3Output = connectedNode;

				EditorGUILayout.LabelField("Option 4", EditorStyles.boldLabel);
				node.Option4 = EditorGUILayout.TextField(node.Option4);
				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option4Output"));

				connectedNode = node.GetOutputPort("Option4Output").Connection?.node;
				node.Option4Output = connectedNode;

				GUILayout.Space(5);
				if (GUILayout.Button("Hide All Options"))
				{
					node.Hide = true;
				}

				var dGraph = node.graph as DialogGraph;
				if (dGraph != null)
				{
					var firstNode = dGraph.GetFirstNode();
					if (firstNode == node)
					{
						GUI.color = Color.red;
						GUILayout.Label("This is the first node.", EditorStyles.boldLabel);
						GUI.color = Color.white;
					}
				}
			}
			else
			{

				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option1Output"));
				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option2Output"));
				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option3Output"));
				NodeEditorGUILayout.AddPortField(node.GetOutputPort("Option4Output"));

				GUILayout.Space(5);
				if (GUILayout.Button("Show All Options"))
				{
					node.Hide = false;
				}

				var dGraph = node.graph as DialogGraph;
				if (dGraph != null)
				{
					var firstNode = dGraph.GetFirstNode();
					if (firstNode == node)
					{
						GUI.color = Color.yellow;
						GUILayout.Label("This is the first node.", EditorStyles.boldLabel);
						GUI.color = Color.white;
					}
				}

			}

			serializedObject.ApplyModifiedProperties();
		}


	}
#endif
#endregion

	/// <summary>
	/// This node is used to create a dialog sequence
	/// </summary>
	[NodeWidth(300), NodeTint("#506f7f")]
	public class DialogNode : Node
	{

		[Input] public Node Input; // The input node

		public string Headline; // The headline of the dialog
		public string Description; // The description of the dialog

		#region  Options
		public string Option1;
		[Output] public Node Option1Output;

		public string Option2;
		[Output] public Node Option2Output;

		public string Option3;
		[Output] public Node Option3Output;

		public string Option4;
		[Output] public Node Option4Output;
		#endregion

		public bool Hide = false; // Hide the options

		/// <summary>
		/// Initialize the node
		/// </summary>
		protected override void Init()
		{
			base.Init();

			name = "Dialog Node";

		}


	}







}