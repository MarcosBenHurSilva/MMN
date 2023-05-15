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
    public class OperacaoCredTestesIncluir : OperacaoCredTestes
	{
        /// <summary> RN de Clientes e OperacaoCred </summary>
        private ClientePxc rnCliente;

        private OperacaoCred rnOperacaoCred;

        private const String CPF_CLIENTE = "85130069048";

        private const String CPF_OperacaoCred_FISICO_EX = "95263065035";

        private const String CNPJ_CLIENTE = "07371226000148";

        private const String CNPJ_OperacaoCred_JURIDICO_EX = "72234233000160";

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
        public void Incluir_Falha_ClienteNull()
        {
            TOOperacaoCred toOperacaoCred = null;

            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCred);
            MMAssert.IsFalse(retorno.OK);
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_ClientePessoaFisica_Inexistente()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCredFisico = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO_EX;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 100000;
            toOperacaoCredFisico.QtdeParcelas = 1;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTFS, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);

            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF011", "Cliente não está cadastrado.");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_ClientePessoaJuridica_Inexistente()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CNPJ_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Juridica;

            TOOperacaoCred toOperacaoCredJuridico = new TOOperacaoCred();
            //toOperacaoCredJuridico.NumContrato = 2;
            toOperacaoCredJuridico.CpfCnpjOperacaoCred = CNPJ_OperacaoCred_JURIDICO_EX;
            toOperacaoCredJuridico.TipoPessoa = TipoPessoa.Juridica;
            toOperacaoCredJuridico.ValorFinanciado = 2000000;
            toOperacaoCredJuridico.QtdeParcelas = 12;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorJ = toOperacaoCredJuridico.ValorFinanciado;
            int parcelasJ = toOperacaoCredJuridico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaJ = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTJ = (valorJ * (decimal)Math.Pow(1 - (double)taxaJ, parcelasJ)) / parcelasJ;
            decimal valorParcelaJ = Math.Round(valorParcelaTJ, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredJuridico.ValorParcela = valorParcelaJ;

            retornoIncluir = this.RN.Incluir(toOperacaoCredJuridicoS);
            MMAssert.Sucesso(retornoIncluir);

            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredJuridicoS);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF011", "Cliente não está cadastrado.");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_ValorFinanciadoNulo()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 0;
            toOperacaoCredFisico.QtdeParcelas = 12;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTF, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);
            
            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF012", "Valor financiado deve ser maior ou igual a R$1.000 (mil reais).");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_ValorFinanciadoBaixo1()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 90;
            toOperacaoCredFisico.QtdeParcelas = 12;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTF, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);
            
            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF012", "Valor financiado deve ser maior ou igual a R$1.000 (mil reais).");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_ValorFinanciadoBaixo2()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 999;
            toOperacaoCredFisico.QtdeParcelas = 12;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTF, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);
            
            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF012", "Valor financiado deve ser maior ou igual a R$1.000 (mil reais).");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_QtdeParcelas_Invalida_Nula()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 9000;
            toOperacaoCredFisico.QtdeParcelas = 0;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTF, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);
            
            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF013", "A quantidade de parcelas deve ser 1, 12, 24, 48 ou 96 apenas.");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_QtdeParcelas_Invalida_EntreValores()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 9000;
            toOperacaoCredFisico.QtdeParcelas = 50;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTF, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);
            
            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF013", "A quantidade de parcelas deve ser 1, 12, 24, 48 ou 96 apenas.");
        }

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Falha_QtdeParcelas_Invalida_ValorAcima()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 9000;
            toOperacaoCredFisico.QtdeParcelas = 100;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTF, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);
            
            Retorno<Int32> retorno = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.FalhaComMensagem<Mensagem>(retorno, "RF013", "A quantidade de parcelas deve ser 1, 12, 24, 48 ou 96 apenas.");
        }        

        /// <summary>Teste</summary>
        [Test(Description = "Teste", Author = "B41660")]
        public void Incluir_Sucesso()
        {
            // TOClientePxc toCliente = new TOClientePxc();
            // toCliente.CodCliente = CPF_CLIENTE;
            // toCliente.TipoPessoa = TipoPessoa.Fisica;

            TOOperacaoCred toOperacaoCred = new TOOperacaoCred();
            //toOperacaoCredFisico.NumContrato = 1;
            toOperacaoCredFisico.CpfCnpjOperacaoCred = CPF_OperacaoCred_FISICO;
            toOperacaoCredFisico.TipoPessoa = TipoPessoa.Fisica;
            toOperacaoCredFisico.ValorFinanciado = 96000;
            toOperacaoCredFisico.QtdeParcelas = 96;
            //Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
            decimal valorF = toOperacaoCredFisico.ValorFinanciado;
            int parcelasF = toOperacaoCredFisico.QuantidadeParcelas;
            //Calcular o valor da taxa de juros mensal (1,1% am)
            decimal taxaF = 0.011m; //(0.011 = 1,1%)
            //Calcular o valor da parcela com base na fórmula fornecida
            decimal valorParcelaTF = (valorF * (decimal)Math.Pow(1 - (double)taxaF, parcelasF)) / parcelasF;
            decimal valorParcelaF = Math.Round(valorParcelaTF, 2);
            //Atribuir o valor da parcela ao objeto toOperacaoCred
            toOperacaoCredFisico.ValorParcela = valorParcelaF;

            Retorno<Int32> retornoIncluir = this.RN.Incluir(toOperacaoCredFisico);
            MMAssert.Sucesso(retornoIncluir);
        }
		#endregion
	}
}

