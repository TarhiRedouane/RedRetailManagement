CREATE PROCEDURE [dbo].[spInsertInventory]
	@ProductId int,
	@Quantity int ,
	@PurchasePrice money,
	@PurchaseDate datetime2
AS
begin
set nocount on;
	insert into Inventory(ProductId,Quantity,PurchasePrice,PurchaseDate)
	OUTPUT inserted.Id
	values(@ProductId,@Quantity,@PurchasePrice,@PurchaseDate)
end

