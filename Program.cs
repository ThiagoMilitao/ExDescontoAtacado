List<Catalogo> produtos = new List<Catalogo>
{
    new Catalogo() { Gtin = 7891024110348, Descricao = "SABONETE OLEO DE ARGAN 90G PALMOLIVE", PrecoVarejo = 2.88, PrecoAtacado = 2.51, UnidadesAtacado = 12 },
    new Catalogo() { Gtin = 7891048038017, Descricao = "CHÁ DE CAMOMILA DR.OETKER", PrecoVarejo = 4.40, PrecoAtacado = 4.37, UnidadesAtacado = 3 },
    new Catalogo() { Gtin = 7896066334509, Descricao = "TORRADA TRADICIONAL WICKBOLD PACOTE 140G", PrecoVarejo = 5.19, PrecoAtacado = 0, UnidadesAtacado = 0 },
    new Catalogo() { Gtin = 7891700203142, Descricao = "BEBIDA À BASE DE SOJA MAÇÃ ADES CAIXA 200ML", PrecoVarejo = 2.39, PrecoAtacado = 2.38, UnidadesAtacado = 6 },
    new Catalogo() { Gtin = 7894321711263, Descricao = "ACHOCOLATADO PÓ ORIGINAL TODDY POTE 400G", PrecoVarejo = 9.79, PrecoAtacado = 0, UnidadesAtacado = 0 },
    new Catalogo() { Gtin = 7896001250611, Descricao = "ADOÇANTE LÍQUIDO SUCRALOSE LINEA CAIXA 25ML", PrecoVarejo = 9.89, PrecoAtacado = 9.10, UnidadesAtacado = 10 },
    new Catalogo() { Gtin = 7793306013029, Descricao = "CEREAL MATINAL CHOCOLATE KELLOGGS SUCRILHOS CAIXA 320G", PrecoVarejo = 12.79, PrecoAtacado = 12.35, UnidadesAtacado = 3 },
    new Catalogo() { Gtin = 7896004400914, Descricao = "COCO RALADO SOCOCO 50G", PrecoVarejo = 4.20, PrecoAtacado = 4.05, UnidadesAtacado = 6 },
    new Catalogo() { Gtin = 7898080640017, Descricao = "LEITE UHT INTEGRAL 1L COM TAMPA ITALAC", PrecoVarejo = 6.99, PrecoAtacado = 6.89, UnidadesAtacado = 12 },
    new Catalogo() { Gtin = 7891025301516, Descricao = "DANONINHO PETIT SUISSE COM POLPA DE MORANGO 360G DANONE", PrecoVarejo = 12.99, PrecoAtacado = 0, UnidadesAtacado = 0 },
    new Catalogo() { Gtin = 7891030003115, Descricao = "CREME DE LEITE LEVE 200G MOCOCA", PrecoVarejo = 3.12, PrecoAtacado = 3.09, UnidadesAtacado = 4 }
};

List<Venda> venda = new List<Venda>
{
    new Venda() { Sequencia = 1, Gtin = 7891048038017, Quantidade = 1, Parcial = 4.40 },
    new Venda() { Sequencia = 2, Gtin = 7896004400914, Quantidade = 4, Parcial = 16.80 },
    new Venda() { Sequencia = 3, Gtin = 7891030003115, Quantidade = 1, Parcial = 3.12 },
    new Venda() { Sequencia = 4, Gtin = 7891024110348, Quantidade = 6, Parcial = 17.28 },
    new Venda() { Sequencia = 5, Gtin = 7898080640017, Quantidade = 24, Parcial = 167.76 },
    new Venda() { Sequencia = 6, Gtin = 7896004400914, Quantidade = 8, Parcial = 33.60 },
    new Venda() { Sequencia = 7, Gtin = 7891700203142, Quantidade = 8, Parcial = 19.12 },
    new Venda() { Sequencia = 8, Gtin = 7891048038017, Quantidade = 1, Parcial = 4.40 },
    new Venda() { Sequencia = 9, Gtin = 7793306013029, Quantidade = 3, Parcial = 38.37 },
    new Venda() { Sequencia = 10, Gtin = 7896066334509, Quantidade = 2, Parcial = 10.38 }
};

List<DescontoProduto> descontoProdutos = new List<DescontoProduto> { };

double valorParcial = 0, subtotal = 0, valorTotal = 0, descontos = 0;

var vendasAgrupadas = venda
    .GroupBy(v => v.Gtin)
    .Select(g => new
    {
        Gtin = g.Key,
        Quantidade = g.Sum(v => v.Quantidade)
    });



foreach (var itemVenda in vendasAgrupadas)
{
    Catalogo produto = produtos.Find(p => p.Gtin == itemVenda.Gtin);



    if (produto != null)
    {
        if (produto.PrecoAtacado > 0 && itemVenda.Quantidade >= produto.UnidadesAtacado)
        {
            valorTotal += produto.PrecoAtacado * itemVenda.Quantidade;
            descontoProdutos.Add(new DescontoProduto { Gtin = produto.Gtin, ValorDesconto = (produto.PrecoVarejo * itemVenda.Quantidade) - (produto.PrecoAtacado * itemVenda.Quantidade) });
            descontos += (produto.PrecoVarejo * itemVenda.Quantidade) - (produto.PrecoAtacado * itemVenda.Quantidade);
        }
        else
        {
            valorTotal += produto.PrecoVarejo * itemVenda.Quantidade;
        }
        subtotal += produto.PrecoVarejo * itemVenda.Quantidade;
    }

    else
    {
        Console.WriteLine($"Produto com GTIN {itemVenda.Gtin} não encontrado no catálogo.");
    }
}

Console.WriteLine($"--- Desconto no Atacado ---");

Console.WriteLine();

foreach (var prod in descontoProdutos)
{
    Console.WriteLine($"{prod.Gtin}   {prod.ValorDesconto:F2}");
}

Console.WriteLine();

Console.WriteLine($"(+) Subtotal  =  {subtotal:F2}");
Console.WriteLine($"(-) Descontos =   {descontos:F2}");
Console.WriteLine($"(=) Total =  {valorTotal:F2}");
