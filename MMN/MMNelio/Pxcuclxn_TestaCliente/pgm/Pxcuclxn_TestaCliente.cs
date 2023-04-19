using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcsclxn;
using NUnit.Framework;

namespace Bergs.Pxc.Pxcuclxn_TestaCliente.Teste
{
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class TestaCliente : AplicacaoTela
    {
        public TestaCliente() :
            this(@"C:\soft\pxc\data\Pxcz01da.mdb")
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="caminho"></param>
        private TestaCliente(String caminho)
            : base(caminho)
        { }

        [Test(Description = "\n========== RNX =========\nBreve descrição do que faz a RNx.")]
        public void RNX()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();
            toCartao.Bandeira = "V";
            toCartao.CodCliente = 191;
            toCartao.Limite = 1;
            toCartao.NumCartao = 1;
            toCartao.TipoPessoa = "J";
            Retorno<Int32> retIncluir = rnCliente.Incluir(toCliente);

            Assert.IsFalse(retIncluir.Ok, "RN1 - falhou, incluiu e não deveria.");
            Assert.AreEqual(retIncluir.Mensagem.ParaOperador, "Só é permitido incluir Cliente para pessoas físicas.",
                "RN1 - mensagem errada: retornou: \"{0}\"", retIncluir.Mensagem.ParaOperador);

        }

        [Test(Description = "\n========== Incluir =========")]
        public void Incluir()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();
            //TODO: colocar aqui os dados para testar a inclusão
                toCliente.CodCliente = 0
                toCliente.DataAtuRating = DateTime.Parse("2022-04-20")
                toCliente.DataCadastro = DateTime.Parse("2022-04-20")
                toCliente.DataNasc = DateTime.Parse("2022-04-20")
                toCliente.NomeCliente = "X"
                toCliente.RatingCliente = "X"
                toCliente.RendaFamiliar = 0
                toCliente.Telefone = 0
                toCliente.TipoPessoa = "X"
            Retorno<Int32> retIncluir = rnCliente.Incluir(toCliente);

            Assert.IsTrue(retIncluir.Ok, "Incluir - erro, retornou: {0}", retIncluir.Mensagem.ParaOperador);
            Assert.Pass("--- conferindo se incluiu na base ---");
            toCliente = new TOCliente();
            toCliente./* campo1 */ = /* valorX */;
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toCliente);
            if (retListar.Ok)
            {
                if (retListar.Dados.Count == 1)
                {
                    if (retListar.Dados[0]./* campo1 */.LerConteudoOuPadrao() == /* valorX */)
                    {
                        Assert.Pass("Dados encontrados na base.");
                    }
                }
            }
        }

        [Test(Description = "\n========== Alterar =========")]
        public void Alterar()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();
            toCliente./* campo1 */ = /* valorX */;
            toCliente./* campo2 */ = /* valor1 */;
            Retorno<Int32> retAlterar = rnCliente.Alterar(toCliente);

            Assert.IsTrue(retAlterar.Ok, "Alterar - erro, retornou: {0}", retAlterar.Mensagem.ParaOperador);
            Assert.Pass("--- conferindo se alterou na base ---");
            toCliente = new TOCliente();
            toCliente./* campo1 */ = /* valorX */;
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toCliente);
            if (retListar.Ok)
            {
                if (retListar.Dados.Count == 1)
                {
                    if (retListar.Dados[0]./* campo2 */.LerConteudoOuPadrao() == /* valor1 */)
                    {
                        Assert.Pass("Alterou a base para limite igual a /* valor1 */.");
                    }
                }
            }
        }

        [Test(Description = "\n========== Alterar.1 =========\nAlterando Cliente inexistente")]
        public void Alterar_1()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();
            toCliente = new TOCliente();
            toCliente.Limite = 20;
            toCliente.NumCartao = 3331;
            Retorno<Int32> retAlterar = rnCliente.Alterar(toCliente);

            Assert.IsFalse(retAlterar.Ok, "Alterar - alterou, mas o Cliente não existe.");
            Assert.AreEqual(retAlterar.Mensagem.ParaOperador, new RegistroInexistenteMensagem().ToString(),
                "Alterar - mensagem errada: retornou: \"{0}\"", retAlterar.Mensagem.ParaOperador);
        }

        [Test(Description = "\n========== Listar =========\nListar os dados da tabela")]
        public void Listar()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();
            toCliente./* campo1 */ = /* valor1 */;
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toCliente);
            Assert.IsTrue(retListar.Ok, "Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);

            //mostrando os dados na tela
            foreach (TOCliente cliente in retListar.Dados)
            {
                //Assert.Pass("{0} {1} - {2}-{3} R$ {4}",
                //    cliente./*campo1*/, cliente./*campo2*/, cliente.NumCartao, cliente.Bandeira, cliente.Limite);
            }
            if (retListar.Dados.Count == 1)
            {
                toCliente = retListar.Dados[0];
                if (toCartao./*campo1*/.LerConteudoOuPadrao() != /* ? */)
                {
                    Assert.Pass("Número do cartão errado");
                }
            }
            else if (retListar.Dados.Count == 0)
            {
                Assert.Pass("Erro, não tem nada na lista");
            }
            else
            {
                Assert.Pass("Erro, listou mais de 1");
            }
        }

        [Test(Description = "\n========== Excluir =========")]
        public void Excluir()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toCliente = new TOCliente();
            toCliente.TipoPessoa = /* valor */;
            toCliente.CodCliente = /* valor */;
            Retorno<Int32> retExcluir = rnCliente.Excluir(toCliente);

            Assert.IsTrue(retExcluir.Ok, "Excluir - erro, retornou: {0}", retExcluir.Mensagem.ParaOperador);
            Assert.Pass("Conferindo a lista na base:");
            toCliente = new TOCliente();
            toCliente.TipoPessoa = /* valor */;
            toCliente.CodCliente = /* valor */;
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toCliente);
            if (!retListar.Ok)
            {
                Assert.Pass("Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);
            }
            else
            {
                if (retListar.Dados.Count > 0)
                {
                    Assert.Pass("Encontrou o registro na base, ou seja, não apagou");
                }
                else
                {
                    Assert.Pass("Não encontrou o registro na base, ou seja, a exclusão funcionou");
                }
            }
        }
    }
}
