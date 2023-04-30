using System.Collections.Generic;

namespace Sources.Editor.ObjectsOnMap {
	public class Man : ObjectOnMap {
		public override ObjectType Type => ObjectType.Man;

		public List<Resource> Resources { get; set; }

		protected override void OnObjectClicked() { }
	}
}