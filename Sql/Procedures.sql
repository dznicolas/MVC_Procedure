create procedure cadastro
(
@Finalidade varchar(50),
@Valor decimal(18,2),
@DataCadastro datetime,
@ClienteId int
)
As
Begin 

insert into  Imovel values(@Finalidade, @Valor, @DataCadastro, @ClienteId)

End

GO 

create procedure alterar
(
@ImovelId int,
@Finalidade varchar(50),
@Valor decimal(18,2),
@DataCadastro datetime,
@ClienteId int
)
As
Begin 

update Imovel set 
Finalidade = @Finalidade, 
Valor = @Valor,
DataCadastro = @DataCadastro,
ClienteId = @ClienteId
where ImovelId = @ImovelId

End

GO

create procedure consultar
(
@ImovelId int
)
As
Begin 

select * from Imovel where ImovelId = @ImovelId

End

GO

create procedure consultarTodos

As
Begin 

select * from Imovel 

End

GO

create procedure excluir
(
@ImovelId int
)
As
Begin 

delete from Imovel where ImovelId = @ImovelId

End

GO

