namespace Sources.Editor.ObjectsOnMap {
	public class Man : ObjectOnMap {
		public override ObjectType Type => ObjectType.Man;

		public Resource Resource { get; set; }

		protected override void OnObjectClicked() { }
	}
}