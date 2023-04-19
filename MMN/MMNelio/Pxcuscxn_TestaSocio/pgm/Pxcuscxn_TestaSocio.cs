using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcsscxn;
using NUnit.Framework;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcuscxn_TestaSocio.Teste
{
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class TestaSocio : AplicacaoTela
    {
        public TestaSocio() :
            this(@"C:\Soft\pxc\data\Pxcz01da.mdb")
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="caminho"></param>
        private TestaSocio(String caminho)
            : base(caminho)
        { }

        [Test(Description = "\n========== RN1 =========\nTipo de pessoa jurídica.")]
        public void RN1()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 191;
            toSocio.TipoPessoaEmpresa = "F";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);

            Assert.IsFalse(retIncluir.Ok, "RN1 - falhou, incluiu e não deveria.");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Empresa deve ser pessoa jurídica.",
                "RN1 - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== RN2 =========\n")]
        public void RN2()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 333;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN2 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "CNPJ inválido.",
                "RN2 - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== RN3 =========\n")]
        public void RN3()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 6000169;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN3 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Empresa com o CNPJ 00.000.006/0001-69 não está cadastrada.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== RN4 =========\n")]
        public void RN4()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "J";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN4 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Sócio deve ser pessoa física.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== RN5 =========\n")]
        public void RN5()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 555;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN5 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "CPF inválido.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== RN6 =========\n")]
        public void RN6()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 5555507;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 10;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN6 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Cliente com o CPF 000.055.555-07 não está cadastrado.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }
        [Test(Description = "\n========== RN7 =========\n")]
        public void RN7()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 3;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN7 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Sócio já possui 10,00% de participação societária.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }


        [Test(Description = "\n========== RN8 =========\n")]
        public void RN8()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 272;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 0;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN8 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Participação societária deve ser maior que zero.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }
        [Test(Description = "\n========== RN8 Alterar =========\n")]
        public void RN8Alterar()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 191;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 0;
            Retorno<Int32> retIncluir = rnSocio.Alterar(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN8 Alterar ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Participação societária deve ser maior que zero.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== RN9 =========\n")]
        public void RN9()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 110;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN9 ERRO - retornou OK - incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "O total de participação societária não pode ultrapassar 100%.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }
        [Test(Description = "\n========== RN9 Alterar =========\nAumentando a participação societária de 70% para 80%")]
        public void RN9Alterar()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 80;
            Retorno<Int32> retIncluir = rnSocio.Alterar(toSocio);
            Assert.IsTrue(retIncluir.Ok, "RN9 alt ERRO - Não permitiu");
        }
        [Test(Description = "\n========== RN9 Alterar =========\nAumentando a participação societária para 99%")]
        public void RN9Alterar2()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 99;
            Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);
            Assert.IsFalse(retAlterar.Ok, "RN9 alt ERRO - Não permitiu");
            Assert.AreEqual(retAlterar.Mensagem.ParaOperador, "O total de participação societária não pode ultrapassar 100%.",
                "RN - mensagem errada: retornou: \"{0}\"", retAlterar.Mensagem.ParaOperador);
        }
        [Test(Description = "\n========== RN9 Alterar =========\nDiminuindo a participação societária de 80% para 60%")]
        public void ValidaRN9AlterarParaMenos()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 4000170;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 353;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 60;
            Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);
            Assert.IsTrue(retAlterar.Ok, "RN9 alt ERRO - Não permitiu");
        }

        [Test(Description = "\n========== RN9 Alterar =========\nAumentando a participação societária de 40% para 60% com 3 sócios")]
        public void ValidaRN9AlterarParaMais()
        {
            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 01111111000138;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 11144416;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 60;
            Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);
            Assert.IsTrue(retAlterar.Ok, "RN9 alt ERRO - Não permitiu");
        }

        [Test(Description = "\n========== RN10 =========\n")]
        public void RN10()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();

            toCliente.CodCliente = 434;
            toCliente.TipoPessoa = "F";
            toCliente.DataNasc = DateTime.Today.AddYears(-21).AddDays(1);

            Retorno<Int32> retAlterar = rnCliente.Alterar(toCliente);
            Assert.IsTrue(retAlterar.Ok, "Não conseguiu alterar a data de nascimento do cliente.");

            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 434;
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 2;
            Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);

            Assert.IsFalse(retIncluir.Ok, "RN10 ERRO - Incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Sócio deve ter no mínimo 21 anos.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);

            toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = 1000136;
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.CodClienteSocio = 25887068; //sem data de nascimento cadastrada
            toSocio.TipoPessoaSocio = "F";
            toSocio.ParticipSocietaria = 2;
            retIncluir = rnSocio.Incluir(toSocio);
            Assert.IsFalse(retIncluir.Ok, "RN10 ERRO - Incluiu e não deveria");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Sócio deve ter no mínimo 21 anos.",
                "RN - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);
        }
        //[Test(Description = "\n========== Incluir =========")]
        //public void Incluir()
        //{
        //    RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
        //    TOSocio toSocio = new TOSocio();
        //    //TODO: colocar aqui os dados para testar a inclusão
        //    toSocio.CodClienteEmpresa = 2000180;
        //    Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
        //    Assert.IsFalse(retIncluir.Ok, "Incluiu e não deveria");
        //    Assert.AreEqual(retIncluir.Mensagem,new CampoObrigatorioMensagem());

        //    Assert.IsTrue(retIncluir.Ok, "Incluir - erro, retornou: {0}", retIncluir.Mensagem.ParaOperador);
        //    Assert.Pass("--- conferindo se incluiu na base ---");
        //    toSocio = new TOSocio();
        //    toSocio.TipoPessoaSocio = /* valor */;
        //    toSocio.CodClienteSocio = /* valor */;
        //    toSocio.TipoPessoaEmpresa = /* valor */;
        //    toSocio.CodClienteEmpresa = /* valor */;
        //    Retorno<List<TOSocio>> retListar = rnSocio.Listar(toSocio);
        //    if (retListar.Ok)
        //    {
        //        if (retListar.Dados.Count == 1)
        //        {
        //            if (retListar.Dados[0]./* campo1 */.LerConteudoOuPadrao() == /* valorX */)
        //            {
        //                Assert.Pass("Dados encontrados na base.");
        //            }
        //        }
        //    }
        //}

        //[Test(Description = "\n========== Alterar =========")]
        //public void Alterar()
        //{
        //    RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
        //    TOSocio toSocio = new TOSocio();
        //    //toSocio.CodClienteEmpresa = ;
        //    //toSocio.CodClienteSocio = ;
        //    //toSocio.ParticipSocietaria = ;
        //    //toSocio.TipoPessoaEmpresa = ;
        //    //toSocio.TipoPessoaSocio = ;
        //    Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);

        //    Assert.IsTrue(retAlterar.Ok, "Alterar - erro, retornou: {0}", retAlterar.Mensagem.ParaOperador);
        //    Assert.Pass("--- conferindo se alterou na base ---");
        //    toSocio = new TOSocio();
        //    toSocio.TipoPessoaSocio = /* valor */;
        //    toSocio.CodClienteSocio = /* valor */;
        //    toSocio.TipoPessoaEmpresa = /* valor */;
        //    toSocio.CodClienteEmpresa = /* valor */;
        //    Retorno<List<TOSocio>> retListar = rnSocio.Listar(toSocio);
        //    if (retListar.Ok)
        //    {
        //        if (retListar.Dados.Count == 1)
        //        {
        //            if (retListar.Dados[0]./* campo2 */.LerConteudoOuPadrao() == /* valor1 */)
        //            {
        //                Assert.Pass("Alterou a base para limite igual a /* valor1 */.");
        //            }
        //        }
        //    }
        //}

        //[Test(Description = "\n========== Alterar.1 =========\nAlterando Socio inexistente")]
        //public void Alterar_1()
        //{
        //    RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
        //    TOSocio toSocio = new TOSocio();
        //    toSocio = new TOSocio();
        //    //toSocio.CodClienteEmpresa = ;
        //    //toSocio.CodClienteSocio = ;
        //    //toSocio.ParticipSocietaria = ;
        //    //toSocio.TipoPessoaEmpresa = ;
        //    //toSocio.TipoPessoaSocio = ;
        //    Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);

        //    Assert.IsFalse(retAlterar.Ok, "Alterar - alterou, mas o Socio não existe.");
        //    Assert.AreEqual(retAlterar.Mensagem.ParaOperador, new RegistroInexistenteMensagem().ToString(),
        //        "Alterar - mensagem errada: retornou: \"{0}\"", retAlterar.Mensagem.ParaOperador);
        //}

        //[Test(Description = "\n========== Listar =========\nListar os dados da tabela")]
        //public void Listar()
        //{
        //    RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
        //    TOSocio toSocio = new TOSocio();
        //    //toSocio.CodClienteEmpresa = ;
        //    //toSocio.CodClienteSocio = ;
        //    //toSocio.ParticipSocietaria = ;
        //    //toSocio.TipoPessoaEmpresa = ;
        //    //toSocio.TipoPessoaSocio = ;
        //    Retorno<List<TOSocio>> retListar = rnSocio.Listar(toSocio);
        //    Assert.IsTrue(retListar.Ok, "Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);

        //    //mostrando os dados na tela
        //    foreach (TOSocio socio in retListar.Dados)
        //    {
        //        //Assert.Pass("{0} {1} - {2}-{3} R$ {4}",
        //        //    socio./*campo1*/, socio./*campo2*/, socio.NumCartao, socio.Bandeira, socio.Limite);
        //    }
        //    if (retListar.Dados.Count == 1)
        //    {
        //        toSocio = retListar.Dados[0];
        //        if (toSocio./*campo1*/.LerConteudoOuPadrao() != /* ? */)
        //        {
        //            Assert.Pass("mensagem");
        //        }
        //    }
        //    else if (retListar.Dados.Count == 0)
        //    {
        //        Assert.Pass("Erro, não tem nada na lista");
        //    }
        //    else
        //    {
        //        Assert.Pass("Erro, listou mais de 1");
        //    }
        //}

        //[Test(Description = "\n========== Excluir =========")]
        //public void Excluir()
        //{
        //    RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
        //    TOSocio toSocio = new TOSocio();
        //    toSocio.TipoPessoaSocio = /* valor */;
        //    toSocio.CodClienteSocio = /* valor */;
        //    toSocio.TipoPessoaEmpresa = /* valor */;
        //    toSocio.CodClienteEmpresa = /* valor */;
        //    Retorno<Int32> retExcluir = rnSocio.Excluir(toSocio);

        //    Assert.IsTrue(retExcluir.Ok, "Excluir - erro, retornou: {0}", retExcluir.Mensagem.ParaOperador);
        //    Assert.Pass("Conferindo a lista na base:");
        //    toSocio = new TOSocio();
        //    toSocio.TipoPessoaSocio = /* valor */;
        //    toSocio.CodClienteSocio = /* valor */;
        //    toSocio.TipoPessoaEmpresa = /* valor */;
        //    toSocio.CodClienteEmpresa = /* valor */;
        //    Retorno<List<TOSocio>> retListar = rnSocio.Listar(toSocio);
        //    if (!retListar.Ok)
        //    {
        //        Assert.Pass("Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);
        //    }
        //    else
        //    {
        //        if (retListar.Dados.Count > 0)
        //        {
        //            Assert.Pass("Encontrou o registro na base, ou seja, não apagou");
        //        }
        //        else
        //        {
        //            Assert.Pass("Não encontrou o registro na base, ou seja, a exclusão funcionou");
        //        }
        //    }
        //}
    }
}
