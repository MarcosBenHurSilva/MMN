using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcsscxn
{
    enum TipoMensagemSocio
    {
        /// <summary>Empresa deve ser pessoa jurídica.</summary>
        EmpresaJuridica,

        /// <summary>CNPJ inválido.</summary>
        CnpjInvalido,

        /// <summary>Empresa com o CNPJ XX.XXX.XXX/XXXX-XX não está cadastrada.</summary>
        EmpresaNaoCadastrada,

        /// <summary>Sócio deve ser pessoa física.</summary>
        SocioFisico,

        /// <summary>CPF inválido.</summary>
        CpfInvalido,

        /// <summary>Cliente com o CPF XXX.XXX.XXX-XX não está cadastrado.</summary>
        ClienteNaoCadastrado,

        /// <summary>Sócio já possui XXX% de participação societária.</summary>
        SocioJaCadastrado,

        /// <summary>Participação societária deve ser maior que zero.</summary>
        ParticipacaoMaiorQueZero,

        /// <summary>O total de participação societária não pode ultrapassar 100%.</summary>
        TotalParticipacaoCem,

        /// <summary>Sócio deve ter no mínimo 21 anos.</summary>
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
                    this.mensagem = string.Format("Empresa deve ser pessoa jurídica.");
                    break;
                case TipoMensagemSocio.CnpjInvalido:
                    this.mensagem = string.Format("CNPJ inválido.");
                    break;
                case TipoMensagemSocio.EmpresaNaoCadastrada:
                    this.mensagem = string.Format(f, "Empresa com o CNPJ {0:cnpj} não está cadastrada.", Double.Parse(parametro[0]));
                    break;
                case TipoMensagemSocio.SocioFisico:
                    this.mensagem = string.Format("Sócio deve ser pessoa física.");
                    break;
                case TipoMensagemSocio.CpfInvalido:
                    this.mensagem = string.Format("CPF inválido.");
                    break;
                case TipoMensagemSocio.ClienteNaoCadastrado:
                    this.mensagem = string.Format(f, "Cliente com o CPF {0:cpf} não está cadastrado.", Double.Parse(parametro[0]));
                    break;
                case TipoMensagemSocio.SocioJaCadastrado:
                    this.mensagem = string.Format("Sócio já possui {0:N}% de participação societária.", parametro[0]);
                    break;
                case TipoMensagemSocio.ParticipacaoMaiorQueZero:
                    this.mensagem = string.Format("Participação societária deve ser maior que zero.");
                    break;
                case TipoMensagemSocio.TotalParticipacaoCem:
                    this.mensagem = string.Format("O total de participação societária não pode ultrapassar 100%.");
                    break;
                case TipoMensagemSocio.SocioMenor:
                    this.mensagem = string.Format("Sócio deve ter no mínimo 21 anos.");
                    break;
                default:
                    break;
            }
        }
    }
}
