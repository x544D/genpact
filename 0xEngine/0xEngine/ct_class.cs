using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenGine
{
    class ct_class
    {


        public int ID { set; get; }
        public string Description { set; get; }
        public asm_class AssemblyScript { set; get; }


        public ct_class(int iD, string description, asm_class assemblyScript)
        {
            ID = iD;
            Description = description;
            AssemblyScript = assemblyScript ;
        }



    }
}
