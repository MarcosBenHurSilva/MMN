using Bergs.Bth.Bthsmoxn;
using Bergs.Bth.Bthstixn;
using Bergs.Bth.Bthstixn.MM4;
using Bergs.Pwx.Pwxoiexn;
using Bergs.Pxc.Pxcbtoxn;
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
	public class OperacaoCredTestes : AbstractTesteRegraNegocio<OperacaoCred>
	{
		#region Métodos de preparação dos testes
		///  <summary>
		/// Executa uma ação UMA vez por classe, ANTES do início da execução dos métodos de teste.
		/// </summary>
		protected override void BeforeAll()
		{
		}
		///  <summary>
		/// Executa uma ação ANTES de cada método de teste da classe.
		/// </summary>
		protected override void BeforeEach()
		{
		}
		///  <summary>
		/// Executa uma ação UMA vez por classe, DEPOIS do término da execução dos métodos de teste.
		/// </summary>
		protected override void AfterAll()
		{
		}
		///  <summary>
		/// Executa uma ação DEPOIS de cada método de teste da classe.
		/// </summary>
		protected override void AfterEach()
		{
		}
		///  <summary>
		/// Método para setar os dados necessários para conexão com o PHA no servidor de build.
		/// </summary>
		/// <returns>TO com dados necessários para conexão no servidor de build.</returns>
		protected override TOPhaServidorBuild SetarDadosServidorBuild()
		{
			return new TOPhaServidorBuild("GALUNO", "TREINAMENTO MM5");
		}
		#endregion
        #region Métodos de teste de sucesso.
        
		#endregion
	}
}

