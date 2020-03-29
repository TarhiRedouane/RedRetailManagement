CREATE PROCEDURE [dbo].[spInsertSaleDetail]
@SaleId int,
@ProductId int,
@Quantity int,
@PurchasePrice money,
@Tax money

AS
begin
set nocount on;
	insert into SaleDetail(SaleId,ProductId,Quantity,PurchasePrice,Tax)
	OUTPUT inserted.Id
	values(@SaleId,@ProductId,@Quantity,@PurchasePrice,@Tax)
end