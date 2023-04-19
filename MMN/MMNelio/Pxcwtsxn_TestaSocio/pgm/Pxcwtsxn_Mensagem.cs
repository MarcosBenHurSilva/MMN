using System;
using System.Collections.Generic;
using System.Text;

namespace Bergs.Pxc.Pxcwtsxn
{
    class Mensagem
    {
        public String regra;
        public String mensagem;
        public Boolean ok;

        public Mensagem(String regra, String mensagem, Boolean ok)
        {
            this.regra = regra;
            this.mensagem = mensagem;
            this.ok = ok;
        }
    }
}
