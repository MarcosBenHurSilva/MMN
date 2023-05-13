// pra RN05: tirei o parms.data_inicio e parms.data_fim do método incluir e salvar
// pra RN06: no método alterar coloquei a condição
// pra RN07: no método contratar coloquei a condição
// pra RN08: no método encerrar coloquei a condição

vf1 = '2.500,00'
vf2 = '1.000,00'

qtd1 = 12;
qtd2 = 1;
convertido2 = parseFloat(vf2.replace('.', '').replace(',', '.'));
taxa2 = Math.pow(1 - 0.011, qtd2);
valor2 = (convertido2 * taxa2) / qtd2;
document.write("Valor financiado 1000 e parcela 1: \n");
document.write(valor2);
document.write("<br>");
convertido = parseFloat(vf1.replace('.', '').replace(',', '.'));
taxa1 = Math.pow(1 - 0.011, qtd1);
valor1 = (convertido * taxa1) / qtd1;
val = +(valor1.toFixed(2))
document.write("Valor financiado 2500 e parcela 12: \n");
document.write(valor1);
document.write("<br>");
document.write("Valor arredondado do reultado acima: \n");
document.write(val);
================================================================================================================================================
$(document).ready(function () {
    // Calcula o valor da parcela quando o valor financiado ou quantidade de parcelas mudar
    $("#valorFinanciado, #quantidadeParcelas").on("change", function () {
        var valorFinanciado = parseFloat($("#valorFinanciado").val());
        var quantidadeParcelas = parseInt($("#quantidadeParcelas").val());
        var taxa = 0.011; // 1,1%
        var valorParcela = (valorFinanciado * Math.pow(1 - taxa, quantidadeParcelas)) / quantidadeParcelas;
        $("#valorParcela").text(valorParcela.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
    }); //aqui provavelmente vai ter que aplicar a máscara de {moeda}
    // Impede que o número do contrato seja inserido com espaços em branco ou caracteres inválidos
    // quando o usuário insere o número, ocorre a formatação. a regex impede o usuário de mandar valores != números
    $("#numeroContrato").on("input", function () {
        $(this).val($(this).val().replace(/\s+/g, "").replace(/[^0-9]/g, "").padStart(7, "0"));
    });

    $("#valorParcela").text("R$ 0,00"); //aqui é pra não aparecer aquele "NaN" no campo valor_parcela, que vai ficar se tu não selecionar a quantidade de parcelas mas informar o valor
});
====================================================================================================================================================
// coloquei placeholder="1,1% a.m." e troquei o mm-desabilitar por readyonly
// (não sei se ele não vai descontar igual, mas por enquanto vou deixar assim. Se sobrar tempo talvez eu procure outras maneiras)
===================================================================================================================================================
<!DOCTYPE html>
<html>
<head>
  <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

</head>
<body>
   <label>Numero Contrato</label>
  <input type="text" id="numeroContrato" maxlength="7">
</body>
<script>
      $('#numeroContrato').on('blur', function() {
        var valor = $(this).val();
        var zerosEsquerda = "0000000";
        var valorCompleto = (zerosEsquerda + valor).slice(-7);
        $(this).val(valorCompleto);
      });
  </script>
</html>
=====================================================================================================================================================
# Javascrpit-aula-2
calculo de taxa de desconto pra notas comerciais de restaurante por ex.
Serve para vc validar um valor de parcela corrigindo ela com um fator de juros, usando javascrpit:

$(document).ready(function () {
    // Calcula o valor da parcela quando o valor financiado ou quantidade de parcelas mudar
    $("#valorFinanciado, #quantidadeParcelas").on("change", function () {
        var valorFinanciado = parseFloat($("#valorFinanciado").val());
        var quantidadeParcelas = parseInt($("#quantidadeParcelas").val());
        var taxaFinanciamento = 0.011; // 1,1%
        var valorParcela = (valorFinanciado * Math.pow(1 - taxaFinanciamento, quantidadeParcelas)) / quantidadeParcelas;
        $("#valorParcela").text(valorParcela.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
    });
    //aqui provavelmente vai ter que aplicar a máscara de {moeda}
    // Impede que o número do contrato seja inserido com espaços em branco ou caracteres inválidos
    // quando o usuário insere o número, ocorre a formatação. a regex impede o usuário de mandar valores != números
    $("#numeroContrato").on("input", function () {
        $(this).val($(this).val().replace(/\s+/g, "").replace(/[^0-9]/g, "").padStart(7, "0"));
    });

    $("#valorParcela").text("R$ 0,00");
    //aqui é pra não aparecer aquele "NaN" no campo valor_parcela, que vai ficar se tu não selecionar a quantidade de parcelas mas informar o valor
});

_

completar com zeros

javascript
$('#iddoinput).onchange('#blur, function() {
        var valor = $(this).val();
        var zerosEsquerda = "0000000";
        var valorCompleto = (zerosEsquerda + valor).slice(-7);
        $(this).val(valorCompleto);
      });


javascript
$('#numeroContrato').on('blur', function() {
        var valor = $(this).val();
        var zerosEsquerda = "0000000";
        var valorCompleto = (zerosEsquerda + valor).slice(-7);
        $(this).val(valorCompleto);
      });
=================================================================================================================================================
# Javascrpit-aula-3
Como fazer a taxa aparecer no fundo do campo no js
Serve para você indicar num campo de inclusão, o conteudo que aparece no fundo. Vamos trabalhar com taxa nessa aula:
considere nn (nada nunca) : variavel do seu sistema.

Solução:
usar na sua div correspondente o placeholder="1,1% a.m."
e
troca o (nada nunca) nn-desabilitar por readyonly
(lembre de pegar as provas antigas da faculdade na xerox, para que possa achar os campos correspondentes!)
==================================================================================================================================================
aí, pra quem ainda não conseguiu exibir o cliente na tela de alterar, eu fiz assim:

no html:
<div>
    <label for="txtCadastroCodCliente">Cliente<\label>
    <select mm-value="{@cbo_cliente}" outros atributos >
        <option mm-value="{@cod_cliente},{@tipo_pessoa}" mm-html="{@cod_cliente} - {@nome_cliente}" outros atributos>
</div>

no js:

TratarRetornoBoxDadosObter: function (oJson) {
   ...
   cbo_cliente: oJson.Dados.registroUsuario.cod_cliente + ',' + oJson.Dados.registroUsuario.tipo_pessoa,
   ...
}
===============================================================================================================================
Chama Funcao on input (parecido com onclick) oPxxxxx.formatarpadstart7
{
S(#formfitro).val()padstart(7,'0')
}
======================================================================================================================================
Pra fazer o cálculo com o que tá na tela, pega pelo id e atribui a uma variável

var qtdeParcelas = $('#txtCadastroQtdeParcelas').val();

... Depois segue o baile com os cálculos
Pra mostrar isso na tela, tem uma função da Infra que se não me engano tu encontra na documentação na aba Útil. Eu não lembro exatamente qual é agora
Ah, e converte pra Float o que estiver nessa variável. Porque o Javascript vai pegar como String
var valorFinanciado = parseFloat($("#valorFinanciado").val()); //o valor financiado tu tem que converter pra Float pra fazer o cáluclo pq o JS pega tudo como String sempre
        var quantidadeParcelas = parseInt($("#quantidadeParcelas").val()); //qtdeParcelas é Int

massss
voltei pro note

var valorFinanciado = parseFloat($("#valorFinanciado").val()); //o valor financiado tu tem que converter pra Float pra fazer o cáluclo pq o JS pega tudo como String sempre
        var quantidadeParcelas = parseInt($("#quantidadeParcelas").val()); //qtdeParcelas é Int

massss


a gente tem máscara no valorFinanciado. pra tirar esse trem, tem que dar replace nos pontos (colocar vazio) e as vírgulas viram pontos. que aí o JS se acha pra fazer o cálculo
===================================================================================================================================================
Coisas que eu anotei

1) Na RN4, lembrar de fazer uma ação pro alterar (com Obter) e outra com Incluir

2) Função pra completar os zeros na filtro:
$('#iddoinput).onchange('#blur, function() {
        var valor = $(this).val();
        var zerosEsquerda = "0000000";
        var valorCompleto = (zerosEsquerda + valor).slice(-7);
        $(this).val(valorCompleto);
      });

