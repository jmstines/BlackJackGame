using System;
using System.Collections.Generic;
using System.Text;

namespace Interactors.Providers
{
  public interface IIdentifierProvider
  {
    string Generate();
  }
}
