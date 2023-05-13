this.exibirOcultarCampo = exibirOcultarCampo;
function exibirOcultarCampo(idCampo, bExibe) {
    var sExibe = (bExibe) ? 'block' : 'none';
    document.getElementById(idCampo).style.display = sExibe;
}

this.exibirCamposTipoPessoa = exibirCamposTipoPessoa;
function exibirCamposTipoPessoa(ind_situacao) {
    var bExibe = (ind_situacao == '1') ? true : false;

    exibirOcultarCampo('trData_Inicio', !bExibe);
    exibirOcultarCampo('trData_Prevista_Fim', !bExibe);
    exibirOcultarCampo('trDataFim', !bExibe);
}
this.exibirCamposTipoPessoa = exibirCamposTipoPessoa;
function exibirCamposTipoPessoa(ind_situacao) {
    var bExibe = (ind_situacao == '2') ? true : false;

    exibirOcultarCampo('trData_Inicio', bExibe);
    exibirOcultarCampo('trData_Prevista_Fim', bExibe);
    exibirOcultarCampo('trDataFim', !bExibe);
}

this.exibirCamposTipoPessoa = exibirCamposTipoPessoa;
function exibirCamposTipoPessoa(ind_situacao) {
    var bExibe = (ind_situacao == '3') ? true : false;

    exibirOcultarCampo('trData_Inicio', !bExibe);
    exibirOcultarCampo('trData_Prevista_Fim', !bExibe);
    exibirOcultarCampo('trDataFim', bExibe);
}


ExibirBotoes: function () {
    // só exibe botão 'Contratar' e 'SALVAR' se a situação da operação é SOLICITADA
    if ($('#txtCadastroIndSituacao').val() == '1') {
        $('#btnContratar').show();
        $('#btnSalvar').show();
    }
    else {
        $('#btnContratar').hide();
        $('#btnSalvar').hide();
    }
    // só exibe botão 'ENCERRAR' se a situação da operação é CONTRATADA
    if ($('#txtCadastroIndSituacao').val() == '2') {
        $('#btnOutro').show();
    }
    else {
        $('#btnOutro').hide();
    }

}

ExibirBotoes: function () {
    if ($('#txtCadastroIndSituacao').val() == '1') {
        $('#btnContratar').show();
        $('#btnOutro').hide();
    }
    else if ($('#txtCadastroIndSituacao').val() == '2') {
        $('#btnContratar').hide();
        $('#btnOutro').show();
    }
    else  // caso contrário, esconde e limpa valor
    {
        $('#btnContratar').hide();
        $('#btnOutro').hide();
    }
}