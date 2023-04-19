using System;
using System.Collections.Generic;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcscoxn;
using NUnit.Framework;

namespace Bergs.Pxc.Pxcucoxn_TestaConta.Teste
{
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class TestaConta : AplicacaoTela
    {
        public TestaConta() :
            this(@"C:\Soft\pxc\data\Pxcz01da.mdb")
        {
        }
        /// <summary></summary>
        /// <param name="caminho"></param>
        private TestaConta(String caminho)
            : base(caminho)
        { }
        Bergs.Pxc.Pxcoiexn.RN.EscopoTransacional escopoCadaMetodo;

        /// <summary>Roda antes de tudo</summary>
        [OneTimeSetUp]
        public void BeforeAll()
        {
            //System.IO.File.Copy(@"C:\Soft\pxc\data\Pxcz01da_TESTACONTAmdb", @"C:\Soft\pxc\data\Pxcz01da.mdb", true);
        }

        /// <summary>Roda depois de tudo</summary>
        [OneTimeTearDown]
        public void AfterAll()
        {
        }

        /// <summary>Roda antes de cada teste</summary>
        [SetUp]
        public void BeforeEach()
        {
            this.escopoCadaMetodo = this.Infra.CriarEscopoTransacional();
        }

        /// <summary>Roda depois de cada teste</summary>
        [TearDown]
        public void AfterEach()
        {
            //realiza o roolback depois das alterações
            this.escopoCadaMetodo.Dispose();
        }

        [Test(Description = "\n========== Incluir =========")]
        public void IncluirTesteCamposObrigatorios()
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            Retorno<Int32> retIncluir = rnConta.Incluir(toConta);
            Assert.IsFalse(retIncluir.Ok, "Incluir - deveria retornar falha de campo obrigatório.");

            toConta.CodCliente = 0;
            retIncluir = rnConta.Incluir(toConta);
            Assert.IsFalse(retIncluir.Ok, "Incluir - deveria retornar falha de campo obrigatório.");

            toConta.Saldo = 0;
            retIncluir = rnConta.Incluir(toConta);
            Assert.IsFalse(retIncluir.Ok, "Incluir - deveria retornar falha de campo obrigatório.");

            toConta.TipoPessoa = "X";
            retIncluir = rnConta.Incluir(toConta);
            Assert.IsFalse(retIncluir.Ok, "Incluir - deveria retornar falha de campo obrigatório.");

            toConta.CodConta = 0;
            retIncluir = rnConta.Incluir(toConta);
            Assert.IsFalse(retIncluir.Ok, "Incluir - deveria retornar falha de campo obrigatório.");

            toConta.CodEspecie = 0;
            retIncluir = rnConta.Incluir(toConta);
            Assert.IsFalse(retIncluir.Ok, "Incluir - deveria retornar falha de campo obrigatório.");
        }

        [Test(Description = "\n========== IncluirComFalhaCPFInvalidoNaoExisteCliente =========")]
        public void IncluirComFalhaCPFInvalidoNaoExisteCliente()
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            //TODO: colocar aqui os dados para testar a inclusão
            toConta.CodAgencia = 100;
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            toConta.Limite = 200;
            toConta.Saldo = 0;
            toConta.CodCliente = 33333;
            toConta.TipoPessoa = "F";
            Retorno<Int32> retIncluir = rnConta.Incluir(toConta);
            Assert.IsFalse(retIncluir.Ok, "Incluir - erro CPF Inválido e incluiu, retornou: {0}", retIncluir.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== Incluir =========")]
        public void IncluirComSucesso()
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            //TODO: colocar aqui os dados para testar a inclusão
            toConta.CodAgencia = 100;
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            toConta.Limite = 200;
            toConta.Saldo = 0;
            toConta.CodCliente = 191;
            toConta.TipoPessoa = "F";
            Retorno<Int32> retIncluir = rnConta.Incluir(toConta);

            Assert.IsTrue(retIncluir.Ok, "Incluir - erro, retornou: {0}", retIncluir.Mensagem.ParaOperador);
            toConta = new TOConta();
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            toConta.CodAgencia = 100;
            Retorno<List<TOConta>> retListar = rnConta.Listar(toConta);
            if (retListar.Ok)
            {
                Assert.AreEqual(1, retListar.Dados.Count, "Deveria retornar somente 1 registro.");
                Assert.AreEqual(0, retListar.Dados[0].Saldo.LerConteudoOuPadrao(), "Saldo deveria ser zero");
                Assert.AreEqual(200, retListar.Dados[0].Limite.LerConteudoOuPadrao(), "Limite deveria ser 200");
            }
        }

