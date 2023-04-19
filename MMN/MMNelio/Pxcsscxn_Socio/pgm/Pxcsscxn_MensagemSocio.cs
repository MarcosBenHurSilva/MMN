using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcsscxn
{
    enum TipoMensagemSocio
    {
        /// <summary>Empresa deve ser pessoa jur�dica.</summary>
        EmpresaJuridica,

        /// <summary>CNPJ inv�lido.</summary>
        CnpjInvalido,

        /// <summary>Empresa com o CNPJ XX.XXX.XXX/XXXX-XX n�o est� cadastrada.</summary>
        EmpresaNaoCadastrada,

        /// <summary>S�cio deve ser pessoa f�sica.</summary>
        SocioFisico,

        /// <summary>CPF inv�lido.</summary>
        CpfInvalido,

        /// <summary>Cliente com o CPF XXX.XXX.XXX-XX n�o est� cadastrado.</summary>
        ClienteNaoCadastrado,

        /// <summary>S�cio j� possui XXX% de participa��o societ�ria.</summary>
        SocioJaCadastrado,

        /// <summary>Participa��o societ�ria deve ser maior que zero.</summary>
        ParticipacaoMaiorQueZero,

        /// <summary>O total de participa��o societ�ria n�o pode ultrapassar 100%.</summary>
        TotalParticipacaoCem,

        /// <summary>S�cio deve ter no m�nimo 21 anos.</summary>
        SocioMenor
    }

    class MensagemSocio : Mensagem
    {
        public MensagemSocio(TipoMensagemSocio tipoMensagemSocio, params string[] parametro)
        {
            Formatador f = new Formatador();
            switch (tipoMensagemSocio)
            {
                case TipoMensagemSocio.EmpresaJuridica:
                    this.mensagem = string.Format("Empresa deve ser pessoa jur�dica.");
                    break;
                case TipoMensagemSocio.CnpjInvalido:
                    this.mensagem = string.Format("CNPJ inv�lido.");
                    break;
                case TipoMensagemSocio.EmpresaNaoCadastrada:
                    this.mensagem = string.Format(f, "Empresa com o CNPJ {0:cnpj} n�o est� cadastrada.", Double.Parse(parametro[0]));
                    break;
                case TipoMensagemSocio.SocioFisico:
                    this.mensagem = string.Format("S�cio deve ser pessoa f�sica.");
                    break;
                case TipoMensagemSocio.CpfInvalido:
                    this.mensagem = string.Format("CPF inv�lido.");
                    break;
                case TipoMensagemSocio.ClienteNaoCadastrado:
                    this.mensagem = string.Format(f, "Cliente com o CPF {0:cpf} n�o est� cadastrado.", Double.Parse(parametro[0]));
                    break;
                case TipoMensagemSocio.SocioJaCadastrado:
                    this.mensagem = string.Format("S�cio j� possui {0:N}% de participa��o societ�ria.", parametro[0]);
                    break;
                case TipoMensagemSocio.ParticipacaoMaiorQueZero:
                    this.mensagem = string.Format("Participa��o societ�ria deve ser maior que zero.");
                    break;
                case TipoMensagemSocio.TotalParticipacaoCem:
                    this.mensagem = string.Format("O total de participa��o societ�ria n�o pode ultrapassar 100%.");
                    break;
                case TipoMensagemSocio.SocioMenor:
                    this.mensagem = string.Format("S�cio deve ter no m�nimo 21 anos.");
                    break;
                default:
                    break;
            }
        }
    }
}
