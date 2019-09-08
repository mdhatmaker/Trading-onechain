using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Serialization
{
    public interface IMemento
    {
        Memento SaveMemento();
        void RestoreFromMemento(Memento memento);
    } // interface
} // namespace
