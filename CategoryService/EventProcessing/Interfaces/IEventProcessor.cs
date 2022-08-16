using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryService.EventProcessing.Interfaces
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}