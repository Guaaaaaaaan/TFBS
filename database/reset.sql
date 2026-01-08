USE TFBS;
GO

BEGIN TRAN;

-- Child tables first
DELETE FROM tfbs.PartUsage;
DELETE FROM tfbs.MaintenanceDetail;
DELETE FROM tfbs.MaintenanceLog;

DELETE FROM tfbs.Trip;
DELETE FROM tfbs.Reservation;

COMMIT;
GO
