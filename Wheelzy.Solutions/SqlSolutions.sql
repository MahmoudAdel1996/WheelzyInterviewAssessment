CREATE PROCEDURE GetInvoicesInDateRange
    @startDate DATE,
    @endDate DATE
AS
BEGIN
    WITH InvoiceInfo AS (
        SELECT
            I.InvoiceId,
            I.Date,
            COALESCE(C.CustomerName, 'No Customer') AS CustomerName,
            ISNULL(SUM(II.Quantity), 0) AS ItemsQuantity,
            ISNULL(MAX(II.Price), 0) AS MostExpensivePrice,
            ISNULL(MIN(II.Price), 0) AS CheapestPrice
        FROM Invoices I
                 LEFT JOIN Customers C ON I.CustomerId = C.CustomerId
                 LEFT JOIN InvoiceItems II ON I.InvoiceId = II.InvoiceId
        WHERE I.Date >= @startDate AND I.Date <= @endDate
        GROUP BY I.InvoiceId, I.Date, CustomerName
    )
    SELECT
        II.InvoiceId,
        II.Date,
        II.CustomerName,
        II.ItemsQuantity,
        (
            SELECT TOP 1 II2.ProductName
            FROM InvoiceItems II2
            WHERE II2.InvoiceId = II.InvoiceId
              AND II2.Price = II.MostExpensivePrice
        ) AS MostExpensiveItem,
        II.MostExpensivePrice,
        (
            SELECT TOP 1 II2.ProductName
            FROM InvoiceItems II2
            WHERE II2.InvoiceId = II.InvoiceId
              AND II2.Price = II.CheapestPrice
        ) AS CheapestItem,
        II.CheapestPrice,
        ISNULL((SELECT SUM(Total) FROM Invoices WHERE InvoiceId = II.InvoiceId), 0) AS Total
    FROM InvoiceInfo II;
END
