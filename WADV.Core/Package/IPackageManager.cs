using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WADV.Core.Package {
    public interface IPackageManager {
        string Name { get; }

        Stream Get(string path);
    }
}