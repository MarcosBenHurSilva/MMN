using Bergs.Bth.Bthsmoxn;
using Bergs.Bth.Bthstixn;
using Bergs.Bth.Bthstixn.MM4;
using Bergs.Pwx.Pwxoiexn;
using Bergs.Pwx.Pwxoiexn.Mensagens;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcsclxn;
using Bergs.Pxc.Pxcsocxn;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bergs.Pxc.Pxcuocxn.Testes
{
	
	///  <summary>
	/// Contém os métodos de teste da classe OperacaoCred.
	/// </summary>
	[TestFixture(Description="Classe de testes para a classe RN OperacaoCred.", Author="B41660")]
    public class OperacaoCredTestesExcluir : OperacaoCredTestes
	{
        /// <summary> RN de Clientes e OperacaoCred </summary>
        private ClientePxc rnCliente;

        private OperacaoCred rnOperacaoCred;

        private const String CPF_CLIENTE = "85130069048";

        private const String CNPJ_CLIENTE = "07371226000148";

        private const String CPF_OperacaoCred_FISICO = "85130069048";

        private const String CNPJ_OperacaoCred_JURIDICO = "07371226000148";

		#region Métodos de preparação dos testes
		///  <summary>
		/// Executa uma ação UMA vez por classe, ANTES do início da execução dos métodos de teste.
		/// </summary>
		protected override void BeforeAll()
		{
            this.rnCliente = this.Infra.InstanciarRN<ClientePxc>();
            this.rnOperacaoCred = this.Infra.InstanciarRN<OperacaoCred>();
		}

		///  <summary>
		/// Executa uma ação ANTES de cada método de teste da classe.
		/// </summary>
		protected override void BeforeEach()
		{
            TOClientePxc toClientePxcF = new TOClientePxc();           
            toClientePxcF.CodCliente = CPF_CLIENTE;
            toClientePxcF.TipoPessoa = TipoPessoa.Fisica;
            toClientePxcF.NomeCliente = "Fangles Tangles Company";
            toClientePxcF.Agencia = 1001;
            toClientePxcF.nomeCliente = "Testilson Testeveira";
            toClientePxcF.nomeMae = "Dona Teste";
            
            Retorno<Int32> retornoIncluir = this.rnCliente.Incluir(toClientePxcF);
            MMAssert.Sucesso(retornoIncluir);
            
            TOClientePxc toClientePxcJ = new TOClientePxc();
            toClientePxcJ.CodCliente = CNPJ_CLIENTE;
            toClientePxcJ.TipoPessoa = TipoPessoa.Juridica;
            toClientePxcJ.NomeCliente = "Fangles Tangles Company";
            toClientePxcJ.Agencia = 1001;
            toClientePxcJ.NomeFantasia = "Magnésia Bisurada";
            toClientePxcJ.DtConstituicao = DateTime.Today.AddYears(-5).Date;
            toClientePxcJ.VlrCapitalSocial = 15000;
            toClientePxcJ.DtAbeCad = DateTime.Today.AddYears(-10).Date;
            
            Retorno<Int32> retornoIncluir = this.rnCliente.Incluir(toClientePxcJ);
            MMAssert.Sucesso(retornoIncluir);

            TOOperacaoCred toOperacaoCredFisicoS = new TOOperacaoCred();
            toOperacaoCredFisicoS.NumContrato = 1;
            toOperacaoCredFisicoS.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisicoS.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisicoS.ValorFinanciado = 100000;
            toOperacaoCredFisicoS.QtdeParcelas = 1;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorFS = toOperacaoCredFisicoS.ValorFinanciado;
            int parcelasFS = toOperacaoCredFisicoS.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaFS = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTFS = (valorFS * (decimal)Math.Pow(1 - (double)taxaFS, parcelasFS)) / parcelasFS;
            decimal valorParcelaFS = Math.Round(valorParcelaTFS, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisicoS.ValorParcela = valorParcelaFS;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisicoS);
            MMAssert.Sucesso(retornoIncluir);

            TOOperacaoCred toOperacaoCredJuridicoS = new TOOperacaoCred();
            toOperacaoCredJuridicoS.NumContrato = 2;
            toOperacaoCredJuridicoS.CpfCnpjOperacaoCred = CNPJ_OperacaoCred_Juridico;
            toOperacaoCredJuridicoS.TipoPessoa = TipoPessoa.Juridica;
            toOperacaoCredJuridicoS.ValorFinanciado = 2000000;
            toOperacaoCredJuridicoS.QtdeParcelas = 12;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorJS = toOperacaoCredJuridicoS.ValorFinanciado;
            int parcelasJS = toOperacaoCredJuridicoS.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaJS = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTJS = (valorJS * (decimal)Math.Pow(1 - (double)taxaJS, parcelasJS)) / parcelasJS;
            decimal valorParcelaJS = Math.Round(valorParcelaTJS, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredJuridico.ValorParcela = valorParcelaJS;

            retornoIncluir = this.RN.Incluir(toOperacaoCredJuridicoS);
            MMAssert.Sucesso(retornoIncluir);

            TOOperacaoCred toOperacaoCredFisicoC = new TOOperacaoCred();
            toOperacaoCredFisicoC.NumContrato = 3;
            toOperacaoCredFisicoC.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisicoC.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisicoC.ValorFinanciado = 2400000;
            toOperacaoCredFisicoC.QtdeParcelas = 24;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorFC = toOperacaoCredFisicoC.ValorFinanciado;
            int parcelasFC = toOperacaoCredFisicoC.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaFC = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTFC = (valorFC * (decimal)Math.Pow(1 - (double)taxaFC, parcelasFC)) / parcelasFC;
            decimal valorParcelaFC = Math.Round(valorParcelaTFC, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisicoC.ValorParcela = valorParcelaFC;
            toOperacaoCredFisicoC.DataInicio = DateTime.Today.AddDays(-5).Date;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisicoC);
            MMAssert.Sucesso(retornoIncluir);

            TOOperacaoCred toOperacaoCredJuridicoC = new TOOperacaoCred();
            toOperacaoCredJuridicoC.NumContrato = 4;
            toOperacaoCredJuridicoC.CpfCnpjOperacaoCred = CNPJ_OperacaoCred_Juridico;
            toOperacaoCredJuridicoC.TipoPessoa = TipoPessoa.Juridica;
            toOperacaoCredJuridicoC.ValorFinanciado = 6000000;
            toOperacaoCredJuridicoC.QtdeParcelas = 48;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorJC = toOperacaoCredJuridicoC.ValorFinanciado;
            int parcelasJC = toOperacaoCredJuridicoC.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaJC = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTJC = (valorJC * (decimal)Math.Pow(1 - (double)taxaJC, parcelasJC)) / parcelasJC;
            decimal valorParcelaJC = Math.Round(valorParcelaTJC, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredJuridicoC.ValorParcela = valorParcelaJC;
            toOperacaoCredJuridicoC.DataInicio = DateTime.Today.AddDays(-5).Date;

            retornoIncluir = this.RN.Incluir(toOperacaoCredJuridicoC);
            MMAssert.Sucesso(retornoIncluir);

            TOOperacaoCred toOperacaoCredFisicoE = new TOOperacaoCred();
            toOperacaoCredFisicoE.NumContrato = 5;
            toOperacaoCredFisicoE.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisicoE.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisicoE.ValorFinanciado = 9600000;
            toOperacaoCredFisicoE.QtdeParcelas = 96;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorFE = toOperacaoCredFisicoE.ValorFinanciado;
            int parcelasFE = toOperacaoCredFisicoE.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaFE = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTFE = (valorFE * (decimal)Math.Pow(1 - (double)taxaFE, parcelasFE)) / parcelasFE;
            decimal valorParcelaFE = Math.Round(valorParcelaTFE, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisicoE.ValorParcela = valorParcelaFE;
            toOperacaoCredFisicoE.DataInicio = DateTime.Today.AddDays(-5).Date;
            toOperacaoCredFisicoE.DataFim = DateTime.Today.AddDays(10).Date;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisicoE);
            MMAssert.Sucesso(retornoIncluir);

            TOOperacaoCred toOperacaoCredJuridicoE = new TOOperacaoCred();
            toOperacaoCredJuridicoE.NumContrato = 6;
            toOperacaoCredJuridicoE.CpfCnpjOperacaoCred = CNPJ_OperacaoCred_Juridico;
            toOperacaoCredJuridicoE.TipoPessoa = TipoPessoa.Juridica;
            toOperacaoCredJuridicoE.ValorFinanciado = 800000;
            toOperacaoCredJuridicoE.QtdeParcelas = 12;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorJE = toOperacaoCredJuridicoE.ValorFinanciado;
            int parcelasJE = toOperacaoCredJuridicoE.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaJE = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTJE = (valorJE * (decimal)Math.Pow(1 - (double)taxaJE, parcelasJE)) / parcelasJE;
            decimal valorParcelaJE = Math.Round(valorParcelaTJE, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredJuridicoE.ValorParcela = valorParcelaJE;
            toOperacaoCredJuridicoE.DataInicio = DateTime.Today.AddDays(-5).Date;
            toOperacaoCredJuridicoE.DataFim = DateTime.Today.AddDays(10).Date;

            retornoIncluir = this.RN.Incluir(toOperacaoCredJuridicoC);
            MMAssert.Sucesso(retornoIncluir);
		}
		
        ///  <summary>
		/// Executa uma ação DEPOIS de cada método de teste da classe.
		/// </summary>
		protected override void AfterEach()
		{
		}

		///  <summary>
		/// Executa uma ação UMA vez por classe, DEPOIS do término da execução dos métodos de teste.
		/// </summary>
		protected override void AfterAll()
		{
            this.rnCliente = null;
            this.rnOperacaoCred = null;
		}

		#endregion
        #region Métodos de teste de falha.

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Excluir_Falha_peracaoCredNull()
        {
            TOOperacaoCred toOperacaoCred = null;

            Retorno<Int32> retorno = this.RN.Excluir(toOperacaoCred);
            MMAssert.IsFalse(retorno.OK);
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Excluir_Falha_OperacaoCredFisicoInexistente()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            toOperacaoCred.NumContrato = 1;
            toOperacaoCred.UltAtualizacao = DateTime.Now;

            Retorno<Int32> retornoOperacaoCred = this.RN.Excluir(toOperacaoCred);
            MMAssert.FalhaComMensagem<Mensagem>(retornoOperacaoCred, String.Empty, "Registro alterado ou excluído por outro usuário.");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Excluir_Sucesso_OperacaoCredFisico()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            toOperacaoCred.NumContrato = 1;
            //toOperacaoCred.UltAtualizacao = DateTime.Now;

            Retorno<TOOperacaoCred> retornoOperacaoCred = this.RN.Obter(toOperacaoCred);
            MMAssert.Sucesso(retornoOperacaoCred);
            TOOperacaoCred toOperacaoCredObter = retornoOperacaoCred.Dados;

            toOperacaoCred = new TOOperacaoCred();
            toOperacaoCred.NumContrato = toOperacaoCredObter.NumContrato;
            toOperacaoCred.UltAtualizacao = toOperacaoCredObter.UltAtualizacao;

            Retorno<Int32> retorno = this.RN.Excluir(toOperacaoCred);
            MMAssert.Sucesso(retorno);
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Excluir_Sucesso_OperacaoCredJuridico()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CNPJ_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Juridica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            toOperacaoCred.NumContrato = 2;

            Retorno<TOOperacaoCred> retornoOperacaoCred = this.RN.Obter(toOperacaoCred);
            MMAssert.Sucesso(retornoOperacaoCred);
            TOOperacaoCred toOperacaoCredObter = retornoOperacaoCred.Dados;

            toOperacaoCred = new TOOperacaoCred();
            toOperacaoCred.NumContrato = toOperacaoCredObter.NumContrato;
            toOperacaoCred.UltAtualizacao = toOperacaoCredObter.UltAtualizacao;

            Retorno<Int32> retorno = this.RN.Excluir(toOperacaoCred);
            MMAssert.Sucesso(retorno);
        }
		#endregion
	}
}

