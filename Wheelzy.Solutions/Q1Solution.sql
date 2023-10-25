WITH CurrentStatus AS
         (SELECT OfferId, Max(Date) AS Date
          FROM [OfferStauts]
          GROUP BY OfferId)
SELECT o.Id as OfferId,
       o.Date as OfferDate,
       b.Name as CurrentBuyer,
       oa.Amount as CurrentAmoutn,
       os.Status as CurrentStatus
FROM [Offer] o
         JOIN OfferAmount oa ON o.OfferAmountId = oa.Id
         JOIN Buyer b ON oa.BuyerId = b.Id
         JOIN CurrentStatus cs ON o.Id = cs.OfferId
         JOIN OfferStatus os ON cs.Date = os.Date AND os.OfferId = cs.OfferId
WHERE o.Date BETWEEN @startDate AND @endDate