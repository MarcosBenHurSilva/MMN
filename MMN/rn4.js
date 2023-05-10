// Na inclusão/alteração, o valor da parcela deve ser [VALOR_FINANCIADO*(1-TAXA)^PARCELAS]/PARCELAS.
// Onde:
// P = Quantidade de Parcelas
// VF = Valor Financiado
// TAXA = 1,1% am (lembre-se de que a taxa na fórmula NÃO deve ser utilizada percentual)

function calcularValorParcela(valorFinanciado, quantidadeParcelas) {
    const taxa = 0.011; // 1,1% ao mês

    const valorParcela = (valorFinanciado * Math.pow(1 - taxa, quantidadeParcelas)) / quantidadeParcelas;

    return valorParcela;
}
