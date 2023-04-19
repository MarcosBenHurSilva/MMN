using System;
using System.Collections.Generic;
using System.Text;

namespace Bergs.Pxc.Pxcwtsxn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            System.IO.File.Copy(@"C:\soft\pxc\data\Pxcz01da_TESTES.mdb", @"C:\soft\pxc\data\Pxcz01da.mdb", true);
            using (MinhaTela telaSocio = new MinhaTela(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                telaSocio.Executar();
            }
        }
    }
}
