using System;

namespace Entities
{
	public class Player
	{
		public string Name { get; private set; }
		public Player(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}
	}
}
