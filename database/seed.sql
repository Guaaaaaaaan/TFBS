USE TFBS;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRAN;

------------------------------------------------------------
-- 1) Department
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.Department WHERE DeptName = 'Computer Science')
    INSERT INTO tfbs.Department (DeptName) VALUES ('Computer Science');

IF NOT EXISTS (SELECT 1 FROM tfbs.Department WHERE DeptName = 'Business')
    INSERT INTO tfbs.Department (DeptName) VALUES ('Business');

IF NOT EXISTS (SELECT 1 FROM tfbs.Department WHERE DeptName = 'Engineering')
    INSERT INTO tfbs.Department (DeptName) VALUES ('Engineering');

DECLARE @DeptCS INT = (SELECT TOP 1 DeptId FROM tfbs.Department WHERE DeptName='Computer Science');
DECLARE @DeptBiz INT = (SELECT TOP 1 DeptId FROM tfbs.Department WHERE DeptName='Business');
DECLARE @DeptEng INT = (SELECT TOP 1 DeptId FROM tfbs.Department WHERE DeptName='Engineering');

------------------------------------------------------------
-- 2) Faculty 
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.Faculty WHERE FacultyName='Alice Tan')
    INSERT INTO tfbs.Faculty (FacultyName, DeptId) VALUES ('Alice Tan', @DeptCS);

IF NOT EXISTS (SELECT 1 FROM tfbs.Faculty WHERE FacultyName='Ben Lim')
    INSERT INTO tfbs.Faculty (FacultyName, DeptId) VALUES ('Ben Lim', @DeptBiz);

IF NOT EXISTS (SELECT 1 FROM tfbs.Faculty WHERE FacultyName='Carol Ng')
    INSERT INTO tfbs.Faculty (FacultyName, DeptId) VALUES ('Carol Ng', @DeptEng);

DECLARE @FacAlice INT = (SELECT TOP 1 FacultyId FROM tfbs.Faculty WHERE FacultyName='Alice Tan');
DECLARE @FacBen   INT = (SELECT TOP 1 FacultyId FROM tfbs.Faculty WHERE FacultyName='Ben Lim');
DECLARE @FacCarol INT = (SELECT TOP 1 FacultyId FROM tfbs.Faculty WHERE FacultyName='Carol Ng');

------------------------------------------------------------
-- 3) VehicleType
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.VehicleType WHERE TypeName='Sedan')
    INSERT INTO tfbs.VehicleType (TypeName, MileageRate) VALUES ('Sedan', 0.80);

IF NOT EXISTS (SELECT 1 FROM tfbs.VehicleType WHERE TypeName='SUV')
    INSERT INTO tfbs.VehicleType (TypeName, MileageRate) VALUES ('SUV', 1.20);

IF NOT EXISTS (SELECT 1 FROM tfbs.VehicleType WHERE TypeName='Van')
    INSERT INTO tfbs.VehicleType (TypeName, MileageRate) VALUES ('Van', 1.50);

DECLARE @TypeSedan INT = (SELECT TOP 1 TypeId FROM tfbs.VehicleType WHERE TypeName='Sedan');
DECLARE @TypeSUV   INT = (SELECT TOP 1 TypeId FROM tfbs.VehicleType WHERE TypeName='SUV');
DECLARE @TypeVan   INT = (SELECT TOP 1 TypeId FROM tfbs.VehicleType WHERE TypeName='Van');

------------------------------------------------------------
-- 4) Vehicle 
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.Vehicle WHERE PlateNumber='SGA-1001')
    INSERT INTO tfbs.Vehicle (PlateNumber, TypeId) VALUES ('SGA-1001', @TypeSedan);

IF NOT EXISTS (SELECT 1 FROM tfbs.Vehicle WHERE PlateNumber='SGA-2002')
    INSERT INTO tfbs.Vehicle (PlateNumber, TypeId) VALUES ('SGA-2002', @TypeSUV);

IF NOT EXISTS (SELECT 1 FROM tfbs.Vehicle WHERE PlateNumber='SGA-3003')
    INSERT INTO tfbs.Vehicle (PlateNumber, TypeId) VALUES ('SGA-3003', @TypeVan);

DECLARE @Veh1 INT = (SELECT TOP 1 VehicleId FROM tfbs.Vehicle WHERE PlateNumber='SGA-1001');
DECLARE @Veh2 INT = (SELECT TOP 1 VehicleId FROM tfbs.Vehicle WHERE PlateNumber='SGA-2002');
DECLARE @Veh3 INT = (SELECT TOP 1 VehicleId FROM tfbs.Vehicle WHERE PlateNumber='SGA-3003');

------------------------------------------------------------
-- 5) Mechanic
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.Mechanic WHERE MechanicName='John Mechanic')
    INSERT INTO tfbs.Mechanic (MechanicName, HasInspectionAuth) VALUES ('John Mechanic', 1);

IF NOT EXISTS (SELECT 1 FROM tfbs.Mechanic WHERE MechanicName='Mike Technician')
    INSERT INTO tfbs.Mechanic (MechanicName, HasInspectionAuth) VALUES ('Mike Technician', 0);

IF NOT EXISTS (SELECT 1 FROM tfbs.Mechanic WHERE MechanicName='Sarah Inspector')
    INSERT INTO tfbs.Mechanic (MechanicName, HasInspectionAuth) VALUES ('Sarah Inspector', 1);

