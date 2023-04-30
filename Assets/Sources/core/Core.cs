using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.core {
	public abstract class Core {
		private Dictionary<Type, ICoreRegistrable> _typesMap = new();
		private List<ICoreInit> _coreInits = new();

		public void Register<T>(ICoreRegistrable manager){
			if (_typesMap.TryGetValue(typeof(T), out _)) {
				Debug.LogError("ERROR. You try add to CORE same manager type");
				return;
			}

			if (manager is ICoreInit coreInit) {
				_coreInits.Add(coreInit);
			}
			
			_typesMap.Add(typeof(T), manager);
		}

		public T Get<T>() {
			_typesMap.TryGetValue(typeof(T), out ICoreRegistrable result);
			return (T) result;
		}

		public void Remove<T>() {
			if (_typesMap.TryGetValue(typeof(T), out _)) {
				Debug.LogError("ERROR. You try remove from CORE non-existent manager type");
				return;
			}

			_typesMap.Remove(typeof(T));
		}

		public void Dispode() {
			_typesMap.Clear();
			_coreInits.Clear();
		}

		public void InitManagers() {
			foreach (var coreInit in _coreInits) {
				coreInit.Init();
			}
			_coreInits.Clear();
		}
	}
}