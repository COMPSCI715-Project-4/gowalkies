#if UIWIDGETS_DATABIND_SUPPORT
namespace UIWidgets.DataBind
{
	using Slash.Unity.DataBind.Foundation.Observers;

	/// <summary>
	/// Observes value changes of the SelectedNode of an DirectoryTreeView.
	/// </summary>
	public class DirectoryTreeViewSelectedNodeObserver : ComponentDataObserver<UIWidgets.DirectoryTreeView, UIWidgets.TreeNode<UIWidgets.FileSystemEntry>>
	{
		/// <inheritdoc />
		protected override void AddListener(UIWidgets.DirectoryTreeView target)
		{
			target.NodeSelected.AddListener(NodeSelectedDirectoryTreeView);
			target.NodeDeselected.AddListener(NodeDeselectedDirectoryTreeView);
		}

		/// <inheritdoc />
		protected override UIWidgets.TreeNode<UIWidgets.FileSystemEntry> GetValue(UIWidgets.DirectoryTreeView target)
		{
			return target.SelectedNode;
		}

		/// <inheritdoc />
		protected override void RemoveListener(UIWidgets.DirectoryTreeView target)
		{
			target.NodeSelected.RemoveListener(NodeSelectedDirectoryTreeView);
			target.NodeDeselected.RemoveListener(NodeDeselectedDirectoryTreeView);
		}

		void NodeSelectedDirectoryTreeView(UIWidgets.TreeNode<UIWidgets.FileSystemEntry> arg0)
		{
			OnTargetValueChanged();
		}

		void NodeDeselectedDirectoryTreeView(UIWidgets.TreeNode<UIWidgets.FileSystemEntry> arg0)
		{
			OnTargetValueChanged();
		}
	}
}
#endif