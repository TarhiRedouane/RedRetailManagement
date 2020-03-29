CREATE PROCEDURE [dbo].[spGetProductById]
	@Id int
AS
begin
set nocount on;
SELECT Id,ProductName,[Description],RetailPrice,QuantityInStock,IsTaxable 
	from dbo.Product
	where Id = @Id;
end
