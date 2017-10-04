using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017.Exceptions {
  class InsufficientCreditsException : Exception {

    public InsufficientCreditsException() : base() {
    }

    public InsufficientCreditsException(string message) : base(message) {
    }

  }
}
