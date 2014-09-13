using System;
using System.IO;
using System.Reflection;
using Castle.Core.Internal;

namespace ConsoleCalculator.Operations
{
    class OperatorLoader : IOperatorLoader
    {

        private const string OPERATOR_DIRECTORY = "Operators";

        public void Load()
        {
            var workingDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (workingDirectoryPath == null) return;

            var operatorPath = Path.Combine(workingDirectoryPath, OPERATOR_DIRECTORY);
            
            if (!Directory.Exists(operatorPath)) return;
            
            Directory.GetFiles(operatorPath, "*.dll")
                     .ForEach(fileName => Assembly.LoadFile(fileName));
            
        }
    }
}