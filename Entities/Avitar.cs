using System;

namespace Entities
{
	public class Avitar
	{
		public string Name { get; private set; }
		public Avitar(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));
	}
}
