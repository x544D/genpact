using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenGine
{
    abstract class asm_class
    {


        private string ModuleName;
        private IntPtr Offset;

        public byte[]   OriginalBytes       { get; }
        public byte[]   FakeBytes           { set; get; } // option to change the directly from program
        public int      AllocSize           { get; }

        
        public string GetAddressAsString()
        {
            return ModuleName == null ? Offset.ToString() : ModuleName + "+" + Offset;
        }

        public asm_class( IntPtr offset, byte[] originalBytes, byte[] fakeBytes , int allocSize , string moduleName = null)
        {
            ModuleName      = moduleName;
            Offset          = offset;
            OriginalBytes   = originalBytes;
            FakeBytes       = fakeBytes;
            AllocSize       = allocSize;
        }


        public abstract void ActivateCheat();
        public abstract void Deactivate();



    }


    

}
