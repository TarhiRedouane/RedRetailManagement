CREATE PROCEDURE [dbo].[spSaleReport]
	
AS
begin 
	select s.SaleDate,s.SubTotal,s.Tax,s.Total,u.FirstName,u.LastName,u.EmailAdress from SaleTable s
	inner join [User] u 
	on s.CashierId = u.Id
end
RETURN 0
