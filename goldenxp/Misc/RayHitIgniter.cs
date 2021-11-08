namespace GameCreator.Core
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using GameCreator.Variables;

	[AddComponentMenu("")]
	public class RayHitIgniter : Igniter 
	{
		public TargetGameObject caster = new TargetGameObject(TargetGameObject.Target.GameObject);
		public float distance = Mathf.Infinity;
		public Vector3 direction = Vector3.forward;
		public LayerMask mask;
		
		[Space, VariableFilter(Variable.DataType.Vector3)]
		public VariableProperty storeHitPoint = new VariableProperty();
		
		[Space][VariableFilter(Variable.DataType.GameObject)]
		public VariableProperty storeCollider = new VariableProperty(Variable.VarType.GlobalVariable);
		
		public Actions failAction;
		
		#if UNITY_EDITOR
		public new static string NAME = "Physics/On Ray Hit";
		#endif

		private void Update()
		{
			GameObject casterGO = caster.GetGameObject(this.gameObject);
			
			RaycastHit hit;
			if (Physics.Raycast(casterGO.transform.position, casterGO.transform.TransformDirection(direction), out hit, distance, mask))
			{
				Debug.DrawRay(casterGO.transform.position, casterGO.transform.TransformDirection(direction) * hit.distance, Color.yellow);
				
				this.storeHitPoint.Set(hit.point, this.gameObject);
				this.storeCollider.Set(hit.collider.gameObject, this.gameObject);
				
				this.ExecuteTrigger(gameObject);
			}
			else
			{
				if (failAction)
				{
					failAction.actionsList.Execute(this.gameObject, this.OnFailActionComplete);
				}
			}
		}
		
		private void OnFailActionComplete()
		{
		}
	}
}