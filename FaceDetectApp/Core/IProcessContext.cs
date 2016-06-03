using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetectApp.Core
{
    public interface IProcessContext
    {
        bool PhotoSelected { get; }
        bool HasResult { get; }
        bool IsWorking { get; }
        string PhotoPath { get; }
        object Result { get; }

        void Reset();
        bool SelectPhoto();
        Task<bool> ProcessPhoto(); 
    }
}
