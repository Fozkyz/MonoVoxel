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

		public List<GameObject> Children
		{
			get { return _children; }
		}

		#endregion

		private bool _enabled;
		private List<GameObject> _children;

		public GameObject(string name = "GameObject")
		{
			ID = Guid.NewGuid();
			Name = name;
			Tag = string.Empty;
			Enabled = true;
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
	}
}
