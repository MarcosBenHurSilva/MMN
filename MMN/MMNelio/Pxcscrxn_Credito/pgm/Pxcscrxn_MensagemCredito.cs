using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcscrxn
{
    /// <summary>Tipos de falha para a RN de Credito</summary>
    public enum TipoFalha
    {
        /// <summary>Falha de campo inválido</summary>
        CampoInvalido
        //TODO: incluir demais erros previstos
    }

    /// <summary>Classe de mensagens para a RN de Credito</summary>
    public class MensagemCredito : Mensagem
    {
        /// <summary>Cria uma mensagem para o usuário final</summary>
        /// <param name="tipoFalha">Tipo da falha</param>
        /// <param name="parametro">Parâmetros para a mensagem</param>
        public MensagemCredito(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.CampoInvalido:
                    this.mensagem = string.Format("Campo {0} inválido.", parametro[0]);
                    break;
                default:
                    break;
            }
        }
    }
}
