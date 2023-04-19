using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcsclxn;
using Bergs.Pxc.Pxcsscxn;

namespace Bergs.Pxc.Pxcwscxn
{
    class TelaSocio : AplicacaoTela
    {
        public TelaSocio(String caminho)
            : base(caminho)
        { }

        public void Executar()
        {
            try
            {
                Menu menu = new Menu(
                new ItemMenu[] {
                        new ItemMenu( new KeyValuePair<int,string>(1, "Incluir sócio"), Incluir, false),
                        new ItemMenu( new KeyValuePair<int,string>(2, "Alterar participação societária de um sócio"), Alterar, false),
                        new ItemMenu( new KeyValuePair<int,string>(3, "Excluir sócio"), Excluir, false),
                        new ItemMenu( new KeyValuePair<int,string>(4, "Listar sócios e empresas"), Listar, false),
                        new ItemMenu( new KeyValuePair<int,string>(0, "Sair"), null, true)
                        }, null);
                Tela.ControlaMenu("Banrisul Sócios", menu);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}\nTecle algo...", e.Message);
                Console.ReadKey();
            }
        }

        #region Itens do menu
        void Incluir(object obj)
        {
            TOCliente toCliente = new TOCliente();
            toCliente.TipoPessoa = "J";
            if (Tela.Confirma("Deseja consultar a empresa pelo CNPJ? "))
            {
                toCliente.CodCliente = Tela.Ler<Double>("Digite o CNPJ da empresa: ");
            }

            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            Retorno<List<TOCliente>> retListarClientes = rnCliente.Listar(toCliente);
            if (!retListarClientes.Ok)
            {
                Console.WriteLine("Erro: " + retListarClientes.Mensagem);
            }
            else
            {
                toCliente = SelecionarEmpresa("Selecione a empresa desejada: ", retListarClientes.Dados);

                if (toCliente != null)
                {
                    Formatador f = new Formatador();
                    Console.WriteLine(String.Format(f, "Empresa selecionada: {0:cnpj} - {1}",
                        toCliente.CodCliente.LerConteudoOuPadrao(),
                        toCliente.NomeCliente.LerConteudoOuPadrao()));

                    TOSocio toSocio = new TOSocio();
                    toSocio.CodClienteEmpresa = toCliente.CodCliente;
                    toSocio.TipoPessoaEmpresa = toCliente.TipoPessoa;
                    toSocio.CodClienteSocio = Tela.Ler<Double>("Digite o CPF do sócio: ");
                    toSocio.TipoPessoaSocio = "F";
                    toSocio.ParticipSocietaria = Tela.Ler<Double>("Digite a participação do sócio na empresa (%): ");

                    RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
                    Retorno<Int32> retIncluir = rnSocio.Incluir(toSocio);
                    Console.WriteLine(retIncluir.Mensagem);
                    return;
                }
            }
            Console.WriteLine("Operação cancelada.");
        }

        void Alterar(object obj)
        {
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = Tela.Ler<Double>("Digite o CNPJ da empresa: ");
            toSocio.TipoPessoaEmpresa = "J";

            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            Retorno<List<TOSocio>> retListarSocios = rnSocio.Listar(toSocio);
            if (!retListarSocios.Ok)
            {
                Console.WriteLine("Erro: " + retListarSocios.Mensagem);
            }
            else
            {
                toSocio = SelecionarSocio("Selecione o sócio desejado: ", retListarSocios.Dados);

                if (toSocio != null)
                {
                    Formatador f = new Formatador();
                    Console.WriteLine(String.Format(f, "Sócio selecionado: {0:cpf} - {1} - Participação: {2:N}%",
                        toSocio.CodClienteSocio.LerConteudoOuPadrao(),
                        toSocio.NomeClienteSocio,
                        toSocio.ParticipSocietaria));

                    toSocio.ParticipSocietaria = Tela.Ler<Double>("Digite a nova participação do sócio: ");

                    Retorno<Int32> retAlterar = rnSocio.Alterar(toSocio);
                    Console.WriteLine(retAlterar.Mensagem);
                    return;
                }
            }
            Console.WriteLine("Operação cancelada.");
        }

        void Excluir(object obj)
        {
            TOSocio toSocio = new TOSocio();
            toSocio.CodClienteEmpresa = Tela.Ler<Double>("Digite o CNPJ da empresa: ");
            toSocio.TipoPessoaEmpresa = "J";

            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            Retorno<List<TOSocio>> retListarSocios = rnSocio.Listar(toSocio);
            if (!retListarSocios.Ok)
            {
                Console.WriteLine("Erro: " + retListarSocios.Mensagem);
            }
            else
            {
                toSocio = SelecionarSocio("Selecione o sócio a ser excluído: ", retListarSocios.Dados);

                if (toSocio != null)
                {
                    Formatador f = new Formatador();
                    if (Tela.Confirma(String.Format(f, "Deseja excluir o sócio {1}, CPF {0:cpf}? ",
                            toSocio.CodClienteSocio.LerConteudoOuPadrao(),
                            toSocio.NomeClienteSocio)))
                    {
                        Retorno<Int32> retExcluir = rnSocio.Excluir(toSocio);
                        Console.WriteLine(retExcluir.Mensagem);
                        return;
                    }
                }
            }
            Console.WriteLine("Operação cancelada.");
        }

