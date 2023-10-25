using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoVoxel
{
	public class GameObject
	{
		#region Fields
		public Guid ID { get; private set; }
		public string Name { get; set; }
		public string Tag { get; set; }
		public bool Enabled
		{
			get { return Enabled; }
			set
			{
				if (value != _enabled)
				{
					_enabled = value;
					if (_enabled)
					{
						OnEnabled();
					}
					else
					{ 
						OnDisabled(); 
					}
				}
			}
		}

		public Transform Transform
		{
			get
			{
				return _components[0] as Transform;
			}
		}

		public List<Transform> Children
		{
			get { return Transform.Children; }
		}

		#endregion

		private bool _enabled;
		private List<Component> _components;

		public GameObject(string name = "GameObject")
		{
			ID = Guid.NewGuid();
			Name = name;
			Tag = string.Empty;
			Enabled = true;

			Transform transform = new Transform();
			_components = new List<Component>() { transform };
		}

		public virtual void OnEnabled()
		{
			foreach (var child in Children)
			{
				child.Enabled = true;
			}
		}

		public virtual void OnDisabled()
		{
			foreach (var child in Children)
			{
				child.Enabled = false;
			}
		}

		public Transform GetChild(int index)
		{
			return Transform.GetChild(index);
		}

		public T AddComponent<T>() where T : Component, new()
		{
			var component = new T();
			component.GameObject = this;
			component.Awake();
			component.Start();
			_components.Add(component);

			return component;
		}

		public T GetComponent<T>() where T : Component
		{
			foreach (var component in _components)
			{
				if (component is T)
				{
					return component as T;
				}
			}

			return null;
		}

		public List<T> GetComponents<T>() where T : Component
		{
			var list = new List<T>();
			foreach (var component in _components)
			{
				if (component is T)
				{
					list.Add(component as T);
				}
			}

			return list;
		}
		
		public bool RemoveComponent(Component component)
		{
			if (component is Transform)
				return false;
			
			return _components.Remove(component);
		}
	}
}
