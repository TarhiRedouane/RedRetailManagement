CREATE PROCEDURE [dbo].[spInsertSale]
@CashierId nvarchar(128),
@SaleDate datetime2,
@SubTotal money,
@Tax money,
@Total money

AS
begin
set nocount on;
	insert into SaleTable (CashierId,SaleDate,SubTotal,Tax,Total)
	OUTPUT inserted.Id
	values(@CashierId,@SaleDate,@SubTotal,@Tax,@Total)
end