        void Listar(object obj)
        {
            TOSocio toSocio = new TOSocio();
            toSocio.TipoPessoaEmpresa = "J";
            toSocio.TipoPessoaSocio = "F";
            if (Tela.Confirma("Deseja consultar os sócios pelo CPF? "))
            {
                toSocio.CodClienteSocio = Tela.Ler<Double>("Digite o CPF do sócio: ");
            }

            RNSocio rnSocio = this.Infra.InstanciarRN<RNSocio>();
            Retorno<List<TOSocio>> retListarSocios = rnSocio.Listar(toSocio);
            if (!retListarSocios.Ok)
            {
                Console.WriteLine("Erro: " + retListarSocios.Mensagem);
            }
            else
            {
                ListarSocios("Sócios: ", retListarSocios.Dados);
            }
        }
        #endregion

        #region Métodos auxiliares de listas
        TOCliente SelecionarEmpresa(String titulo, List<TOCliente> listaEmpresas)
        {
            CabecalhoLista[] cabecalho = new CabecalhoLista[2];
            cabecalho[0] = new CabecalhoLista("CNPJ", CabecalhoLista.AlinhamentoCelula.Centralizado);
            cabecalho[1] = new CabecalhoLista("Nome da Empresa", CabecalhoLista.AlinhamentoCelula.Esquerda);
            
            List<LinhaLista> registros = new List<LinhaLista>();
            Formatador f = new Formatador();
            foreach (TOCliente empresa in listaEmpresas)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(String.Format(f, "{0:cnpj}", empresa.CodCliente.LerConteudoOuPadrao()));
                linha.Celulas.Add(empresa.NomeCliente.LerConteudoOuPadrao());
                registros.Add(linha);
            }

            if (listaEmpresas.Count > 0)
            {
                int itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros, 15);
                if (itemSelecionado != -1)
                {
                    return listaEmpresas[itemSelecionado];
                }
            }
            else
            {
                Console.WriteLine("Nenhuma empresa encontrada.");
            }

            return null;
        }

        TOSocio SelecionarSocio(String titulo, List<TOSocio> listaSocios)
        {
            CabecalhoLista[] cabecalho = new CabecalhoLista[3];
            cabecalho[0] = new CabecalhoLista("CPF", CabecalhoLista.AlinhamentoCelula.Centralizado);
            cabecalho[1] = new CabecalhoLista("Nome", CabecalhoLista.AlinhamentoCelula.Esquerda);
            cabecalho[2] = new CabecalhoLista("Participação (%)", CabecalhoLista.AlinhamentoCelula.Centralizado);

            List<LinhaLista> registros = new List<LinhaLista>();
            Formatador f = new Formatador();
            foreach (TOSocio socio in listaSocios)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(String.Format(f, "{0:cpf}", socio.CodClienteSocio.LerConteudoOuPadrao()));
                linha.Celulas.Add(socio.NomeClienteSocio.LerConteudoOuPadrao());
                linha.Celulas.Add(socio.ParticipSocietaria.LerConteudoOuPadrao().ToString("N"));
                registros.Add(linha);
            }

            if (listaSocios.Count > 0)
            {
                int itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros, 15);
                if (itemSelecionado != -1)
                {
                    return listaSocios[itemSelecionado];
                }
            }
            else
            {
                Console.WriteLine("Nenhum sócio encontrado.");
            }

            return null;
        }

        void ListarSocios(String titulo, List<TOSocio> listaSocios)
        {
            CabecalhoLista[] cabecalho = new CabecalhoLista[5];
            cabecalho[0] = new CabecalhoLista("CNPJ", CabecalhoLista.AlinhamentoCelula.Centralizado);
            cabecalho[1] = new CabecalhoLista("Nome da Empresa", CabecalhoLista.AlinhamentoCelula.Esquerda);
            cabecalho[2] = new CabecalhoLista("CPF", CabecalhoLista.AlinhamentoCelula.Centralizado);
            cabecalho[3] = new CabecalhoLista("Nome do Sócio", CabecalhoLista.AlinhamentoCelula.Esquerda);
            cabecalho[4] = new CabecalhoLista("Participação (%)", CabecalhoLista.AlinhamentoCelula.Centralizado);

            List<LinhaLista> registros = new List<LinhaLista>();
            Formatador f = new Formatador();
            foreach (TOSocio socio in listaSocios)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(String.Format(f, "{0:cnpj}", socio.CodClienteEmpresa.LerConteudoOuPadrao()));
                linha.Celulas.Add(socio.NomeClienteEmpresa.LerConteudoOuPadrao());
                linha.Celulas.Add(String.Format(f, "{0:cpf}", socio.CodClienteSocio.LerConteudoOuPadrao()));
                linha.Celulas.Add(socio.NomeClienteSocio.LerConteudoOuPadrao());
                linha.Celulas.Add(socio.ParticipSocietaria.LerConteudoOuPadrao().ToString("N"));
                registros.Add(linha);
            }

            if (listaSocios.Count > 0)
            {
                Tela.ImprimeLista(titulo, cabecalho, registros, 15);
            }
            else
            {
                Console.WriteLine("Nenhum sócio encontrado.");
            }
        }
        #endregion
    }
}