        [Test(Description = "\n========== Alterar =========")]
        public void AlterarTesteCamposObrigatorios()
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            Retorno<Int32> retAlterar = rnConta.Alterar(toConta);
            Assert.IsFalse(retAlterar.Ok, "Alterar - deveria retornar falha de campo obrigatório.");

            toConta.CodConta = 0;
            retAlterar = rnConta.Alterar(toConta);
            Assert.IsFalse(retAlterar.Ok, "Alterar - deveria retornar falha de campo obrigatório.");

            toConta.CodEspecie = 0;
            retAlterar = rnConta.Alterar(toConta);
            Assert.IsFalse(retAlterar.Ok, "Alterar - deveria retornar falha de campo obrigatório.");
        }

        [Test(Description = "\n========== Alterar =========")]
        public void AlterarComSucesso()
        {
            IncluirComSucesso();

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            toConta.CodAgencia = 100;
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            toConta.Limite = 500;
            Retorno<Int32> retAlterar = rnConta.Alterar(toConta);

            Assert.IsTrue(retAlterar.Ok, "Alterar - erro, retornou: {0}", retAlterar.Mensagem.ParaOperador);
            toConta = new TOConta();
            toConta.CodAgencia = 100;
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            Retorno<List<TOConta>> retListar = rnConta.Listar(toConta);
            if (retListar.Ok)
            {
                Assert.AreEqual(1, retListar.Dados.Count, "Deveria retornar somente 1 registro.");
                //verificar demais campos
                Assert.AreEqual(500, retListar.Dados[0].Limite.LerConteudoOuPadrao(), "Deveria ter limite de 500");
            }
        }

        [Test(Description = "\n========== Alterar.1 =========\nAlterando Conta inexistente")]
        public void Alterar_1()
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            toConta = new TOConta();
            toConta.CodAgencia = -1;
            //toConta.CodCliente = ;
            toConta.CodConta = -1;
            toConta.CodEspecie = 35;
            toConta.Limite = new Pxcoiexn.BD.CampoTabela<double>(null);
            //toConta.Saldo = ;
            //toConta.TipoPessoa = ;
            Retorno<Int32> retAlterar = rnConta.Alterar(toConta);

            Assert.IsFalse(retAlterar.Ok, "Alterar - alterou, mas o Conta não existe.");
            Assert.AreEqual(new RegistroInexistenteMensagem().ToString(), retAlterar.Mensagem.ParaOperador,
                "Alterar - mensagem errada: retornou: \"{0}\"", retAlterar.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== Listar =========\nListar os dados da tabela")]
        public void Listar()
        {
            IncluirComSucesso();
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            toConta.CodAgencia = 100;
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            Retorno<List<TOConta>> retListar = rnConta.Listar(toConta);
            Assert.IsTrue(retListar.Ok, "Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);

            if (retListar.Dados.Count == 1)
            {
                Assert.AreEqual(1, retListar.Dados.Count, "Deveria retornar somente 1 registro.");
            }
        }

        [Test(Description = "\n========== Excluir =========")]
        public void ExcluirTesteCamposObrigatorios()
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            Retorno<Int32> retExcluir = rnConta.Excluir(toConta);
            Assert.IsFalse(retExcluir.Ok, "Excluir - deveria retornar falha de campo obrigatório.");

            toConta.CodConta = 0;
            retExcluir = rnConta.Excluir(toConta);
            Assert.IsFalse(retExcluir.Ok, "Excluir - deveria retornar falha de campo obrigatório.");

            toConta.CodEspecie = 0;
            retExcluir = rnConta.Excluir(toConta);
            Assert.IsFalse(retExcluir.Ok, "Excluir - deveria retornar falha de campo obrigatório.");
        }

        [Test(Description = "\n========== Excluir =========")]
        public void Excluir()
        {
            IncluirComSucesso();
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            toConta.CodAgencia = 100;
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            Retorno<Int32> retExcluir = rnConta.Excluir(toConta);

            Assert.IsTrue(retExcluir.Ok, "Excluir - erro, retornou: {0}", retExcluir.Mensagem.ParaOperador);
            toConta = new TOConta();
            toConta.CodAgencia = 100;
            toConta.CodConta = 100;
            toConta.CodEspecie = 35;
            Retorno<List<TOConta>> retListar = rnConta.Listar(toConta);
            Assert.IsTrue(retListar.Ok, "Listar - erro");
            Assert.AreEqual(0, retListar.Dados.Count, "Não deveria retornar nada, pois excluiu.");
        }
    }
}