------------------------------------------------------------
-- 6) Part  
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.Part WHERE PartName='Brake Pad')
    INSERT INTO tfbs.Part (PartName, QtyOnHand, MinQty) VALUES ('Brake Pad', 50, 5);

IF NOT EXISTS (SELECT 1 FROM tfbs.Part WHERE PartName='Oil Filter')
    INSERT INTO tfbs.Part (PartName, QtyOnHand, MinQty) VALUES ('Oil Filter', 30, 5);

IF NOT EXISTS (SELECT 1 FROM tfbs.Part WHERE PartName='Air Filter')
    INSERT INTO tfbs.Part (PartName, QtyOnHand, MinQty) VALUES ('Air Filter', 20, 6);

------------------------------------------------------------
-- 7) Reservation 
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM tfbs.Reservation
    WHERE ExpectedDepartureDate='2026-01-10' AND Destination='Off-campus training'
)
INSERT INTO tfbs.Reservation (DeptId, FacultyId, RequiredTypeId, ExpectedDepartureDate, Destination)
VALUES (@DeptCS, @FacAlice, @TypeSedan, '2026-01-10', 'Off-campus training');

IF NOT EXISTS (
    SELECT 1 FROM tfbs.Reservation
    WHERE ExpectedDepartureDate='2026-01-11' AND Destination='Client meeting'
)
INSERT INTO tfbs.Reservation (DeptId, FacultyId, RequiredTypeId, ExpectedDepartureDate, Destination)
VALUES (@DeptBiz, @FacBen, @TypeSUV, '2026-01-11', 'Client meeting');

IF NOT EXISTS (
    SELECT 1 FROM tfbs.Reservation
    WHERE ExpectedDepartureDate='2026-01-12' AND Destination='Field research'
)
INSERT INTO tfbs.Reservation (DeptId, FacultyId, RequiredTypeId, ExpectedDepartureDate, Destination)
VALUES (@DeptEng, @FacCarol, @TypeVan, '2026-01-12', 'Field research');

DECLARE @Res1 INT = (SELECT TOP 1 ReservationId FROM tfbs.Reservation WHERE ExpectedDepartureDate='2026-01-10' AND Destination='Off-campus training' ORDER BY ReservationId DESC);
DECLARE @Res2 INT = (SELECT TOP 1 ReservationId FROM tfbs.Reservation WHERE ExpectedDepartureDate='2026-01-11' AND Destination='Client meeting' ORDER BY ReservationId DESC);
DECLARE @Res3 INT = (SELECT TOP 1 ReservationId FROM tfbs.Reservation WHERE ExpectedDepartureDate='2026-01-12' AND Destination='Field research' ORDER BY ReservationId DESC);

------------------------------------------------------------
-- 8) Trip
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.Trip WHERE ReservationId=@Res1)
    INSERT INTO tfbs.Trip (ReservationId, VehicleId, FacultyId, StartOdometer, EndOdometer)
    VALUES (@Res1, @Veh1, @FacAlice, 10000, 10500);

IF NOT EXISTS (SELECT 1 FROM tfbs.Trip WHERE ReservationId=@Res2)
    INSERT INTO tfbs.Trip (ReservationId, VehicleId, FacultyId, StartOdometer, EndOdometer)
    VALUES (@Res2, @Veh2, @FacBen, 20000, 20300);

IF NOT EXISTS (SELECT 1 FROM tfbs.Trip WHERE ReservationId=@Res3)
    INSERT INTO tfbs.Trip (ReservationId, VehicleId, FacultyId, StartOdometer, EndOdometer)
    VALUES (@Res3, @Veh3, @FacCarol, 30000, 30500);

------------------------------------------------------------
-- 9) MaintenanceLog 
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM tfbs.MaintenanceLog WHERE VehicleId=@Veh1 AND LogStartDate='2026-01-13')
    INSERT INTO tfbs.MaintenanceLog (VehicleId, LogStartDate) VALUES (@Veh1, '2026-01-13');

IF NOT EXISTS (SELECT 1 FROM tfbs.MaintenanceLog WHERE VehicleId=@Veh2 AND LogStartDate='2026-01-14')
    INSERT INTO tfbs.MaintenanceLog (VehicleId, LogStartDate) VALUES (@Veh2, '2026-01-14');

IF NOT EXISTS (SELECT 1 FROM tfbs.MaintenanceLog WHERE VehicleId=@Veh3 AND LogStartDate='2026-01-15')
    INSERT INTO tfbs.MaintenanceLog (VehicleId, LogStartDate) VALUES (@Veh3, '2026-01-15');

COMMIT;
GO

------------------------------------------------------------
-- Debug helpers
------------------------------------------------------------
SELECT PartId, PartName, QtyOnHand, MinQty FROM tfbs.Part ORDER BY PartId;
SELECT MechanicId, MechanicName, HasInspectionAuth FROM tfbs.Mechanic ORDER BY MechanicId;
SELECT LogId, VehicleId, LogStartDate, CompletionDate FROM tfbs.MaintenanceLog ORDER BY LogId;
SELECT ReservationId, ExpectedDepartureDate, Destination FROM tfbs.Reservation ORDER BY ReservationId DESC;
SELECT TripId, ReservationId, VehicleId, FacultyId FROM tfbs.Trip ORDER BY TripId DESC;
GO