3) padEnd pq são 0 a direita no que eu cahei que era PadSTart


4) Usar com funções .hide e .show


5) Usar isso pra fazer ficar o "1,1% a.m"

"coloquei placeholder="1,1% a.m." e troquei o mm-desabilitar por readyonly
(não sei se ele não vai descontar igual, mas por enquanto vou deixar assim. Se sobrar tempo talvez eu procure outras maneiras)"
=========================================================================================================================================
List<int> parcelas = new List<int>(){1, 12, 24, 48, 96};

if(!parcelas.Contains(oCre.Operacao.Atributo.LerConteudoouPadrao()){
       Falha;
} (editado)
============================================================================================================================================
$(document).ready(function () {
    var valorParcela = 0; // adiciona a variável global

    // Calcula o valor da parcela quando o valor financiado ou quantidade de parcelas mudar
    $("#valorFinanciado, #quantidadeParcelas").on("change", function () {
        var valorFinanciado = parseFloat($("#valorFinanciado").val());
        var quantidadeParcelas = parseInt($("#quantidadeParcelas").val());
        var taxa = 0.011; // 1,1%
        valorParcela = (valorFinanciado * Math.pow(1 - taxa, quantidadeParcelas)) / quantidadeParcelas; // atualiza a variável global
        $("#valorParcela").text(valorParcela.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
    }); //aqui provavelmente vai ter que aplicar a máscara de {moeda}

    // Impede que o número do contrato seja inserido com espaços em branco ou caracteres inválidos
    // quando o usuário insere o número, ocorre a formatação. a regex impede o usuário de mandar valores != números
    $("#numeroContrato").on("input", function () {
        $(this).val($(this).val().replace(/\s+/g, "").replace(/[^0-9]/g, "").padStart(7, "0"));
    });

    $("#valorParcela").text("R$ 0,00"); //aqui é pra não aparecer aquele "NaN" no campo valor_parcela, que vai ficar se tu não selecionar a quantidade de parcelas mas informar o valor
});

function clicarIncluir() {
    if (!oInfra.getUtil().validarFormulario('#formCadastro')) {
        return false;
    }

    var parms = oInfra.getTela().getParametros().oPares;
    parms.tipo_pessoa = $('input[name=txtCadastroTipoPessoa]:checked').val();
    parms.cod_cliente = $('#txtCadastroCodCliente').val().trim();
    parms.nome_cliente = $('#txtCadastroNomeCliente').val().trim();
    parms.agencia = $('#txtCadastroAgencia').val().trim();
    <!-------------------------------------------------------------------------------------------------------------------!>
    parms.valor = $('#valorFinanciado').val().trim();
    parms.qtde_parcelas = $('#quantidadeParcelas').val().trim();
    <!-------------------------------------------------------------------------------------------------------------------!>
    parms.dt_abe_cad = $('#txtCadastroDtAbeCad').val().trim();
    parms.nome_mae = $('#txtCadastroNomeMae').val().trim();
    parms.nome_fantasia = $('#txtCadastroNomeFantasia').val().trim();
    parms.vlr_capital_social = $('#txtCadastroVlrCapitalSocial').val().trim();
    parms.dt_constituicao = $('#txtCadastroDtConstituicao').val().trim();
    parms.cod_operador = $('#txtCadastroCodOperador').val().trim();
    // usa a variável global valorParcela
    parms.valor_parcela = valorParcela.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    oInfra.getServidor().invocarServico('Pxcwclxn_ClientePxc.asmx/Incluir', {parametros: parms, retorno: oPxcsclhn.tratarSalvar});
====================================================================================================================================================
clicarIncluir: function () {
    if (!oInfra.getUtil().validarFormulario('#formCadastro')) {
      return false;
    }

    var parms = oInfra.getTela().getParametros().oPares;
    parms.tipo_pessoa = $('input[name=txtCadastroTipoPessoa]:checked').val();
    parms.cod_cliente = $('#txtCadastroCodCliente').val().trim();
    parms.nome_cliente = $('#txtCadastroNomeCliente').val().trim();
    parms.agencia = $('#txtCadastroAgencia').val().trim();
<!-------------------------------------------------------------------------------------------------------------------!>
    parms.valor = $('#valorFinanciado').val().trim();
    parms.qtde_parcelas = $('#quantidadeParcelas').val().trim();
    var valorFinanciado = parseFloat(parms.valor);
    var quantidadeParcelas = parseInt(parms.qtde_parcelas);
    var taxa = 0.011; // 1,1%
    var valorParcela = (valorFinanciado * Math.pow(1 - taxa, quantidadeParcelas)) / quantidadeParcelas;
    parms.valor_parcela = valorParcela.toFixed(2);
<!---------------------------------------------------------------------------------------------------------------------!>
    parms.dt_abe_cad = $('#txtCadastroDtAbeCad').val().trim();
    parms.nome_mae = $('#txtCadastroNomeMae').val().trim();
    parms.nome_fantasia = $('#txtCadastroNomeFantasia').val().trim();
    parms.vlr_capital_social = $('#txtCadastroVlrCapitalSocial').val().trim();
    parms.dt_constituicao = $('#txtCadastroDtConstituicao').val().trim();
    parms.cod_operador = $('#txtCadastroCodOperador').val().trim();
    oInfra.getServidor().invocarServico('Pxcwclxn_ClientePxc.asmx/Incluir', { parametros: parms, retorno: oPxcsclhn.tratarSalvar });
    // salvou produto
  }
