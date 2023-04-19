using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn.Interface;

namespace Bergs.Pxc.Pxcwscxn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            using (TelaSocio telaSocio = new TelaSocio(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                telaSocio.Executar();
            }
        }
    }


}
