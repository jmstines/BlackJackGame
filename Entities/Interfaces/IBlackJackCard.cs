using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Interfaces
{
	public interface IBlackJackCard : ICard
	{
		bool FaceDown { get; }
	}
}
