namespace GameCreator.Core
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[AddComponentMenu("")]
	public class ConditionIsChild : ICondition
	{
		public TargetGameObject targetChild = new TargetGameObject();
		public TargetGameObject targetParent = new TargetGameObject();
		
		public override bool Check(GameObject target)
		{
			GameObject _child = this.targetChild.GetGameObject(target);
			if (_child == null) return false;
			GameObject _parent = this.targetParent.GetGameObject(target);
			if (_parent == null) return false;
			
			return _child.transform.IsChildOf(_parent.transform);
		}
        
		#if UNITY_EDITOR
		public static new string NAME = "Object/Is Child Of";
		
		private const string NODE_TITLE = "Is {0} Child Of {1}";

		public override string GetNodeTitle()
		{
			return string.Format(
				NODE_TITLE,
				this.targetChild,
				this.targetParent
			);
		}
		
		
		#endif
	}
}
