USE FypProjectDB;
GO
EXEC sp_rename '[dbo].AccountProfiles', 'AccountProfile';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].Accounts', 'Account';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].appointments', 'Appointment';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].MedicalHistorys', 'MedicalHistory';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].Medicines', 'Medicine';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].OffDays', 'OffDay';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].Services', 'Service';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].slotDurations', 'SlotDuration';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].SpecialHolidays', 'SpecialHoliday';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].SystemUsers', 'SystemUser';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].timeSlots', 'TimeSlot';

USE FypProjectDB;
GO
EXEC sp_rename '[dbo].Users', 'User';