using Bergs.Pxc.Pxcwcoxn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestaConta
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 150;
            using (MinhaTela minhaTela = new MinhaTela(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                minhaTela.Executar();
            }
        }
    }
}
