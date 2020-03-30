CREATE PROCEDURE [dbo].[SpGetInventories]

AS
begin
set nocount on;
	SELECT  [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	from Inventory
end
