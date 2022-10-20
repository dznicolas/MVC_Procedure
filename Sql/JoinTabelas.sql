SELECT 
Imovel.ImovelId,
Imovel.Finalidade,
Imovel.Valor,
Imovel.DataCadastro,
Imovel.ClienteId,
Cliente.NomeCliente
FROM Imovel
JOIN
Cliente ON Imovel.ClienteId=Cliente.ClienteId