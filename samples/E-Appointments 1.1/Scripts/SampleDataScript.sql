USE [EAppointments]
GO
   
PRINT 'Deleting from table: [dbo].[Session]'
DELETE FROM [dbo].[Session]
GO

PRINT 'Deleting from table: [dbo].[User]'
DELETE FROM [dbo].[User]
GO

PRINT 'Deleting from table: [dbo].[Appointment]'
DELETE FROM [dbo].[Appointment]
GO

PRINT 'Deleting from table: [dbo].[Slot]'
DELETE FROM [dbo].[Slot]
GO

PRINT 'Deleting from table: [dbo].[ProviderClinicType]'
DELETE FROM [dbo].[ProviderClinicType]
GO

PRINT 'Deleting from table: [dbo].[Provider]'
DELETE FROM [dbo].[Provider]
GO

PRINT 'Deleting from table: [dbo].[Patient]'
DELETE FROM [dbo].[Patient]
GO

PRINT 'Deleting from table: [dbo].[Referrer]'
DELETE FROM [dbo].[Referrer]
GO

PRINT 'Deleting from table: [dbo].[ClinicType]'
DELETE FROM [dbo].[ClinicType]
GO

PRINT 'Deleting from table: [dbo].[Specialty]'
DELETE FROM [dbo].[Specialty]
GO

PRINT 'Deleting from table: [dbo].[ZipCode]'
DELETE FROM [dbo].[ZipCode]
GO

/* Insert scripts for table: [dbo].[User] */
PRINT 'Inserting rows into table: [dbo].[User]'

INSERT INTO [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [Email], [Role], [RefUserId]) VALUES ('{646DE0FA-455F-4687-8EE3-2528413AB72B}', 'bmsadmin', 'password', 'BMS', 'Administrator', 'bmsadmin@somecompany.com', 3, NULL)
GO
INSERT INTO [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [Email], [Role], [RefUserId]) VALUES ('{37DF7492-9F5C-4D60-A701-5D35DA019572}', 'vikram', 'password', 'Patient', 'User', 'patient@somecompany.com', 0, '{50B07541-30E0-4897-B44E-0B6E9AAEB916}')
GO
INSERT INTO [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [Email], [Role], [RefUserId]) VALUES ('{41F3E9F3-F21C-45D3-B935-9ED87459DB0F}', 'jdoe', 'password', 'Rahul', 'Khedekar', 'rkhedekar@somecompany.com', 1, '{0A8018CE-C670-4A1B-BD4D-29BB32250261}')
GO
INSERT INTO [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [Email], [Role], [RefUserId]) VALUES ('{2BD00BC7-8854-4cf1-8FBB-B72BCD99E9E9}', 'gsinha', 'password', 'Gaurav', 'Sinha', 'gsinha@somecompany.com', 1, '{BCEA4208-FA51-4C94-9E93-7C60660F5E55}')
GO
INSERT INTO [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [Email], [Role], [RefUserId]) VALUES ('{1C6DA68F-BA43-49CF-B92E-E5F16E6233B3}', 'jehangir', 'password', 'Provider', 'Clinician', 'providerclinician@somecompany.com', 2, '{B016A0CB-E552-4683-A0A1-850340D25A18}')
GO
INSERT INTO [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [Email], [Role], [RefUserId]) VALUES ('{61C5EEA5-C67C-4d7c-9D6E-88B4E1C97A4F}', 'sanjiv', 'password', 'Sanjiv', 'Chawla', 'schawla@somecompany.com', 1, '{12015779-4F3D-4817-8C53-45DA8D9242DE}')
GO

SET NOCOUNT ON
GO

/* Insert scripts for table: [dbo].[Specialty] */
PRINT 'Inserting rows into table: [dbo].[Specialty]'

INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Rheumatology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{2890AB10-FFAC-466E-AAC7-011C621F3E27}', 'Old Age Psychiatry')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{4AA15781-843C-4E67-A593-087ECB1FBCB2}', 'Cardiac Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Ophthalmology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{A9599785-0BE4-491A-BAEA-145A475101B9}', 'Clinical Immunology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{F45E5634-9027-43EF-AA63-1783EA896517}', 'Haemophilia')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{94E89ABC-2073-425B-87D7-1A47BB1F87A4}', 'Learning Disabilities')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Neurology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{4C17F1EF-9E4A-4C09-B199-1BBD25756665}', 'Vascular Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{B819DD43-84F6-4071-8F16-1CE76DBA5F36}', 'Colorectal Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Clonical oncology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{A2092336-B8E3-4785-820A-2275BFEAC22D}', 'Thoracic Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{89CA028F-4C4A-4331-87E0-23D4B852E932}', 'Paediatric Neurology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Gynaecology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{42101765-7A68-44FC-B44B-2800E574993C}', 'Child and Adolescent Psychiatry')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{B46D0CA1-B279-4D1D-BFE7-2C31228831A2}', 'Clinical Physiology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{68CF3418-42D5-492D-B887-2C473A9F8E56}', 'Pain Management')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{0C39A141-2726-4C5C-96E5-304AD8AC7A42}', 'Neontology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{2A566FF2-EBB7-4048-8806-32D1F2E9EB5C}', 'Allergy')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{CB28FA68-15AB-48E5-B677-3B319E71E367}', 'Chemical Pathology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{A813538A-B488-4306-89C4-4037D3183E23}', 'Urology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{778D618E-67C7-4C55-8A29-43E323A0F4A4}', 'Paediatric Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Plastic Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{56CEE91E-1DC3-44CD-9AFE-43FD30E3C5DD}', 'Well Babies')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{2C781E77-9228-45EA-975E-48132469D6A2}', 'Oral Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{0A4A8CFD-14E3-43A5-8E76-4A984AEC1258}', 'Tropical Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{DF515952-A1E2-43DE-B0FF-4F6F3FEBA348}', 'Genito-urinary Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{8035192F-7B17-4072-A3DF-5857CBB675A3}', 'Rehabilitation')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{E6CD76A4-4C03-4906-8C10-61192DD6D6B8}', 'Infectious Diseases')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{47C101E6-0B8F-47B7-A024-61BAA3BA7D8A}', 'Burns Care')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{11238044-9868-4570-A3E5-6480844EDAF1}', 'Geriatric Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'Medical Ophthalmology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{F6951048-0199-48DD-A534-6AB7B2D9E491}', 'Paediatric Dentistry')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{AF61FDBC-9901-4D3A-B852-6B4CB4D5AAA5}', 'Clinical Pharmacology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{66A1F0D9-1989-4247-8B9F-6C11970B3822}', 'Breast Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{C8F01D47-E979-4B93-9F28-6C5F06B201A1}', 'Gastroenterology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{F184B44D-C8BF-4C83-877B-6DA9A8BC6A0D}', 'Maxillo-facial Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{C857F755-08C0-44CD-ADDF-712F7D3FF176}', 'Psychotherapy')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{CF22D591-C38D-43F7-B8C1-73CC92CBBEE5}', 'Nephrology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Dermatology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{D79B608C-C7F3-42E3-BB0B-7BCE11AB4816}', 'Adult Mental Illness')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{9D3BDA9C-0F46-49E2-A9FA-80CC9D2B60E1}', 'Palliative Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{E3D35CAC-0AD9-45A0-A1AB-8B8A4A46A641}', 'Orthodontics')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'Diabetic Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{E6548AC1-5A09-4D31-9B8B-92EB818C0821}', 'Hepatobiliary and Pancreatic surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Trauma & Orthopaedics')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{E64763B7-5A35-4BAA-B4F2-A30275F3C007}', 'Hepatology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{FDEAD469-73A0-4085-8197-A54895F93027}', 'Thoracic Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{B0CDC5A7-143F-45DD-8177-AF786055A404}', 'Sleep Studies')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{B25D4531-E8F9-4777-A97D-B1231DFAFDAF}', 'Clinical Microbiology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'General Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Medical Oncology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatrics')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{10445633-31E5-4228-8451-BCDAC56710B3}', '2WW')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{4BF363C4-C133-49DA-81C5-BFB7340B8272}', 'Upper Gastrointestinal surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{43C05B94-E2C2-47ED-9778-C498AB8BE7F9}', 'Radiology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Endocrinology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{8DE531B4-6F57-4ED6-8AC9-C825ABDB29C3}', 'Forensic Psychiatry')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{20AE86CA-9B66-427A-8250-C929ED4EEFB1}', 'Clinical Genetics')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{BFEAD61A-B22B-4251-92AF-CC83A57BABD6}', 'Restorative Dentistry')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Obstetrics')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{B69749E1-0C18-44F7-8E15-D8643137A139}', 'Paediatric Cardiology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{DE59072A-3FD6-4A31-981F-DDDE6234150C}', 'Interventional Radiology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{E28D2481-E4F2-461A-9106-E18008DF9415}', 'Gynaecological Oncology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{F8A7006F-AB45-4A50-86C8-E194C09EE5D6}', 'Neurosurgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{783E9FA0-533A-467C-93E4-E84BAD1B81E5}', 'Clinical Haematology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Cardiology')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{A8144D3B-8422-4D95-B069-F2D7DA8DD2CE}', 'Bone Marrow and Transplantation')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{E342373A-601D-4B80-9F16-F70650E4A0EA}', 'Transplantation Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{CC6DE6B6-6BEC-4CE6-ABCE-F7538BE8CA57}', 'Clinical Immunology and Allergy')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{AE10D17D-7EBD-48DC-B65E-F76C3E62367F}', 'Cardiothoracic Surgery')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{CFC12AE1-E3D2-4106-A7F3-F7C86A6BD8BE}', 'ENT')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Ear Nose and Throat')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{4AA76E00-8D01-4EFA-8F36-FAD5B89743A7}', 'Audiological Medicine')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{00828D58-3DF7-4EE2-ABE8-FBF9827C06BD}', 'Dental Medicine Specialities')
GO
INSERT INTO [dbo].[Specialty] ([Id], [Name]) VALUES ('{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'General Surgery')
GO
   
SET NOCOUNT ON
GO

/* Insert scripts for table: [dbo].[ClinicType] */
PRINT 'Inserting rows into table: [dbo].[ClinicType]'

INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E11EC421-1262-4CF6-82E7-003ACD01AA26}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Allergy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C558E6CE-8F72-43E4-947E-0056D4663340}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Physiotherapy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CCCA81C2-FB67-4245-8863-026216C108ED}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Hypertension')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{40D39135-18B4-401A-BE91-02AD2B4F8AB0}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Skin Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{58E436B0-ED39-4D4A-B14C-03631842E6F5}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'General Urology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{496E4F1A-990F-4B37-86DE-036E4C6BF999}', '{E64763B7-5A35-4BAA-B4F2-A30275F3C007}', 'Infectious Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C6AA9C1B-F3D3-45B9-B15A-03B75F1440AB}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Endoscopy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{02AFF6E2-864D-472C-A974-03FC563214E0}', '{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'General Mecical Ophthalmology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E83607EC-D973-4178-9C97-03FE174882A0}', '{20AE86CA-9B66-427A-8250-C929ED4EEFB1}', 'General Clinical Genetics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D6FC24B7-30D6-4C74-B498-048145C28756}', '{E342373A-601D-4B80-9F16-F70650E4A0EA}', 'General Transplantation Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1DDC64B5-C6DC-48D2-B89A-048387B48F35}', '{A9599785-0BE4-491A-BAEA-145A475101B9}', 'Immunodeficiency')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F9A1DF3A-A445-4296-BBC4-05BCBA21C7D5}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Foetal Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B30D2E6C-4FA5-48A0-8B3C-05F233D99BFE}', '{B69749E1-0C18-44F7-8E15-D8643137A139}', 'Foetal Cardiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E620679E-B267-4A16-9631-0672BD0E84DE}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Botulinum Toxin')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CC216CA9-FAA1-45FB-B192-0691F6EA4754}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Mammoplasty')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8BBA5489-1728-49B9-B36A-078CC3F453A9}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Pain Management')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E70E882E-A32D-4A7E-B568-091D899B7813}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Head and Neck Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4955E607-D029-4CCF-B06F-0A47E4D6F8DE}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Spinal Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6742F68B-4F51-46CD-8D35-0B0E03AADDF6}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Orthoptics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{164D4921-F826-4173-9ED3-0CA596A34DC2}', '{4BF363C4-C133-49DA-81C5-BFB7340B8272}', 'General Upper Gastrointestinal Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{213E839F-24C8-4722-9949-0E846FE31449}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Sleep/Fatigue')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D555A719-B1DB-477B-A08B-0EDD14922CCA}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Corneal (anterior segment) eye disorder')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{43378A7B-0532-4967-87FB-0F65FE559CD8}', '{778D618E-67C7-4C55-8A29-43E323A0F4A4}', 'Cardiac Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F90903F8-061B-430F-AADA-0FBCCF0EDCFA}', '{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'Paediatric')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{09AE2184-21E5-4BA3-99D7-10344B335CC4}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Urological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6652B116-F197-409E-AB20-10CAC5DDA099}', '{BFEAD61A-B22B-4251-92AF-CC83A57BABD6}', 'General Restorative Dentistry')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0E1454CF-CB82-4038-AF00-10CBEA986E03}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Psychosexual')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{81657D5E-BB5A-407B-8728-116932135F2F}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'General ENT')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7F2AD0AB-9F0D-4B7C-A656-11C09BFB8BFF}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Allergy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A5727F1B-3997-40CE-BDF6-126A5A436380}', '{89CA028F-4C4A-4331-87E0-23D4B852E932}', 'General Paediatric neurology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D1EF9047-B07F-4185-ACD6-13E29614E647}', '{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'Pregnancy and Maternal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5935A904-1A57-4C9F-AA8C-13F43C6E9630}', '{CB28FA68-15AB-48E5-B677-3B319E71E367}', 'General Chemical Pathology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{EC49F85A-6557-48A9-A5A3-14CF0C2A873D}', '{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'Renal Diabeties')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{684A0CEC-1FE1-4A26-BC8A-14E6BD58C6E7}', '{B819DD43-84F6-4071-8F16-1CE76DBA5F36}', 'Rapid Access Colorectal Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3FA07FCE-731A-41BC-820A-1541C1C3DFEF}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Tuberculosis')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6FE6D25F-626C-45E1-B761-158FB8560AE1}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Low Vision')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{998080AB-F1B4-48B5-9CDB-17756D6C87E2}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Urology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6C18FBB6-B283-4FD0-B710-177FCD600DB8}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Male Sterilisation/Reversal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E24793C5-1584-45B8-859E-185FB113D3A9}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Paediatric Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B2515F48-AFEA-4AE0-9C55-18693D9583E3}', '{2C781E77-9228-45EA-975E-48132469D6A2}', 'General Paediatric Oral Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{94D42097-BA2A-4910-AC44-1932E7739391}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Thyroid/parathyroid Tumours')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6E0C9CD6-ABBD-4BDD-88BA-19C9580EB7C0}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Glaucoma')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8FBEB18D-7010-4CD3-84A6-1A1892137911}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Laser')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1946746E-C157-4210-963B-1A3042095A73}', '{B25D4531-E8F9-4777-A97D-B1231DFAFDAF}', 'General Clinical Microbiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B906A8A5-E347-446E-90E6-1B53AD311507}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'General Neurology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{673A5292-4700-4B01-B17B-1C4F73BF6E52}', '{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'Podiatry and Foot')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5A163D4A-27BE-4A1D-8DE8-1C85EA32655A}', '{0A4A8CFD-14E3-43A5-8E76-4A984AEC1258}', 'General Tropical Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{77BAF929-DD4D-4480-9A80-1CBC54CA450A}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Hypertension')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6A0A3C46-FB79-4B5A-83F2-1D196CA07C19}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Rapid Access Rheumatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{733CE55F-56FC-4D07-8326-1E033A4CA9BC}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Endoscopy (Lower GI)')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C7E9D959-4159-4D8B-9663-1E84D87ED6D1}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Breast')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8B0C16FA-8591-4ACD-BBE6-1F29C55E6292}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Movement Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0449D636-1C2B-4FD7-8B07-20DCF5F5F93D}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Upper Limb')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{466B2623-156E-4966-AFBD-21957D7A8699}', '{B69749E1-0C18-44F7-8E15-D8643137A139}', 'Pulmonary Hypertension')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B38C57FA-3B9C-47B2-906E-21F1EEDA1916}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Skin Photosensitivity')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0D817901-14C5-4962-AC2F-22A5A1AECE01}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Valve Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{41D3936F-EBED-4C01-AFEA-233007B9ED57}', '{E3D35CAC-0AD9-45A0-A1AB-8B8A4A46A641}', 'Routine Paediatric Orthodontics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{9C1F56D4-B78D-4CFA-9710-2355964CA68A}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Genetics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DA59FACB-9196-4651-8C78-236331088AA5}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Well Woman')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3E106A44-6884-4FCA-AB3D-23AB40C5CF0A}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Congenital Heart Disease')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BC0E328D-E180-4CDA-8E5A-23F7C863C810}', '{C857F755-08C0-44CD-ADDF-712F7D3FF176}', 'General Psychotherapy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{145FD76C-7C38-43EC-9D6A-23FB6238EF98}', '{DE59072A-3FD6-4A31-981F-DDDE6234150C}', 'General Interventional Radiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A5AD5C26-F610-479F-9015-244BEBC0E165}', '{66A1F0D9-1989-4247-8B9F-6C11970B3822}', 'Rapid Access Breast Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4B04E926-9A14-4EAF-BFF4-24AC34158AA1}', '{CF22D591-C38D-43F7-B8C1-73CC92CBBEE5}', 'General Nephrology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{420A94AD-6B45-4CEF-BA6F-24FEA24E4D1D}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Breast Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{33541DFD-49B4-4D10-84FD-2508B451CF4F}', '{E64763B7-5A35-4BAA-B4F2-A30275F3C007}', 'Metabolic Liver Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7A0DD497-F180-4558-B4D7-251DC643457C}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Adolescent Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2815ABD9-EB9B-4DC4-AC27-256189040777}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'General Clinical Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{29668BD1-ED0F-4316-8678-25B200E55630}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Recurrent Miscarriage')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{26299BC7-B869-45D3-A665-2664ED9DCC42}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'General Dermatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F4BC9AF0-0F64-437E-A978-27C83CD5A1DA}', '{9D3BDA9C-0F46-49E2-A9FA-80CC9D2B60E1}', 'General Palliative Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A5249356-63C9-4F8C-993C-2845D4EDBFF9}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'General Gastroenterology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D9454935-1845-407D-9501-2889F2504762}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Rheumatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{84C9A91E-E37F-4452-A4C4-28CA91FFF9BF}', '{11238044-9868-4570-A3E5-6480844EDAF1}', 'Stroke/TIA')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3C86B7B3-CAC3-4025-8007-29683A4888F1}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Genital Dermatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{611BB0DF-BCDF-46AB-A74D-298D252D79DD}', '{BFEAD61A-B22B-4251-92AF-CC83A57BABD6}', 'Surgical Dentistry')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6C9A0654-CAC2-4B43-9B03-2A53832D4A32}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Haematuria')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6D48F6FE-C9B7-4F69-98DF-2B03DEDFC182}', '{A2092336-B8E3-4785-820A-2275BFEAC22D}', 'General Thoracic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{20944D0B-97BB-42FA-A3FD-2B0D457D3DC2}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'General Infant')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CA4697AD-EBB8-4070-B6FC-2B9DFFC13D08}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Lipid Management')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{EA25545D-BE5A-4CFD-9AED-2BF250D695C8}', '{778D618E-67C7-4C55-8A29-43E323A0F4A4}', 'Upper Limb')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B804708C-D869-406B-A800-2C3E6E294410}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Strabismus/Ocular Motility')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7885B3FC-D326-4742-A365-2C73BDAFD5B6}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Ophthalmology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4E6DA349-159F-4A44-B176-2CE9B0085F08}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Prostate Assessment')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{60C1D8B9-25E0-414F-A76C-2D266D176089}', '{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'Genetic Eye Disorder')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6EE9F758-6F6A-4617-8ADB-30F7210C68F1}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Paediatric Plastic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{81EA7163-1B82-4927-87E6-31C873CD5416}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Oral Dermatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{43D3DCF4-40E4-452C-A019-32C8921237C7}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Orthapaedic Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{16EF900F-6F16-4834-B7A0-32E459B402F1}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Rapid Access Endocrinology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1BCA76F6-EDFD-470B-8B0C-32F714CD5A05}', '{B819DD43-84F6-4071-8F16-1CE76DBA5F36}', 'Colonoscopy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C74C08A4-9DBD-4F5D-ADAB-337EB4DCA981}', '{4BF363C4-C133-49DA-81C5-BFB7340B8272}', 'Endoscopy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{95757A97-2327-47F2-AEBA-343E3660030C}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric ENT')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{90A543FA-1D32-4D14-926D-34D2F21931D1}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Minor Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F7AF63D9-31D8-439C-A959-3505FE53F062}', '{783E9FA0-533A-467C-93E4-E84BAD1B81E5}', 'Haemoglobinopathies')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A0F9193D-5E80-4A48-88A6-35462140D4D2}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Neurology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{08386F37-9B92-4236-840F-35AC2116F364}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'USS Gynae')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{72A8F8F6-BD3B-4D47-949F-360D305027FF}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Acute Brain Injury')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{77D94730-1955-41D7-9BD7-36BB80913900}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Direct Access Gastroscopy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{56FDD257-0F50-43F4-8DDF-36C9B996EBD3}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Urological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3D96DB3F-4D10-4C81-A5EE-36CF0AFD1A4B}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Thoracic Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C8BEA356-2B49-47BA-91E1-37481BC94947}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Facial Plastic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2A7F9E0C-1EE6-4639-BA08-37606820E748}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Audiology Adult')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{582420B9-B65A-4955-B1CA-378222CB0E34}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Diabetic Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F1AC772B-35DA-4655-ACED-37A285C83280}', '{778D618E-67C7-4C55-8A29-43E323A0F4A4}', 'General Paediatric Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6EE78F11-68B2-4AB5-B8E4-37E6F75E4F9A}', '{CF22D591-C38D-43F7-B8C1-73CC92CBBEE5}', 'Vasculities')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B016A881-03E0-401D-A6E3-38749E1CC0FD}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Paediatric Strabismus/Ocular Motility')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{26F3CBC9-5D77-428D-B01F-39E205507B98}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Plastic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1D360CA8-EB4B-47E7-8280-3A57F4D08163}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Diabetes')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0C7D0533-7427-4CB6-9DF6-3A63516D0884}', '{A8144D3B-8422-4D95-B069-F2D7DA8DD2CE}', 'General Bone and Marrow Transplantation')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CBE80C5E-0664-4908-9A92-3A63C40B500C}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Cardiology Genetics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CA7B0666-15EA-45CE-A95B-3A6E7515C7CB}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Osteoperosis')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0C2CB1E5-5F27-407D-AB59-3C576D7696A5}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Functional Bowel Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{46365C79-6879-4605-91E5-3C71A8DAC7F2}', '{2890AB10-FFAC-466E-AAC7-011C621F3E27}', 'General Old Age Psychiatry')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1EEDDA91-48FB-44E4-A7C2-3C94CF202C6D}', '{F184B44D-C8BF-4C83-877B-6DA9A8BC6A0D}', 'Rapid Access Neck Lumps')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6BC613E5-9ACF-4EF1-AA60-3CC7AAA7E4A6}', '{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'Paediatric Transitional')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{33FCB7D9-8268-445D-8EFF-3D074F7897DB}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Neuromuscular')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1DFFA6C9-A74F-41D8-A9F3-3DD5793C598C}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'General Gynaecology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6FD4E616-414E-41C8-AE25-3E02F29FF36B}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Maternal/Foetal Risk')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8F09D1EA-7E79-46DD-BFDC-3E1CE9B024AD}', '{B819DD43-84F6-4071-8F16-1CE76DBA5F36}', 'General Rectal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7C0A2071-A6BC-441F-9EA2-3E51ACB885B9}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Podiatry')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5AC71783-897B-4E99-BFA1-3E7C87E9BFD0}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Metabolic Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{89012D29-F609-450D-9083-3F8EF2726E6B}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Lumps and Bumps')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{81DEAD24-A534-4114-B4C2-404EE5598D66}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Vascular')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{311E6181-C3C9-4582-97E4-40CDC079CF43}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Lupus')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A81FEDC9-5EF6-470A-A5CD-40CE524ADA04}', '{11238044-9868-4570-A3E5-6480844EDAF1}', 'Cognitive Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D3928F43-0CB2-4413-857B-4155E7E5CF87}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'General Paediatric Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5F0D06A7-9375-4014-AF8B-41DDFED82E61}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Reproductive Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DA4F1FF4-78AD-406D-8C78-433DD773448F}', '{B0CDC5A7-143F-45DD-8177-AF786055A404}', 'General Sleep Studies')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{545A4D1B-1B58-439E-8B8D-43C4AD91E4B5}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'General Ophthalmology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CE8D049B-9E18-43E4-BBC5-44C6DEE08BD5}', '{B69749E1-0C18-44F7-8E15-D8643137A139}', 'General Paediatric Cardiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CB8B50A5-2E9D-49AC-9E42-4509353C5706}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Osteoporosis')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E70679C8-08D2-4748-AAC2-463F8391B081}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Epilepsy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{486A81A0-AB71-46D2-815B-47118BC688E7}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Paediatrics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B0010C8B-58A1-4BA3-BE70-496121970AA0}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Multiple Sclerosis')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{46A19DF7-25A6-4A57-AF93-49CD54EAE33C}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Ear Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{9C3B1808-D661-4D57-AECA-49EE868FD70E}', '{0A4A8CFD-14E3-43A5-8E76-4A984AEC1258}', 'Ophthalmology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4367C81A-E101-463E-BF66-4A447D3AE119}', '{11238044-9868-4570-A3E5-6480844EDAF1}', 'Movement Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{033B106E-55B2-41BA-AAD4-4B6A1D2D951D}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Upper GI Target')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{99EF577D-9B83-4649-8CA6-4B995C9D2D6D}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Snoring/Sleep Apnoea')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F258536F-53B3-41B1-8C21-4BBC13ACC16A}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Lipid Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6C77266B-0C23-45D8-B075-4DAD4042C638}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'General Surgery Target')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{73D1C67A-8007-497A-951B-4E0ABAC4AC79}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Minor Plastic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6E940BE3-E9AE-42AC-B1DE-4E7DAE4A7F9A}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Menopause')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{FCDE2E2E-C714-4534-8FBC-4F7C314CD9EC}', '{8035192F-7B17-4072-A3DF-5857CBB675A3}', 'Neuro-rehabilitation')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5012A202-5EEC-4A93-864B-4F904D8E0EFD}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Lower Limb')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{35D47FA7-24ED-4F63-972B-4F96AA955475}', '{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'Medical Retinal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8463664B-82D5-43F6-BA7F-502622220B2C}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Family Planning')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B8806177-22B5-4CB5-AAD0-50722FABCEF3}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Fracture')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BABFC27E-5BD2-4188-AADB-5150CE8E83AD}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Gynaecological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A8ABA4E7-44DF-420B-95B0-51A5E6312AAF}', '{11238044-9868-4570-A3E5-6480844EDAF1}', 'Metabolic Bone Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A274BA07-2155-48FC-8544-52C89679D512}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Laser')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{57E7AB16-F340-44A6-93E2-53A254B03EE8}', '{4AA76E00-8D01-4EFA-8F36-FAD5B89743A7}', 'General Paediatric Audiological Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4335CF8C-1A17-41B7-8788-589FF171DA0F}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Paediatric Rheumatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{65D7DBE4-859F-4754-9DC3-5959B5EDFB24}', '{4AA15781-843C-4E67-A593-087ECB1FBCB2}', 'General Cardiac Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{775CE304-609B-437C-B6C2-5B08D18FA759}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Cerebrovascular')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{16067268-E6DC-4552-A3C4-5C1085585EEA}', '{B819DD43-84F6-4071-8F16-1CE76DBA5F36}', 'Flexi Sigmoidoscopy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{22045AA8-3578-4B63-8A5A-5CFE52D23633}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Medical Retina')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D0A4D316-D0BC-496C-BB25-5D10FB552A33}', '{A9599785-0BE4-491A-BAEA-145A475101B9}', 'General Clinical Immunology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F5713ED5-AAC5-41C6-947C-5D2C1148CE46}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Pelvic Floor')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{34A481E0-0CF7-44D1-911A-5EE1D18B2135}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'General Obstetrics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{37B94486-CF49-453C-A809-613015FA8B18}', '{CF22D591-C38D-43F7-B8C1-73CC92CBBEE5}', 'Renal Diabeties')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4B809213-FA5D-44AE-BAFD-61FFE4CBAAB2}', '{4C17F1EF-9E4A-4C09-B199-1BBD25756665}', 'Doppler Test')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{EDB9B052-43F3-4AC6-9EF7-625CCD8724C2}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Acute Access ENT')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1A7C0267-7F49-408C-8D74-629D6DE9093B}', '{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'Low Visual Aid')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{20CE4BAB-27E2-4411-AF11-62C80FD2F97F}', '{43C05B94-E2C2-47ED-9778-C498AB8BE7F9}', 'General Radiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F6F651EE-A823-4899-A041-63AAF2EDCD53}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Endocrine Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{898FDA16-69EF-45BC-8582-63CDCCEF666D}', '{B819DD43-84F6-4071-8F16-1CE76DBA5F36}', 'General Colorectal Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3383E4C1-738B-444D-AFC9-63E3A157FCE4}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Pain Management')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5CFA697D-3877-4124-BABE-651FEAC023EA}', '{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'Eye Screening')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6FF77659-38C2-405F-AF8B-6594332C4EB9}', '{47C101E6-0B8F-47B7-A024-61BAA3BA7D8A}', 'Acute Access Burns Care')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4413558B-5A48-485C-8C0F-65A5BEEE1A7D}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Behavioural Problems')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{29EF594A-4F70-4433-BE62-65E2708A9B52}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Gastrointestinal Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{44C31E22-01A8-48E8-B495-6687FD749D70}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Gastroenterology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DC085D7E-85DD-4D2E-9FA8-670CD15ED4A6}', '{68CF3418-42D5-492D-B887-2C473A9F8E56}', 'General Pain Management')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7403EAEC-7138-4330-94CB-673F3DB87503}', '{778D618E-67C7-4C55-8A29-43E323A0F4A4}', 'Urology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7678C2A7-647F-471A-BB8A-67D66337EC38}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Liver Conditions')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{70663897-A2B5-4698-998A-68695326939E}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Urodynamics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{20EE241C-0095-4C89-8429-689FB8195B81}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Developmental')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0A105530-81CA-4CDB-89EB-6905941EA36B}', '{783E9FA0-533A-467C-93E4-E84BAD1B81E5}', 'Rapid Access Clinical Haematology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DE5DD1AD-7157-4651-8701-6A9722628422}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Heart Failure')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{38C35479-CA4C-4AEF-9081-6AA34F510858}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Rapid Access Gynaecology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A925C3C7-0BF1-4BFB-A22E-6B3265CDCFDF}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Vitro-retinal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3F6AD579-2BA1-4A35-8D36-6B5941CF980B}', '{F184B44D-C8BF-4C83-877B-6DA9A8BC6A0D}', 'Rapid Access Facial Skin Cancer')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7ABE3EF1-C800-4E03-918E-6BA0444A870F}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'General Cardiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BE6BF640-8496-4253-82CC-6C7DAD8D2CCD}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Vascular')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{83151396-25A3-4682-AB57-6C84800E2055}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Foetal Cardiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B594D9B5-40A3-4099-A84A-6D5B633E48F6}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Thyroid Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BF4BD875-1FE2-4785-8C2A-6D8B97230871}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Inflammatory Bowel Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{FF9E5927-6D0F-47D8-95D9-6DA0239346BB}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Lacrimal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{771C1A59-8045-4F11-BF69-6DE014B1FA5B}', '{CF22D591-C38D-43F7-B8C1-73CC92CBBEE5}', 'Metabolic Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3DDDC997-85A4-47BE-8A73-6E855B3CD199}', '{F45E5634-9027-43EF-AA63-1783EA896517}', 'Paediatric Haemophilia')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{FAEA4EDA-E9C6-4602-8A5C-70E0BF168F5C}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Perineal Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{EC4BDF5D-88BE-42CF-80D8-70F8DE217DE0}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Erectile Dysfunction/Andrology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{53165B66-95F2-4A30-A805-71994123891D}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Continence/Incontinence')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CDF83FB0-2AAE-4EDC-ACBB-7249CB2D4BD8}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'General Medical Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{240B87DB-4564-45B2-8A97-728ACACA00D8}', '{CC6DE6B6-6BEC-4CE6-ABCE-F7538BE8CA57}', 'Immune Deficiency')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3F8948B5-DC81-42A5-B349-734FA391A3A1}', '{42101765-7A68-44FC-B44B-2800E574993C}', 'General Child and Adolescent Psychiatry')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{966992D2-0727-4F40-986E-75300D923707}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Audiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0585471D-03C0-4D0B-A6A3-755F2517A1FB}', '{4AA76E00-8D01-4EFA-8F36-FAD5B89743A7}', 'General Audiological Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A1F13490-2328-48A9-B6E2-7585D8B3EC9F}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Neonatal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{EF00AF88-7C0A-46E6-B831-75D5F943682C}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Leg Ulcer')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{915C9819-26D2-48E6-8D62-769222C25D1F}', '{4BF363C4-C133-49DA-81C5-BFB7340B8272}', 'Rapid Access')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F9A5817A-CE71-412B-8A86-76C4566BEA39}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Neuro-oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2FA2284E-35E8-4824-B0EF-76DDCF4C0768}', '{66A1F0D9-1989-4247-8B9F-6C11970B3822}', 'General Breast Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6E7FF9D3-40F0-4340-AEBC-776006128181}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Haemoglobinopathies')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{76F4D8D4-C3B2-49EE-BB67-77CF84B51432}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Gastrointestinal Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C2DDD6CE-3238-420E-B2AF-78211A73F0F2}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Paediatric')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{935545FE-B017-4D8F-90C1-789FF9442F0C}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Otology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{302037C4-3621-46C1-B554-78C1263F29E3}', '{43C05B94-E2C2-47ED-9778-C498AB8BE7F9}', 'Ultrasound')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{AEAEF7A1-8D8B-4972-ADDF-7997414FD238}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Arrhythmia')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8876DAD7-DB3E-4914-9D79-79C4D5F166CB}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'General Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{690066AA-FFF3-4CA0-AFD7-7A2BD2BC94D1}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Eyelid (adnexal) disorder')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{9521E60B-CEED-4FD8-BEAA-7A4EDA8C5055}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Gynaecological Endocrinology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5EEC2A0C-B7D9-44FD-A4BC-7B9C81EDF245}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Rapid Access Urology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{61A057D5-E23C-4F5F-B460-7BF709B2FFD0}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Paediatric')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2A4A0032-A2E5-4C95-AB90-7BFB4B70B3FD}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Respiratory')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3EF5A36B-4F53-468A-92A2-7CBF80D7D663}', '{CB28FA68-15AB-48E5-B677-3B319E71E367}', 'Metabolic Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D9F00B32-81CD-4D50-87BA-7CC1FE7A7BE1}', '{AF61FDBC-9901-4D3A-B852-6B4CB4D5AAA5}', 'General Clinical Pharmacology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{92AC4C9B-5A36-47D5-94F8-7CCD990EDCA0}', '{CC6DE6B6-6BEC-4CE6-ABCE-F7538BE8CA57}', 'Autoimmune Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{779F613A-73CB-4FDF-9647-7D2FFAEEB424}', '{E64763B7-5A35-4BAA-B4F2-A30275F3C007}', 'General Hepatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{70E0EC42-7286-47A6-B779-7D7288F3A1CD}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'General Paediatric')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{98826EE1-4C62-4AED-8AAB-7EF35EAAEF32}', '{8DE531B4-6F57-4ED6-8AC9-C825ABDB29C3}', 'General Forensic Psychiatry')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{9F4E2AB0-F5D5-44AF-BFA1-7F3C6BEEB747}', '{CC6DE6B6-6BEC-4CE6-ABCE-F7538BE8CA57}', 'Adult')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{99B5651B-5E55-4DC4-B659-7F89A2EE26CD}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Endocrine Pregnancy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3FB31BBD-A38B-41F6-BDC7-7FCFE619EC21}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Foetal Surgical')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2D183E15-3A43-4966-9EB9-837E43F331DB}', '{66A1F0D9-1989-4247-8B9F-6C11970B3822}', 'Breast One Stop Unit')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7FD5FB64-4C18-4CC7-A58F-839D45D8E006}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Endoscopy (Upper GI)')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8F6ACCDB-F3C3-41B6-AB66-8405589A9F71}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Keratopathies')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E968AEEE-4BD9-404B-8EBE-84DC8E489777}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Rapid Access Opthalmology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{653DD348-E35B-4A24-9FA0-85061A3964FE}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Gastrointestinal Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F7633193-1EFC-49FC-923B-851809CB1222}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Head and Neck Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1A8117DB-544F-484D-B7FF-851AD628811E}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Cosmetic Camouflage')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{366F9757-5F5B-4677-A179-8558FECD528B}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Rapid Access Chest Pain')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D192C699-7232-4560-B61C-857243222044}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Swallowing Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CD450DFB-2175-4265-A494-86854CB9EB02}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Hernia')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{AD633E4B-52E2-4F05-94C8-86FBD8BB1EA8}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Metabolic Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{533903F9-D6AA-41CF-9EAA-87505CA08481}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Gender Reassignment')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{48B18C03-98EA-406E-A3E9-875D74C91537}', '{E6CD76A4-4C03-4906-8C10-61192DD6D6B8}', 'General Infectious Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{67E6ECED-4FAB-484B-81BD-88802A56FA08}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Nasal Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{169855E2-31C6-4360-9E6C-891675EE349D}', '{F184B44D-C8BF-4C83-877B-6DA9A8BC6A0D}', 'General Max-Fac Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BD2530CA-C367-4ACE-96F5-896B2F9C7813}', '{BFEAD61A-B22B-4251-92AF-CC83A57BABD6}', 'Prosthodontics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DEC491E5-94EB-476B-8CF0-899FDC3B36E4}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Occupational Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{ECAC1D14-119D-4748-B6DC-8BD7E1AB1FCF}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Twins')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{846A2BB1-28E6-4E74-82D0-8D3AA686E1DB}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Male Sterilisation/Reversal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CB4B65F6-D5F3-43C1-8424-8E3D9AFD62EE}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'General Plastic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A548B837-4191-442D-BFA2-8F7C9AC075E4}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Menorrhagia')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5102E3A2-FDF2-4B34-9F21-8F97D97E3426}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Acute Response Rheumatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F079A22B-0542-4154-B2BA-8F9B2F274133}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Rapid Access Neurology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2DA47AEE-4F08-4F8B-81A3-900A9EAD6E9D}', '{F184B44D-C8BF-4C83-877B-6DA9A8BC6A0D}', 'Salivary Gland Disease')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{EF1DA7EB-75A9-4323-B01F-90379B92A548}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Diabetes')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1F2E5B22-D798-4196-8D0C-91CB161EEEBE}', '{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'Contact Lens')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B9AE0683-1F5A-4E54-BCBB-92BC448A6461}', '{E6548AC1-5A09-4D31-9B8B-92EB818C0821}', 'General Hepatobiliary and Pancreatic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B6F5DE04-F914-45F4-987C-93FC4AD01E49}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Stroke')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{32A88067-7EE8-48B6-A0A2-93FD9EA02C7D}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Upper gi')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4F1AD66C-3A92-40BE-975B-94514C34411E}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Rapid Access')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C3CA8F28-BE73-4401-8FD5-95C0AF2987AB}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Haematological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{104CA247-338C-45AD-9058-9669071B8ABC}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Metabolic Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{04EDE646-890E-4A30-B3D0-9697D7B317B1}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'General Baby Clinic')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{ED367FD5-28B8-41EF-988A-974F0AFE0F53}', '{F8A7006F-AB45-4A50-86C8-E194C09EE5D6}', 'Spinal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3D1C2C4C-CC44-4953-83CF-981160FCDE7B}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Neuro-ophthalmology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BDC9D76F-FCDD-4229-B358-98202DE07416}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Craniofacial Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B82D2404-6DD6-409F-8696-98ABEA9DF5BE}', '{4AA15781-843C-4E67-A593-087ECB1FBCB2}', 'Paediatric Cardiac Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{116F3756-A4F7-4AE8-BC9B-98BAA8F511D4}', '{CB28FA68-15AB-48E5-B677-3B319E71E367}', 'Lipid Disporders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B44FB895-1AF6-4EBB-A458-991E096D0D4E}', '{778D618E-67C7-4C55-8A29-43E323A0F4A4}', 'Neurosurgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0184A9A6-1CE7-442D-8582-998F27EA212E}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Male Reconstruction')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{3637CF6C-1426-42B4-8214-9A273DD47ACF}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Breast Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B7AE8DB7-C5AC-4075-9EEB-9A2C23EC37D9}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Rapid Access Thoracic Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{42991CE4-9525-4688-B920-9A5988A133CF}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Pregnancy Advisory Service')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{30101A87-5E23-4891-9034-9A65AA906754}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Metabolic Bone Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{06DAB805-F703-4FF0-B3BD-9A663215E6F4}', '{783E9FA0-533A-467C-93E4-E84BAD1B81E5}', 'General Haematology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{379B1CCC-0A32-42DB-907E-9B9363E3F2C2}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Transient Ischaemic Attack/Stroke')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E6426D70-175A-4484-A1CF-9B98AD12CEE4}', '{66A1F0D9-1989-4247-8B9F-6C11970B3822}', 'Breast Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CB4A87F7-6CD8-4D52-98AE-9BDDECEF760B}', '{2C781E77-9228-45EA-975E-48132469D6A2}', 'General Oral Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{AC2BBB3A-17B1-4D33-97EF-9C250AC2886C}', '{8035192F-7B17-4072-A3DF-5857CBB675A3}', 'General Rehabilitation')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{47E35AD9-4538-4327-917C-9CA4375B2E73}', '{2A566FF2-EBB7-4048-8806-32D1F2E9EB5C}', 'Food and Drug Allergy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C8DAF68B-AD79-4DD9-B7B4-9CB6685A219C}', '{94E89ABC-2073-425B-87D7-1A47BB1F87A4}', 'General Learning Disabilities')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{64608C7F-52A8-4A7F-AE41-9D07DE3787E6}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Urogynaecology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F83D2AB1-BF4F-48A6-9AC7-9D307266DAAD}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Maternal Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F63769FD-1AC4-4E21-9FDF-9DC561D6508D}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Rapid Access')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{EDFFD195-4123-499C-990E-9EA3C05EE606}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Allergies')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7B908C58-7EF4-4BDD-9C9D-9EFC43F7C4A3}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Urogynaecology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8E355C12-E526-4867-A1A9-9FE6AB72FAE5}', '{D79B608C-C7F3-42E3-BB0B-7BCE11AB4816}', 'General Adult Mental Illness')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{FA953924-3917-4F71-B18F-A01E33187871}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Orthopaedic Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A5DDBD80-431F-4239-8CE8-A17F22057F3D}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Urological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{45272D8D-0C3D-45E8-8783-A1AEC01BC9AD}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Changing Lesions')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C44692D9-2A60-496D-B5BF-A3C8F9B99053}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Dietetics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{48F07FA1-234B-4F7E-8EEC-A5795D642A0C}', '{F45E5634-9027-43EF-AA63-1783EA896517}', 'General Haemophilia')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{723EAD60-C684-42A5-96FA-A75E063971CD}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Lipid Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6C5763CA-C488-4BBD-B3F7-A8C749CC6525}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Paediatric Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{468AB214-F1C6-45BD-96CC-A932808D999A}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Cystic Fibrosis')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B1F78DAB-AD95-44C5-91C9-AA3823872107}', '{783E9FA0-533A-467C-93E4-E84BAD1B81E5}', 'Haematological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D2FCC07D-2F96-44B0-AE2F-AA62093A6058}', '{CC6DE6B6-6BEC-4CE6-ABCE-F7538BE8CA57}', 'General Clinical Immunology and Allergy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1DC6BBD2-D7A3-4F72-9F48-AC604B1E2053}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Erectile Dysfunction')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C6AAA39C-8E51-49EF-A1D6-AE20C34290F7}', '{4AA76E00-8D01-4EFA-8F36-FAD5B89743A7}', 'General Adult Audiological Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DBF478B1-970F-46C8-9B02-AE64BE94DBF5}', '{CB28FA68-15AB-48E5-B677-3B319E71E367}', 'Genetic Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CC769BE5-7188-4499-8247-AE84952DFB50}', '{11238044-9868-4570-A3E5-6480844EDAF1}', 'General Geriatric Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5B998031-405D-4AB9-8578-AEAD96AEAEEB}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Colposcopy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F6A346F5-88C5-4238-A4F2-AF9E3AFF483A}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Metabolic Eye Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CCC11271-C063-4EB7-ACB8-B02C9AC897D4}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'External Eye Disorder')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D492A01B-B79B-497D-AEC2-B03D87E6F731}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Orthopaedic Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{FD44F46D-E833-4AAD-8F0B-B06F6CB7B81B}', '{F8A7006F-AB45-4A50-86C8-E194C09EE5D6}', 'Movement Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D45E6CE7-BD63-43AB-ABB4-B0DF19DF59B7}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Dermatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{779C4099-ABBA-4780-926C-B1E0FFBED8AA}', '{BFEAD61A-B22B-4251-92AF-CC83A57BABD6}', 'Periodontics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D0CF2C24-699E-4D76-97EF-B2317DAE9A1A}', '{DF515952-A1E2-43DE-B0FF-4F6F3FEBA348}', 'General Genito-urinary medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{AD02888A-988F-4F2C-94CD-B254D15D40DA}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Diabetic Medical Retina')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{79254BB1-65F1-4772-A795-B3FDDC8C8FA3}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Reproductive Endocrine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C3C6EFE2-4E95-4007-8318-B4B8E524033E}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Dermatological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1ECD46CA-ABA5-440A-AEBF-B4C7DDB33738}', '{47C101E6-0B8F-47B7-A024-61BAA3BA7D8A}', 'Burns Case Rehabilitation')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1000302E-3E7D-455C-8D67-B4ECB0F10394}', '{0A4A8CFD-14E3-43A5-8E76-4A984AEC1258}', 'Dermatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1A944680-77FB-4D23-8B3F-B58F0682F0C3}', '{AE10D17D-7EBD-48DC-B65E-F76C3E62367F}', 'General Cardiothoracic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{57B3F2DF-62D5-42E0-B59F-B5E3A11CEF2E}', '{E28D2481-E4F2-461A-9106-E18008DF9415}', 'General Gynaecological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{79951A50-313D-4DA9-8048-B624334C45D5}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Voice Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5E70C75A-0C9B-4730-B260-B64D65E36CB4}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Antenatal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B938B0AF-B367-46B5-BF97-B788FCBA149B}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Spinal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{611C190A-F6F5-4F81-A625-B807D014D6E8}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'EPAU')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DE34858A-B708-4413-B8C5-B8A0DCAE98CC}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Motor Neurone Disease')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{765692AD-1606-4DAD-9A9F-BAEED6604525}', '{E58FC643-F6AB-411C-943C-EE400241CB5D}', 'Acute Access Cardiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0F701472-95F4-4AFD-B15D-BB5A2B97763D}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Cataract')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CBF79E5D-EAE5-4023-AEFF-BC04EE87EDF6}', '{47C101E6-0B8F-47B7-A024-61BAA3BA7D8A}', 'General Burns Care')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0678221F-DD77-4FE1-94A7-BC6684533FC4}', '{0C39A141-2726-4C5C-96E5-304AD8AC7A42}', 'General Neonatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{871DE533-6287-4530-B60F-BCFF52E8754C}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Haematology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{37C6DD9C-DC66-4716-A36D-BD525D1C0D65}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Phlebotomy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{75F3F88C-C7FA-4158-A800-BD875A26B502}', '{66A1F0D9-1989-4247-8B9F-6C11970B3822}', 'Mammoplasty')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{A4CAB2FA-F04D-44D9-8DAC-BE4F90E5884E}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Cardiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{192565DA-AA42-4720-9F8D-BE5D3738521E}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'General Endocrinology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{88DFCDAD-CE7F-4EA9-8418-BFC7C45D9A97}', '{783E9FA0-533A-467C-93E4-E84BAD1B81E5}', 'Haemostasis and Thrombosis')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{676F4044-56BC-4478-B41F-C07F6107B1B7}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Immunotherapy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{34ED42C6-481F-4411-A593-C090F98416FF}', '{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'General Diabetic Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{769823A1-29AE-4D8B-AAB1-C0A2C5BA0354}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Lower Limb')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4ABE29BF-DF42-4A89-9E70-C20A32F1B2B1}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Lower Urinary Tract Symptoms')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1348930F-1858-43F3-9FEA-C3DE45CAF24D}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'General Endocrinology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F8F5ACB3-618F-4699-8D50-C44E3429DED5}', '{4C17F1EF-9E4A-4C09-B199-1BBD25756665}', 'Leg Ulcer')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{483381D5-2F3A-4907-A855-C468182E7F39}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Rapid Access')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D8268219-C35E-4BAC-834E-C60AC2826D16}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Post Menopausal Bleeding')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{91B1CA62-AF02-4760-AF03-C65914C139DE}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'General Adult')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D8F27FBE-C570-4020-BFB7-C913D7D7767A}', '{E3D35CAC-0AD9-45A0-A1AB-8B8A4A46A641}', 'General Orthodontics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7D1118E5-82C2-4DC8-AB24-C975FEC03733}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Vulval Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BF9625F8-BD92-45E9-846E-CA50ED39B712}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Craniofacial')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DF1EA696-DD14-4D94-AE6D-CA7E2117F482}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'Upper GI')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{842BC504-04A5-4409-BA73-CAD3B49CBF2C}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Cognitive Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{06BBB82B-8DC5-4D18-9803-CC04F93CB8CC}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Skin Lyhphoma')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D64A6691-03D2-4FD7-ABC9-CC57DAE4EB8F}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Thoracic Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4BE63A9E-2A86-46BD-B6F1-CD24BA1811A2}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Audiology Paediatric')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{306E32FA-B62D-4A8B-A070-CE35759FD279}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'USS Obstetrics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2CAA98FF-D408-47CE-A104-D0C3093051AB}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Laser Assessment')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{90675150-090E-4270-8343-D0F920B311B9}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Upper GI')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0FC84530-C391-4713-9416-D246AF1EA38E}', '{56CEE91E-1DC3-44CD-9AFE-43FD30E3C5DD}', 'General Well Babies')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DD7133FC-EB48-482F-AB3E-D2968EFDAC36}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'Soft Tissue Clinic')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B136E778-0C02-4746-BD3E-D32B6BEE57FE}', '{4C17F1EF-9E4A-4C09-B199-1BBD25756665}', 'Aneurysm')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B876C644-BF1B-4358-8B34-D3364CF2BB4D}', '{F8A7006F-AB45-4A50-86C8-E194C09EE5D6}', 'General Neurosurgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{05C25BE3-8888-4858-84E4-D3927A8B408A}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'General Thoracic Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{FC93D8BE-5757-4A74-848B-D3A6C4AE2375}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Fast Track')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{B83F8063-DDD3-47C7-93C8-D43761ADE86E}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Salivary Gland Disease')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F3D95A2D-BB2E-40CA-88EF-D4D0F7B7CBA2}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Balance and Dizziness')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{129B716C-A845-4E9B-84B4-D4F1B6E8A354}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Menstrual Disorders')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{45519F33-D2AC-4063-AD78-D5C2D61D0027}', '{4CB91333-FF0C-43A8-BBA6-0006619B6578}', 'General Rheumatology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{FCDC37ED-815D-4A8A-B3C3-D6265413F466}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Urodynamics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2878CB42-118A-4F51-89ED-D69978524A0B}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Hand')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{AD040ADF-0EC5-471F-B01B-D7E6F78E0966}', '{0A4A8CFD-14E3-43A5-8E76-4A984AEC1258}', 'Paediatric')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6A4E415E-09B5-49F2-BE05-D7F87131F4C2}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Sleep')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4B092CE1-74C5-479B-B69C-DA66BD3D3A31}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Gynaecology and Perineal Reconstruction')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{1F197AD6-8E05-4FC9-AB03-DB0CC3EEFA8C}', '{0A4A8CFD-14E3-43A5-8E76-4A984AEC1258}', 'Leprosy/Leishmoniasis')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D6310807-E02E-4366-AED0-DBD4EC1AC04C}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Diabetes')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E95CE28B-8DCE-4C7C-B297-DC37C672DC72}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Fertility/Infertility')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5F19BDFB-CAE1-4E74-AC81-DD4041A518CD}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'General Adult Trauma and Orthopaedics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8CBA04BB-3D49-45EE-A1DD-DE419099B04F}', '{6A7CAAE9-AEEE-4A02-8C72-43E918009841}', 'Rapid Access Plastic Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7F7EC984-D76E-4521-82AF-DF4A6C902061}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Haematological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{77F0ECB1-099E-4B4D-950A-DFD566A6050B}', '{3C52C061-3DA7-4D6C-AB47-FC57C4502AE5}', 'Rapid Access General Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7818E0D4-4710-4376-B050-E115057DD2D1}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Immunology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{2DC1E54E-C078-4389-B1E1-E47B5E3786B3}', '{D5B4D1FF-584A-49DC-8019-6AAB10E30886}', 'Metabolic Eye Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BCB1B414-7724-408C-9B1B-E4EA2A07D3A2}', '{DA52DBF2-7266-4EBE-8950-1AE07967C0E4}', 'Headache')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{78BAAD85-F1FD-4B8D-9B3C-E5A1FB5B9FD6}', '{11238044-9868-4570-A3E5-6480844EDAF1}', 'Falls')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{42A566A1-8ABC-45F0-B1C9-E5E037383AA4}', '{F6951048-0199-48DD-A534-6AB7B2D9E491}', 'General Paediatric Dentistry')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8F52689A-3808-4E39-91DC-E6DC3E2A5BAC}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Renal')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CB14AC00-45AF-47A0-B1EC-E892A55E666A}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Neuro-oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C85CE23F-017F-4DFC-89AB-E8ED68CC8981}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Acute Access')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{73CA29C9-9FF3-4099-A680-EA41F1435B56}', '{F184B44D-C8BF-4C83-877B-6DA9A8BC6A0D}', 'Rapid Access Head and Neck Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{32DD6704-046D-47A7-ACD1-EB919194905D}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Sarcoid')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CCE86BC7-3DE9-4570-9BE6-EB9BC3EAD3D5}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Trauma and Orthopaedics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{5016A4B1-6DA3-4339-8D3D-EBBB4ECBA526}', '{15E55050-CFA8-4FFA-93CC-25C8EC28F39D}', 'Adolescent Gynae')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{E75E5F07-3BAD-459B-A7A4-EC211450D025}', '{F184B44D-C8BF-4C83-877B-6DA9A8BC6A0D}', 'General Paediatric Maxillofacial Surgery')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F3D394C5-B377-4094-AC9F-EC37BF3E81D9}', '{783E9FA0-533A-467C-93E4-E84BAD1B81E5}', 'Anti Coagulant')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{34C66A94-70CC-4FC0-9FA6-EDB07EF7452B}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Gynaecological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{BCBF8733-C5F7-4060-8B8E-EDC5C306C203}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Lower GI')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6A4D0DEF-390B-4461-B35C-EDCF8B76BE16}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Immunisation')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{8158AAFA-E64F-403B-A42B-EE04759D289F}', '{64DD7722-A95D-42CC-8165-C56D02C764C8}', 'Endocine Pregnancy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{CB1CAF4E-0114-4598-869C-EEF1F05CF60A}', '{BFEAD61A-B22B-4251-92AF-CC83A57BABD6}', 'Endodontics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{DCA56333-85CA-4BF8-A8B9-EFF4A178517E}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Haemophilia')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{C1DA63C1-2BEA-4F42-A082-F19CFF07C952}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Infectious Diseases')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{4961C254-E2BF-4343-82D5-F1D99AA7AC1B}', '{A813538A-B488-4306-89C4-4037D3183E23}', 'Female Reconstruction')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{7CC6C542-314D-46D6-ADD8-F41F7D6F2434}', '{00828D58-3DF7-4EE2-ABE8-FBF9827C06BD}', 'General Dental Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{9036B587-8072-4C9C-819F-F52DA6A2D1F4}', '{3CCA9551-C3C4-4E35-AC6D-B16C1236C103}', 'General Medicine')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{D1727E7B-124A-4C88-B9E2-F5BA86C4608A}', '{B46D0CA1-B279-4D1D-BFE7-2C31228831A2}', 'General Clinical Phsysiology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{6A3C36DC-1739-4985-B29C-F5CF01644F61}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Genetics')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{19EDEC98-4EB8-4737-B422-F696C940C547}', '{9CE485AA-6464-4267-9DD3-8DFB0D4A224F}', 'Young Adult')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{F748C249-91DE-4C6F-AD1D-F72BBAF7954F}', '{6BF8FADF-8A9E-4834-B646-B9618034F663}', 'Dermatological Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{AF0781CF-46A0-494D-84DA-F7D111B47D9D}', '{2A566FF2-EBB7-4048-8806-32D1F2E9EB5C}', 'General Allergy')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{ED61C331-70EE-4F4D-BFD5-F7DEA880E379}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'COPD')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0B54FC1B-9915-49BE-8790-F89D20187EEE}', '{2E85B151-99B7-43A7-AF74-BBF88A035A45}', 'Paediatric Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{74C1C6F6-BDCB-4C5F-980B-F9011200560E}', '{D4370AD2-3178-4D4D-95EB-CD6CC595B80A}', 'Foetal Urology & Nephrology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{98300022-788F-425A-8A6D-F93A6E2E9B8D}', '{FDEAD469-73A0-4085-8197-A54895F93027}', 'Asthma')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{0C4BE6D5-472D-4FDC-A48A-F93BB5D744F0}', '{C48A499D-3AEA-4CAF-8129-F87D8B25122C}', 'Rapid Access ENT')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{53C98B3E-8E17-42AE-BB95-F9ADF95089EE}', '{1CC893A6-CEC2-4BCE-9182-752E84F8BD68}', 'Hair and Nails')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{38B1BA9D-6875-41F8-A070-FB892962109C}', '{8BEF0B3A-089C-4050-9112-9858237715A4}', 'Upper Limb')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{25DB0EEC-7DCB-43A7-98B0-FD8F6C5ACC77}', '{D4C506F3-C9DF-4256-A1D2-0CE2236BE14B}', 'Ophthalmic Emergency')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{389F5321-1B34-41C7-81DF-FEE5FFA11E1C}', '{6FF4D6D1-8596-4098-AD05-1D02D52BCA78}', 'Thoracic Oncology')
GO
INSERT INTO [dbo].[ClinicType] ([Id], [SpecialtyId], [Name]) VALUES ('{160DC496-3941-4373-A867-FF61D3802276}', '{4C17F1EF-9E4A-4C09-B199-1BBD25756665}', 'General Vascular')
GO
   
SET NOCOUNT ON
GO

/* Insert scripts for table: [dbo].[Provider] */
PRINT 'Inserting rows into table: [dbo].[Provider]'

INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{C5CD1D5E-6B17-441D-A4CA-0EBFB6330F97}', 'Inlaks & BudhraNI', 'Koregaon Park, Pune 411 001', 'Inlaks', '', 18.535027, 73.887466, 'Psychological Disorders, Ophthalmic Problems', '', '', 'Ambulance on Demand')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{FE4E5072-9D91-4DD5-AB4A-1639E34FE3A8}', 'Ruby Hall Clinic', '40, Sasoon Road, Pune- 411001', 'Ruby Hall Clinic Group', 'info@rubyhall.com', 18.53348, 73.877156, 'Psychological Disorders, Ophthalmic Problems', '', '', 'Ambulance on Demand')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{0DB1911A-AFD9-40E5-A708-182D0697B5FE}', 'Talera Hospital', 'Tanaji Nagar, Chinchwadgaon, Chinchwad', 'Talera Hospital', '', 18.625671, 73.784574, '', '', '', 'Ambulance on Demand')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{C9BC4EF2-634E-4D68-8EA6-432AC739DB39}', 'Shree Hospital', '2105 East South Boulevard', 'BBD Organization', 'bmc@boulevardbroken.org', 74.0156, 62.1155, 'Fractures, Accident Victims', 'Bone Recuperation', '', 'Residential Campus')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{C49C0269-0A39-42CE-BD12-837A140C6A0F}', 'Sancheti Hospital', '16, SHIVAJI NAGAR, PUNE - 411005', 'Sancheti Hospital', 'sanchetihospital@eth.net', 18.529859, 73.852994, 'ORTHOPAEDICS', '', '', 'Ambulance on Demand')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{B016A0CB-E552-4683-A0A1-850340D25A18}', 'Jehangir Hospital', '32, Sassoon Road, Pune 411001', 'Jehangir Hospital Group', 'info@jehangirhospital.com', 18.530673, 73.876662, 'Psychological Disorders, Ophthalmic Problems', '', '', 'Ambulance on Demand')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{410F66B9-D8D9-4DE4-96ED-92616AB7A173}', 'Bharati Hospital', 'Lal Bahadur Shastri Marg, Pune 411 030', 'Bharati Vidyapeeth', 'bharati@vsnl.com', 18.459463, 73.856747, '', '', '', 'Ambulance on Demand')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{A353AF78-2125-49D0-8E71-A0775D277DDC}', 'KK Hospital', 'Nagar Road', 'Cooper Inc', 'contactus@cooper.org', 18.7500, 73.7500, 'Obstetreatics', 'Physiotherapy, CBT, Sonogram', 'Do not perform MRIs', 'Cater to Emergencies')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{AA2664A7-2B57-4E30-8442-ADD8D9089867}', 'Konkar Hospital', 'Kasturbawadi', 'Co. Hsg Society', 'care@als.com', 18.63320, 73.86548, 'Paediatrics, Neurosis, Dental Problems', 'Surgically Assisted Orthodontics', '', 'Ambulance on Demand')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{0D5232E4-D8F0-4809-AFBD-D033CC2A430C}', 'Chandralok Hospital', 'B32 Champion House', 'BP Dev Authority', 'atmore@zeus.org', 18.50589, 73.50589, 'Spinal Disorders', 'Artificial Limb Installation', '', 'Transport Available')
GO
INSERT INTO [dbo].[Provider] ([Id], [Name], [Location], [Organization], [Email], [Latitude], [Longitude], [ConditionsTreated], [ProceduresPerformed], [Exclusions], [AlternativeServices]) VALUES ('{131903C6-8244-4A4C-9DCA-EBFCF8816EB4}', 'Acts General Hospital', 'Vikas Nagar', 'ACTS', 'contactus@andalusia.org', 18.60846, 73.60486, 'Psychological Disorders, Ophthalmic Problems', 'LASIK', '', 'Hostel Facility Available')
GO

SET NOCOUNT ON
GO

/* Insert scripts for table: [dbo].[ProviderClinicType] */
PRINT 'Inserting rows into table: [dbo].[ProviderClinicType]'

INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', '{C49C0269-0A39-42CE-BD12-837A140C6A0F}', '{CCE86BC7-3DE9-4570-9BE6-EB9BC3EAD3D5}', 3, 1, '10/07/2007 02:00:00 PM', '10/07/2007 03:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{01B40C1C-42F6-4340-BF24-0B6DC2FB12A4}', '{410F66B9-D8D9-4DE4-96ED-92616AB7A173}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 3, 1, '01/01/2007 10:00:00 AM', '01/01/2007 12:00:00 PM', 14)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{FE56161B-D3EF-468C-9028-0CD2194884D3}', '{0D5232E4-D8F0-4809-AFBD-D033CC2A430C}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 3, 2, '01/01/2007 10:00:00 AM', '01/01/2007 12:00:00 PM', 65)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', '{C49C0269-0A39-42CE-BD12-837A140C6A0F}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 3, 1, '10/07/2007 02:00:00 PM', '10/07/2007 03:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', '{C49C0269-0A39-42CE-BD12-837A140C6A0F}', '{77BAF929-DD4D-4480-9A80-1CBC54CA450A}', 3, 1, '10/07/2007 02:00:00 PM', '10/07/2007 03:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{D68A7F6F-7011-4557-8C02-1B54F8A3B638}', '{AA2664A7-2B57-4E30-8442-ADD8D9089867}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 1, 2, '01/01/2007 08:00:00 AM', '01/01/2007 10:00:00 AM', 14)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', '{0DB1911A-AFD9-40E5-A708-182D0697B5FE}', '{0D817901-14C5-4962-AC2F-22A5A1AECE01}', 3, 1, '10/07/2007 12:00:00 PM', '10/07/2007 01:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', '{FE4E5072-9D91-4DD5-AB4A-1639E34FE3A8}', '{3FA07FCE-731A-41BC-820A-1541C1C3DFEF}', 3, 1, '10/07/2007 10:00:00 AM', '10/07/2007 11:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{DB436540-4FA9-4220-A387-303D6CCEA02A}', '{0DB1911A-AFD9-40E5-A708-182D0697B5FE}', '{CCE86BC7-3DE9-4570-9BE6-EB9BC3EAD3D5}', 3, 1, '10/07/2007 12:00:00 PM', '10/07/2007 01:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', '{B016A0CB-E552-4683-A0A1-850340D25A18}', '{77BAF929-DD4D-4480-9A80-1CBC54CA450A}', 3, 1, '10/07/2007 04:00:00 PM', '10/07/2007 05:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{78D1498F-F887-4226-8002-45298A260F7C}', '{B016A0CB-E552-4683-A0A1-850340D25A18}', '{0D817901-14C5-4962-AC2F-22A5A1AECE01}', 3, 1, '10/07/2007 04:00:00 PM', '10/07/2007 05:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{5162C029-6458-42B4-BA09-656AE9CF4A6E}', '{0DB1911A-AFD9-40E5-A708-182D0697B5FE}', '{77BAF929-DD4D-4480-9A80-1CBC54CA450A}', 3, 1, '10/07/2007 12:00:00 PM', '10/07/2007 01:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{9636AFDF-BC28-4FB9-82E2-7D1460B8E51A}', '{C9BC4EF2-634E-4D68-8EA6-432AC739DB39}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 1, 4, '01/01/2007 10:00:00 AM', '01/01/2007 02:00:00 PM', 56)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', '{0DB1911A-AFD9-40E5-A708-182D0697B5FE}', '{3FA07FCE-731A-41BC-820A-1541C1C3DFEF}', 3, 1, '10/07/2007 12:00:00 PM', '10/07/2007 01:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{DE7A10B1-E5F3-4E26-A485-85F46D497801}', '{FE4E5072-9D91-4DD5-AB4A-1639E34FE3A8}', '{CCE86BC7-3DE9-4570-9BE6-EB9BC3EAD3D5}', 3, 1, '10/07/2007 10:00:00 AM', '10/07/2007 11:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', '{B016A0CB-E552-4683-A0A1-850340D25A18}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 3, 1, '10/07/2007 04:00:00 PM', '10/07/2007 05:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{65669836-2818-4490-984B-96DF3B00B8F6}', '{FE4E5072-9D91-4DD5-AB4A-1639E34FE3A8}', '{0D817901-14C5-4962-AC2F-22A5A1AECE01}', 3, 1, '10/07/2007 10:00:00 AM', '10/07/2007 11:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{3EB0E812-381A-496E-BD10-A40C41A91ECE}', '{C5CD1D5E-6B17-441D-A4CA-0EBFB6330F97}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 3, 1, '10/07/2007 08:00:00 AM', '10/07/2007 09:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', '{FE4E5072-9D91-4DD5-AB4A-1639E34FE3A8}', '{77BAF929-DD4D-4480-9A80-1CBC54CA450A}', 3, 1, '10/07/2007 10:00:00 AM', '10/07/2007 11:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{D5F8A78B-9766-471D-AA79-AF9F551A9564}', '{B016A0CB-E552-4683-A0A1-850340D25A18}', '{CCE86BC7-3DE9-4570-9BE6-EB9BC3EAD3D5}', 3, 1, '10/07/2007 04:00:00 PM', '10/07/2007 05:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{BE6446D9-D2FF-4713-B8DB-B8A8050C0607}', '{131903C6-8244-4A4C-9DCA-EBFCF8816EB4}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 2, 1, '01/01/2007 12:00:00 PM', '01/01/2007 01:00:00 PM', 42)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', '{C5CD1D5E-6B17-441D-A4CA-0EBFB6330F97}', '{CCE86BC7-3DE9-4570-9BE6-EB9BC3EAD3D5}', 3, 1, '10/07/2007 08:00:00 AM', '10/07/2007 09:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', '{C49C0269-0A39-42CE-BD12-837A140C6A0F}', '{3FA07FCE-731A-41BC-820A-1541C1C3DFEF}', 3, 1, '10/07/2007 02:00:00 PM', '10/07/2007 03:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', '{FE4E5072-9D91-4DD5-AB4A-1639E34FE3A8}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 3, 1, '10/07/2007 10:00:00 AM', '10/07/2007 11:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{6E43DDF4-B572-4A0E-A23C-CD600736607D}', '{C5CD1D5E-6B17-441D-A4CA-0EBFB6330F97}', '{3FA07FCE-731A-41BC-820A-1541C1C3DFEF}', 3, 1, '10/07/2007 08:00:00 AM', '10/07/2007 09:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', '{B016A0CB-E552-4683-A0A1-850340D25A18}', '{3FA07FCE-731A-41BC-820A-1541C1C3DFEF}', 3, 1, '10/07/2007 04:00:00 PM', '10/07/2007 05:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', '{C49C0269-0A39-42CE-BD12-837A140C6A0F}', '{0D817901-14C5-4962-AC2F-22A5A1AECE01}', 3, 1, '10/07/2007 02:00:00 PM', '10/07/2007 03:00:00 PM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{F479B006-10AE-4CAF-9841-EF0164553900}', '{A353AF78-2125-49D0-8E71-A0775D277DDC}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 4, 1, '01/01/2007 10:00:00 AM', '01/01/2007 11:00:00 AM', 42)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', '{C5CD1D5E-6B17-441D-A4CA-0EBFB6330F97}', '{0D817901-14C5-4962-AC2F-22A5A1AECE01}', 3, 1, '10/07/2007 08:00:00 AM', '10/07/2007 09:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{83190621-D256-465C-B146-F08254398286}', '{C5CD1D5E-6B17-441D-A4CA-0EBFB6330F97}', '{77BAF929-DD4D-4480-9A80-1CBC54CA450A}', 3, 1, '10/07/2007 08:00:00 AM', '10/07/2007 09:00:00 AM', 130)
GO
INSERT INTO [dbo].[ProviderClinicType] ([Id], [ProviderId], [ClinicTypeId], [SlotsAvailable], [SlotDuration], [DayStartTime], [DayEndTime], [WeekDays]) VALUES ('{906F9571-D0CE-4768-847A-F7D617506748}', '{0DB1911A-AFD9-40E5-A708-182D0697B5FE}', '{E11EC421-1262-4CF6-82E7-003ACD01AA26}', 3, 1, '10/07/2007 12:00:00 PM', '10/07/2007 01:00:00 PM', 130)
GO

SET NOCOUNT ON
GO

/* Insert scripts for table: [dbo].[Patient] */
PRINT 'Inserting rows into table: [dbo].[Patient]'

INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{ED0BEAAF-3B14-4DA9-BC15-30E8A514F36E}', '998898717', 'Mr', 'Jason', 'DSilva', 'M', '01/09/1958', 'C/ Araquil, 67', '29 King' + char(39) + 's Way', 'Seattle', 'WA', 'USA', '98105', 0, '(91) 555 22 82', 'jdsilva@hotmail.com', '{BCEA4208-FA51-4C94-9E93-7C60660F5E55}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{6CD87492-EFC6-489D-A370-4FDF25079E97}', '998898726', 'Mr', 'Adhip', 'Gupta', 'M', '12/08/1948', 'Walserweg 21', 'Brovallavägen 231', 'Seattle', 'WA', 'USA', '98122', 0, '0241-039123', 'adgupta@hotmail.com', '{0A8018CE-C670-4A1B-BD4D-29BB32250261}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{95E6E962-8860-45FA-B4B8-7C80DE266D5D}', '998898714', 'Mr', 'Srikanth', 'Sundaram', 'M', '03/04/1955', 'Berguvsvägen  8', 'Calle del Rosal 4', 'London', 'CA', 'UK', 'SW1 8JR', 0, '0921-12 34 65', 'ssundaram@hotmail.com', '{0A8018CE-C670-4A1B-BD4D-29BB32250261}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{B91AD300-3A40-4AAF-AD6E-86093575A38A}', '998898720', 'Mr', 'Naveen', 'Bansal', 'M', '08/30/1963', 'Fauntleroy Circus', 'Tiergartenstraße 5', 'Kirkland', 'WA', 'USA', '98033', 0, '(171) 555-1212', 'VictoriaAshworth@hotmail.com', '{72B26DEF-D38D-4F67-9B2A-E673830D61CB}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{E32B78DF-DA23-4027-ABAD-96B879BA9916}', '998898712', 'Mrs', 'Supriya', 'Sen', 'F', '08/30/1963', 'Mataderos  2312', '707 Oxford Rd.', 'Kirkland', 'WA', 'USA', '98033', 0, '(5) 555-3932', 'AntonioMoreno@hotmail.com', '{72B26DEF-D38D-4F67-9B2A-E673830D61CB}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{193D3D18-254F-4C46-A7C4-9772B368361F}', '998898728', 'Mr', 'Deepak', 'Gupta', 'M', '08/30/1963', '35 King George', 'Order Processing Dept. 2100 Paul Revere Blvd.', 'Kirkland', 'WA', 'USA', '98033', 1, '(171) 555-0297', 'AnnDevon@yahoo.com', '{72B26DEF-D38D-4F67-9B2A-E673830D61CB}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{055E4D05-017B-45FF-B03C-AFD6254A14B0}', '998898723', 'Mrs', 'Deepa', 'Nair', 'F', '07/02/1963', 'Hauptstr. 29', 'Viale Dante, 75', 'London', 'CA', 'UK', 'EC2 7JR', 0, '0452-076545', 'YangWang@hotmail.com', '{EA9D8831-143E-431A-8418-A15F490D3482}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{25E6E628-620C-421C-930E-B1192516A68E}', '998898713', 'Mr', 'Ravi', 'Srivastava', 'M', '09/19/1937', '120 Hanover Sq.', '9-8 Sekimai Musashino-shi', 'Redmond', 'WA', 'USA', '98052', 1, '(171) 555-7788', 'rsrivastava@yahoo.com', '{BCEA4208-FA51-4C94-9E93-7C60660F5E55}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{45C67FF6-BB63-4DA6-9B13-B50005B776F3}', '998898724', 'Mrs', 'George', 'Aikara', 'F', '05/29/1960', 'Av. dos Lusíadas, 23', 'Hatlevegen 5', 'London', 'CA', 'UK', 'RG1 9SP', 0, '(11) 555-7647', 'PedroAfonso@hotmail.com', '{72B26DEF-D38D-4F67-9B2A-E673830D61CB}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{C9CF57E8-3FC0-4B43-B91C-B6422D1FC292}', '998898711', 'Mrs', 'Priyanka', 'Singh', 'F', '02/19/1952', 'Avda. de la Constitución 2222', 'P.O. Box 78934', 'Tacoma', 'WA', 'USA', '98401', 0, '(5) 555-4729', 'AnaTrujillo@hotmail.com', '{EA9D8831-143E-431A-8418-A15F490D3482}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{428B0CC2-E10B-423D-B220-B6F4F6E0317B}', '998898725', 'Mr', 'Sushil', 'Joshi', 'M', '01/09/1958', 'Berkeley Gardens 12  Brewery', '3400 - 8th Avenue Suite 210', 'Seattle', 'WA', 'USA', '98105', 1, '(171) 555-2282', 'sjoshi@yahoo.com', '{BCEA4208-FA51-4C94-9E93-7C60660F5E55}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{D111A187-A4E0-4D30-A2A4-BCDBB1D8F019}', '998898721', 'Mr', 'Ashish', 'Sharma', 'M', '09/19/1937', 'Cerrito 333', 'Bogenallee 51', 'Redmond', 'WA', 'USA', '98052', 0, '(1) 135-5555', 'asharma@hotmail.com', '{BCEA4208-FA51-4C94-9E93-7C60660F5E55}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{F447968F-89A9-45FA-AADF-BD31692EB1EC}', '998898722', 'Miss', 'Pritika', 'Gulliani', 'F', '03/04/1955', 'Sierras de Granada 9993', 'Frahmredder 112a', 'London', 'CA', 'UK', 'SW1 8JR', 1, '(5) 555-3392', 'pgulliani@yahoo.com', '{0A8018CE-C670-4A1B-BD4D-29BB32250261}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{46F065CA-EC1C-466C-8B75-CADEFE1AFAAC}', '998898727', 'Mrs', 'Archana', 'Menon', 'F', '02/19/1952', '67, rue des Cinquante Otages', '203, Rue des Francs-Bourgeois', 'Tacoma', 'WA', 'USA', '98401', 0, '40.67.88.88', 'JanineLabrune@hotmail.com', '{EA9D8831-143E-431A-8418-A15F490D3482}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{B3A7F462-9F9B-44B3-9A2D-D1A2E0817553}', '998898716', 'Mr', 'Pranav', 'Chopra', 'M', '05/29/1960', '24, place Kléber', '74 Rose St. Moonie Ponds', 'London', 'CA', 'UK', 'RG1 9SP', 1, '88.60.15.31', 'FrédériqueCiteaux@yahoo.com', '{72B26DEF-D38D-4F67-9B2A-E673830D61CB}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{5C8F98F7-8F8C-4DEF-AC2C-DB0A4E3D9C6C}', '998898729', 'Miss', 'Deepti', 'Waani', 'F', '09/19/1937', 'Kirchgasse 6', '471 Serangoon Loop, Suite #402', 'Redmond', 'WA', 'USA', '98052', 0, '7675-3425', 'deeptiw@hotmail.com', '{BCEA4208-FA51-4C94-9E93-7C60660F5E55}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{6F48021D-CD6C-4D2B-AC1B-E4CF51BE4261}', '998898710', 'Mr', 'Bipin', 'Kadam', 'M', '12/08/1948', 'Obere Str. 57', '49 Gilbert St.', 'Seattle', 'WA', 'USA', '98122', 1, '030-0074321', 'bkadam@yahoo.com', '{0A8018CE-C670-4A1B-BD4D-29BB32250261}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{51B051E7-6F5B-4AD0-9C0B-F05E2B334DF7}', '998898719', 'Mr', 'Aryan', 'Sharma', 'M', '02/19/1952', '23 Tsawassen Blvd.', 'Av. das Americanas 12.890', 'Tacoma', 'WA', 'USA', '98401', 1, '(604) 555-4729', 'ElizabethLincoln@yahoo.com', '{EA9D8831-143E-431A-8418-A15F490D3482}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{469C409E-A11A-4BE4-AD01-F1AF6890E3FB}', '998898718', 'Mr', 'Siddharth', 'Rathod', 'M', '12/08/1948', '12, rue des Bouchers', 'Kaloadagatan 13', 'Seattle', 'WA', 'USA', '98122', 0, '91.24.45.40', 'srathod@hotmail.com', '{0A8018CE-C670-4A1B-BD4D-29BB32250261}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{0A4A2C30-7AF1-476B-84E4-F6D8CBD3F07A}', '998898715', 'Mrs', 'Heena', 'Kaur', 'F', '07/02/1963', 'Forsterstr. 57', '92 Setsuko Chuo-ku', 'London', 'CA', 'UK', 'EC2 7JR', 0, '0621-08460', 'HannaMoos@hotmail.com', '{EA9D8831-143E-431A-8418-A15F490D3482}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{50B07541-30E0-4897-B44E-0B6E9AAEB916}', '998898815', 'Mr', 'Vikram', 'Rajkondawar', 'M', '7/2/1974', '25, West Avenue', 'Opp Connaught Place', 'Bangalore', 'KA', 'IND', '98401', 0, '9890564596', 'vikram@microsoft.com', '{12015779-4F3D-4817-8C53-45DA8D9242DE}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{C880339E-EF6C-43ef-9B2D-B5F7D427C08F}', '998898816', 'Mr', 'Kuljeet', 'Tatla', 'M', '11/3/1984', '501 Quartz', 'Nyati Empire, Kharadi', 'Pune', 'MH', 'IND', '411014', 0, '9960662596', 'ktatla@unknown.com', '{12015779-4F3D-4817-8C53-45DA8D9242DE}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{D909966D-DF19-4e5c-A58B-74ACB35C8C2A}', '998898817', 'Mr', 'Bharat', 'Garg', 'M', '06/15/1974', '81 Ruby', 'Oasis, Wanowri', 'Pune', 'MH', 'IND', '411015', 1, '9860426596', 'bgarg@somecompany.com', '{12015779-4F3D-4817-8C53-45DA8D9242DE}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{7C581235-9892-4bca-BECE-789E7B2F3E65}', '998898818', 'Ms', 'Snehal', 'Tikekar', 'F', '02/07/1980', 'Lunkad GoldCoast', 'Viman Nagar', 'Pune', 'MH', 'IND', '411014', 1, '9325017159', 'stikekar@somecompany.com', '{12015779-4F3D-4817-8C53-45DA8D9242DE}')
GO
INSERT INTO [dbo].[Patient] ([Id], [PatientNo], [Title], [FirstName], [LastName], [Gender], [DOB], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [ConsentToCallback], [ContactNumber], [Email], [ReferrerId]) VALUES ('{6AF27030-BF94-471b-BFBD-17A637B9EA0F}', '998898819', 'Mr', 'Vaidyanath', 'Srinivasan', 'M', '08/19/1978', 'Richie Apartmentns', 'Aundh', 'Pune', 'MH', 'IND', '411015', 1, '9860656269', 'vsreenivasan@somecompany.com', '{12015779-4F3D-4817-8C53-45DA8D9242DE}')
GO

   
SET NOCOUNT ON
GO

/* Insert scripts for table: [dbo].[Referrer] */
PRINT 'Inserting rows into table: [dbo].[Referrer]'

INSERT INTO [dbo].[Referrer] ([Id], [FirstName], [ClinicName], [LastName], [Email]) VALUES ('{0A8018CE-C670-4A1B-BD4D-29BB32250261}', 'Rahul', 'Sancheti Hospital', 'Khedekar', 'rkhedekar@SeattleGeneralHospital.org')
GO
INSERT INTO [dbo].[Referrer] ([Id], [FirstName], [ClinicName], [LastName], [Email]) VALUES ('{4F36E555-7FB0-4D31-9A59-43E8E76A5421}', 'Gautam', 'Sarlok Hospital', 'Kumar', 'gautamk@sarlok.com')
GO
INSERT INTO [dbo].[Referrer] ([Id], [FirstName], [ClinicName], [LastName], [Email]) VALUES ('{BCEA4208-FA51-4C94-9E93-7C60660F5E55}', 'Gaurav', 'KK Hospital', 'Sinha', 'gsinha@RedmondGeneralHospital.org')
GO
INSERT INTO [dbo].[Referrer] ([Id], [FirstName], [ClinicName], [LastName], [Email]) VALUES ('{EA9D8831-143E-431A-8418-A15F490D3482}', 'Aftab', 'Jehangir Hospital', 'Ali', 'AndrewFuller@TacomaGeneralHospital.org')
GO
INSERT INTO [dbo].[Referrer] ([Id], [FirstName], [ClinicName], [LastName], [Email]) VALUES ('{72B26DEF-D38D-4F67-9B2A-E673830D61CB}', 'Augustin', 'ACTS Hospital', 'DSouza', 'JanetLeverling@KirklandGeneralHospital.org')
GO
INSERT INTO [dbo].[Referrer] ([Id], [FirstName], [ClinicName], [LastName], [Email]) VALUES ('{034F7CD0-57C1-4E1B-AFBD-EC53B9C20804}', 'Aurobindo', 'Bharati Hospital', 'Chaudhary', 'StevenBuchanan@LondonGeneralHospital.org')
GO
INSERT INTO [dbo].[Referrer] ([Id], [FirstName], [ClinicName], [LastName], [Email]) VALUES ('{12015779-4F3D-4817-8C53-45DA8D9242DE}', 'Sanjiv', 'Ruby Hall Clinic', 'Chawla', 'schawla@somecompany.org')
GO
   

SET NOCOUNT ON
GO

/* Insert scripts for table: [dbo].[Slot] */
PRINT 'Inserting rows into table: [dbo].[Slot]'

DECLARE @Separator nvarchar(2)
Set @Separator = '-'

DECLARE @DateYearString nvarchar(4)
Set @DateYearString = DatePart(yy, Convert(DateTime, GetDate()))

DECLARE @DateMonthString nvarchar(2)
Set @DateMonthString = DatePart(mm, Convert(DateTime, GetDate()))

DECLARE @DateDayString nvarchar(2)
Set @DateDayString = DatePart(dd, Convert(DateTime, GetDate()))

DECLARE @DayTimeString nvarchar(10)
Set @DayTimeString = '10:00:00'

DECLARE @DateString nvarchar(20)
Set @DateString = @DateYearString + @Separator + @DateMonthString + @Separator + @DateDayString + ' ' + @DayTimeString

INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{143F8C8F-93CA-41B0-9A60-019CC0E4046A}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(day, 21, Convert(DateTime, @DateString)), DateAdd(hh,1,DateAdd(day,21,Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{043F8C8F-93CA-41B0-9A60-019CC0E4046A}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(day, 21, Convert(DateTime, @DateString)), DateAdd(hh,1,DateAdd(day,21,Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D1E1D0A8-6188-40B1-AE3D-01E9CED7D546}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 5, DateAdd(day, 21, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 21, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A3BC1EE8-A705-4CED-8DC8-02C258B19777}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 5, DateAdd(day, 21, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 21, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{224806AD-CBAC-40EB-B61A-030F9330026E}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{57571492-512D-453F-B87B-0315028D05CF}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 8, DateAdd(day, 21, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 21, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{3288998F-8E35-4B84-9FCF-03CC6FBE4F6F}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 2, DateAdd(day, 21, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 21, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2DA39730-CDFB-4322-BF86-043151EAE5F2}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{03770929-0792-48D5-AC6B-044652A1CCAD}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{528E9140-DBFA-495C-817E-04EB98914D29}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 1, DateAdd(day, 21, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 21, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{131BA9C3-455F-456B-9629-058E5E61F229}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BA7F59B7-CD5B-404C-A5EB-06E1F0FBA0FB}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 8, DateAdd(day, 16, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 16, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A30493C0-E672-4B89-855B-07E47BF31B71}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 1, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{1A081E3A-9682-4B48-88FC-0839D8DE5CDF}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 8, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E36DD2C5-83B4-48B6-8AC0-0860A7B2B30C}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4EB43709-A6A0-4707-A70D-08A4FFC3A1C1}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{474279BB-CFBF-4E9C-8A31-08BCE4748660}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 8, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F9840F9A-CF29-47E4-B722-092B44DF9827}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{86268481-4B13-4372-A349-0A36887FD82E}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{7AA63B48-1E77-4882-BA54-0A9B2AC21E5B}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{AE8510D1-3B84-4063-A1C6-0DEBDDF6F3B3}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 1, DateAdd(day, 21, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 21, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2FB69AED-95EB-415F-82FA-0E38739C83DF}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 8, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{85442433-4A88-4D9C-AA35-0E9C6A70FF72}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 1, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B1AC330B-B49F-447A-B4C7-0F46710091FB}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 2, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4B57F171-B361-433B-9B12-102EC933672E}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C62A6038-58AF-467B-8984-10904DFFE8B0}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{61BDCF66-2599-4DCA-B71B-1188DC1D4691}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2C0BED63-0F26-442C-BC5E-130F52127414}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{113B3CBF-8123-457B-8896-13DEB0D15D52}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{82A1B613-5239-4A42-A5AA-14BD03448CFE}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B29CBB04-9182-47A7-994A-153F49FA3839}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4A9662EC-BAE7-4AD5-A398-168F4C60F91F}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 8, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{173EE3C4-95F6-48EA-A15B-16FABE5353FD}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BDCB076B-1BE6-4729-9A71-173BAAAB39E9}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{70AB61C9-1D01-42B7-BFCE-1787E7559AA2}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{AF97C721-CA08-4C42-8612-17FD35EDDCEE}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{58C2AE8D-C0CA-4BF7-8B8F-1AD06867A992}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 11, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2803D4CF-C913-45E8-BB8F-1AE38D605AEC}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{78B8BE1F-433A-4F92-9627-1C58CD7C0BB9}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{3A3C5262-90B9-46E7-9C6A-1CB5E9B5657E}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{15342AA8-9315-44B7-AD8A-1D3345570ABA}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B77FF1E9-D451-48E8-B6FD-1F2C291F58FD}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F2261832-ADDF-4E6E-AF62-20833B32B883}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{46275E9B-CE87-4EF8-A0A2-20A7A4805DE5}', '{FE56161B-D3EF-468C-9028-0CD2194884D3}', DateAdd(hh, 10, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C2E9A7B4-2E5E-4309-9A2F-20E15B07B26E}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 1, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{7A80BB73-96C3-46F6-BABC-21A9D01FCFFC}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{AFD7B683-96D0-4EE9-BCF2-21B50FB6B305}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{0928846A-5E83-4E8C-BCF5-273656AFA6EC}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CD30F95E-55FF-4840-A423-282FFB116DDE}', '{BE6446D9-D2FF-4713-B8DB-B8A8050C0607}', DateAdd(hh, 8, DateAdd(day, -5, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, -5, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{7856BD91-094B-4056-87CF-28AC23417048}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 2, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A1108004-7801-4DCB-A480-2A7B224A1343}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{3DBE3213-1A56-42DD-97DE-2ABA1F8AB735}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B833732D-AF1D-401B-A41B-2B3A12779C4E}', '{FE56161B-D3EF-468C-9028-0CD2194884D3}', DateAdd(hh, 1, DateAdd(day, -2, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, -2, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{006D2A3E-6A5E-43DB-BF41-2BEC58C23583}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 1, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E60C466B-4810-4C54-85B6-2C890689D788}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 11, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CC7922C8-70FC-43EB-87BC-2D0F709C3EA0}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BC21E176-664E-4B61-8C3E-317EE3DE0B38}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 10, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{31EA3C82-FF67-46D8-811C-332D0FFC354F}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5533EDB6-8348-4A1A-955C-3479BD5F4CD3}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C8A09E18-CD02-45CC-813C-351ABA5173BC}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C9FB65C0-A849-4D9A-B1EE-38479EBA2D36}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 1, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{65409F13-B7EE-4051-A82B-3A19B6E996B0}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 1, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D93779EC-8299-41E3-A789-3A2A983C1688}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 11, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F16BF56F-BD29-4307-A4B6-3B0180D3AF5B}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 4, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{023C8190-A8EF-4813-AE51-3B0E1FAF6337}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D77E8AF7-D446-4946-8B1D-3B41E47E4719}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 11, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{221B1273-A23D-49D7-BB64-3C38AD8819C0}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C645D52E-51BE-4FA5-A230-3CBD0B32DF08}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{3C6E1E0B-E92F-4000-A14B-3D2221E1A887}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{42373BB0-FBD9-4057-AC66-3D6D07DABD1B}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4D50A279-EAC7-4305-AAE8-3E4D51E0CA1F}', '{9636AFDF-BC28-4FB9-82E2-7D1460B8E51A}', DateAdd(hh, 7, DateAdd(day, 26, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 26, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B9B404FC-6328-4AD7-AFB8-3E7C19387A64}', '{FE56161B-D3EF-468C-9028-0CD2194884D3}', DateAdd(hh, 4, DateAdd(day, -4, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, -4, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{6EC59D40-BB07-4977-B443-410D83D4AF9D}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 4, DateAdd(day, 10, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 10, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{FB2AFAB0-8E07-4310-BCA1-423365AD7026}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 1, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{89057088-248A-4D52-BF7C-4293595B3CE1}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F5F19F23-F715-4FB9-BC65-42AC519FC47E}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{45B235FA-3DB9-4967-891A-43135A3B016F}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E6CCF09A-F1E6-462A-862D-46849C84DC8F}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{9021F080-C908-4F29-BBA1-474F46D63B60}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D0C5D4E3-C789-469E-9B36-4835395EE446}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{77912721-7060-40E7-8178-4938BFA1A46E}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 1, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{12F46D73-39EF-4DD1-BE30-4AD2A2849502}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BBCB9ED3-1721-4B36-9B64-4AE09842C821}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 10, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E9B74448-A214-4416-9D88-4BE870C2D2FE}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 2, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F969E02E-53FF-487F-8F43-4D756FE03DC5}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{EC6A7A8D-AC47-4475-9044-4D91EB886BC5}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{1244CDBB-D8A9-451B-BBA6-4F8C6B599943}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{930C5D22-8110-4C5D-A88F-50298BB875B8}', '{9636AFDF-BC28-4FB9-82E2-7D1460B8E51A}', DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{69DAF113-3186-4552-8F00-50B9289DA9CB}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 1, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C69D9C70-3142-4E31-8274-511274633FE1}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 1, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{AFA4B31C-CE4B-486D-AEF5-541D2F228D43}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{37EAD5CE-F269-4D74-8695-54D06B5DEDD4}', '{D68A7F6F-7011-4557-8C02-1B54F8A3B638}', DateAdd(hh, 4, DateAdd(day, -3, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, -3, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B6A046C2-AC2E-40AF-AAE8-564796B20C92}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{71E1A11D-E9F0-48A7-A256-56A457ED388E}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 4, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{47D217AF-1F22-4327-B404-57159D9F5C2D}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D6F2B5B2-1448-47C1-B779-5728D28E7EE9}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F0B2E488-0497-496F-A320-57C9FF391D6A}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{68A83300-722A-4655-969F-59CD9B729CD9}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5551C9C0-89D7-49B7-91BE-5C941FFEDAA1}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{62AF8C37-2815-470E-8BEB-5CB948057275}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F0C6022C-6521-485A-9788-5CE6D3838058}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 4, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{9CCDC3AE-6BC8-4BD1-A7B8-5CF316D7E47A}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 8, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{6E3B03B4-114A-4712-B0A5-5F88C8BC0216}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B0A7A4AA-3D30-4501-A748-60CA7F7CFC41}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F8839A65-A928-462A-B258-6326B8B9C029}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{62B14184-AB75-4CF9-97D3-644D0BEDD86D}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 4, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BD7A643C-0C79-4808-A04C-6484510AE0EF}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E3356379-B062-4F56-9E52-6880D076D16F}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{DE10E2AD-A8A0-4F49-9430-68BB21183278}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{241B4D7D-6476-4B13-9B52-69010A7E1128}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A9976B15-94C0-4192-856C-6B428AD48772}', '{BE6446D9-D2FF-4713-B8DB-B8A8050C0607}', DateAdd(hh, 10, DateAdd(day, 11, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 11, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C0936BE3-5C32-4ADB-9F93-6D1133725FC2}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{815A830A-179B-4D90-99BF-6D1763CEF0DC}', '{9636AFDF-BC28-4FB9-82E2-7D1460B8E51A}', DateAdd(hh, 2, DateAdd(day, -1, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, -1, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{9BDCC907-AF33-43A4-9BFD-6D3B55CBFBCF}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{7BF034F7-A49A-4486-A87F-6D54A5CC9472}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5E7BBEC8-9753-4A1A-ABAC-6DDC63F72F1B}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{DCB0965E-51DE-498E-8A23-6E8898474FDB}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{389344EE-FA99-45C0-8153-6E8F07F44E54}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BFA3CCAF-10B3-4629-BF7E-6F32E5DF0EC7}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 1, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E154B63B-1B61-4093-B661-6F334EF96B20}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{6483ED9D-E41B-4098-9B27-6F67B6984929}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D90779FB-39D9-427A-81DC-6F8650F0095C}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 8, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{136DFA1E-46DD-46FB-BA7C-708C9FF5C9C3}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 4, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{66617073-85AA-4EE0-9682-70CE2183A803}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{3C3137C9-27D7-4D7D-9462-70DD3433F979}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{24BDC841-BAD9-4BD6-8019-71399F4FCE54}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D24626E1-DDB3-44DD-8C55-72372FE9274F}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{68B101E5-FA90-454A-863C-73D79ABF94B2}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BC705345-F9AC-46F2-A6EA-749DF7AA7341}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{6D4C1430-C82B-487C-9724-74ADD21DD2D7}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{05EEAF1A-D67C-450B-86DA-75222DFCA183}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F9C0C294-77DD-4196-9E68-762377BEB23C}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{ED36E895-1FCF-4401-8DF6-772E61ADB402}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D2E786E4-EE94-4451-B8E7-7BA23A434C44}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{DDCE1A70-9018-4D4B-B238-7D32740318A8}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{09757E53-D9AD-44B5-858E-7D33B6065856}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{1349DA66-5837-4E66-9945-7DA0F5865576}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{73A986FE-269E-4812-9DCC-7FF389227589}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CDC1D887-BA20-4706-8F42-8024AD8663D3}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2FDEEFF7-F322-4792-87E8-81B9036F8D4B}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 1, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{7EEB87EC-2121-4F0B-9108-83231D077B1E}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 4, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E3AEBEEF-62FB-4C99-BF9C-83F1CE2653BB}', '{D68A7F6F-7011-4557-8C02-1B54F8A3B638}', DateAdd(hh, 4, DateAdd(day, -7, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, -7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{DFD8C6D3-1919-4285-BE42-8531F3C1C9EC}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CDF7C0FC-43BF-4C33-9CF3-8555F46BC85C}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{0A866E48-C75F-4664-B947-87C78B7B297D}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 1, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A9DA3266-F76C-4AC7-8252-882108CC7B8D}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F14021AD-984E-4C6D-8012-899EB6535AAB}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D24A0DDC-3EC1-4957-A907-8A06C0D52A49}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D2DD1169-0800-4A73-9EB2-8C83D844DF12}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 1, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{40927ACA-7B11-449F-85AD-8CE02644C7ED}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 1, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B9207739-F85F-43B2-80BA-8DA9FA285E5E}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{83A37E70-17F8-499F-845A-8DAD41645385}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{68FD6CD3-8DCA-40D0-9916-8E222427EB98}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E6FD8875-B6E8-4CB2-93F7-8E2FA5D0FE78}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C445C6CA-B676-4D1D-9A02-8E896F7EFDDB}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{0AC89379-D3E8-42F5-9799-8F3027CA11C7}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C9B7B305-A89E-4374-ABDC-907173375D04}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CC2A7985-3692-4F40-B093-90A2C17BCBA3}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D2096818-E672-49BB-B90C-9107EEB182D9}', '{FE56161B-D3EF-468C-9028-0CD2194884D3}', DateAdd(hh, 10, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CBCE7126-F576-41E4-9B24-9161EA9BFC63}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F49675C6-9455-4456-938C-91EACFB316CC}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A810E72E-8D92-4963-AAA0-92DF2FC754C2}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 12, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C6D3968D-AD96-4E6C-8CC8-948FA31F473C}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A9D76FC5-0FAE-49CA-A5CD-97E920654BF6}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{237B99D2-1723-43D6-9092-9952B43F7698}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5E4DD483-B299-49B8-A790-99727DD82E5C}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C19672EA-47A0-477F-814A-9AC9A8962386}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E39C71A1-3D0D-461C-9C49-9AEFE60DC4D8}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{62507434-6E28-4754-9119-9C52FFA8B805}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 1, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{09CAD7F8-5BDA-46F7-8F72-9D0CC7740BE5}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 2, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F94217A2-EB40-44A3-8440-9DA623CAE89A}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CFAE3F73-D0DB-4D21-A60D-9DFC19A42DD7}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{EE638C88-FE56-48E2-B65A-9E2B0AE7F1FD}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E024E585-F3DE-469C-BEC8-9E488484AEAA}', '{A42EA95C-56DA-4F68-AA1C-82559E04F90A}', DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{7A9B893F-2106-4FB3-B1E1-9F4BCE7C8361}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BCF0CE33-BBCA-4EDA-A5AE-A00F069190CF}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{6D8243C6-D93C-421C-88AA-A2DFBB8C0AB8}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E1F2796B-8703-4619-907E-A31606CE3D78}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C2BFD5D7-156A-4B85-91B3-A3A3C6AC5018}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2510D629-9BDC-4053-8C79-A4EA44BD5AA4}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{87948502-0F3A-473E-922D-A5373CFA9336}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 1, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{95C3A561-533A-48AE-B01A-A7DB26AE8E06}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{7A0952DE-5C22-4C06-B360-A822D99B991D}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{02C20CAA-9BCF-49CF-A62E-A8579FC22F67}', '{FE56161B-D3EF-468C-9028-0CD2194884D3}', DateAdd(hh, 4, DateAdd(day, 11, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 11, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{60A8C898-F1FF-4C82-B471-A8A0B8D69A99}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{3558E48B-1AD4-45C2-85B7-A91147383C89}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{FE726D17-33BB-4DD0-9DAA-AAADDD65457B}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 10, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{465D886F-F8C4-42F8-8711-AC17A2F77DA4}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{AF727B31-BE1A-44BB-8E9E-ACEC1601D255}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{45AADBD0-7083-4B01-BBAB-ADB8118FC704}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{918537E9-8F9F-4EF4-9AF4-AE2643A72471}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{6B80EAF8-6169-4117-A23B-B0C3455553E4}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 2, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E94AA225-5F6C-4902-A085-B0D6EE2EA783}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 8, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{188B961E-AC13-4104-92AC-B15072BF715D}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 12, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4AB0E4D3-1B6D-4D8E-B94E-B622A6664FF8}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D0470133-1586-4CC8-8AE2-B65FC8BB4781}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D7711939-CD0D-4D90-83D1-B6F07A8D59B6}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{8DFB06C1-1F5F-4017-B6A7-B7E5F70D68E3}', '{BE6446D9-D2FF-4713-B8DB-B8A8050C0607}', DateAdd(hh, 8, DateAdd(day, 23, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 23, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{28E1DB1A-BAE0-46D9-95C4-B92D41BE6EDE}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{63F4B4B1-FCD0-4BF9-9AEA-BA488E281585}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{1AB18211-0E4D-4EB1-84CE-BA7E4F25B0E1}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{19891AE8-8F07-4AD5-A665-BBC5B373D896}', '{5B43D1A3-DA0D-4C5E-9F13-13D750808A9B}', DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5933F8A6-A450-4AF2-8809-BBD492B50EFA}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{12E81125-DAE1-45BA-9153-BCF5DB28C9A4}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{15B941CD-0A69-404F-AD1E-BDDEB4BF71E6}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{626A9347-BB82-4D90-AABD-BE883373C77D}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{EA894613-4BC6-4CA7-AFCC-BFB8AA6731E9}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2CD35783-0171-4D21-AC2F-C0350E0C7D85}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5F686465-0F31-4234-84AF-C1A6B82EE006}', '{BE6446D9-D2FF-4713-B8DB-B8A8050C0607}', DateAdd(hh, 1, DateAdd(day, -9, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, -9, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{225F77F8-2B97-40D8-9FB3-C21AE467867C}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5F318F96-2107-47FD-A6D3-C227020E48DE}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{3A919355-F4E4-4959-8109-C35621D8EB01}', '{DE7A10B1-E5F3-4E26-A485-85F46D497801}', DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{21C60A3D-5DAB-4222-832F-C413857FBD99}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C5DFCB0D-86C6-42BA-AAA7-C48946AC861D}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{9C3E2DC3-2F99-403F-B51E-C762D44614BB}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 8, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{6F8EFD92-7BC1-471B-8BE1-C7B34C1A54DD}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{383298D4-C342-4906-99E6-C97C40B72B43}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F69E0FEE-1619-4ED8-882F-C9CC1DD5EB16}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{E93C115D-12B6-4F69-A148-CA56A1FC19D4}', '{767D3E43-E975-4EBB-8FF1-CC627B591F7F}', DateAdd(hh, 12, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CEF015A6-1F70-4C88-9E26-CB06271694AF}', '{BC372FE2-3908-41C1-8B90-D5E2B11253CB}', DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{993781B5-38FB-45F5-A2B8-CD220E234AED}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{939CEC2D-0856-4760-8F8D-CD6BD6945C48}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{468168F6-D170-426B-923B-CDC325108740}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{DB271805-18D9-4BE7-A564-CEA1D01E8617}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5D332E68-3397-46AA-850D-D05CAD2FB2CB}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 6, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{1C7CC0B2-1661-4005-8DCA-D27175FB237A}', '{3EB0E812-381A-496E-BD10-A40C41A91ECE}', DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5E74CE58-7CE8-47DF-A2F8-D2DFA4F9BCD6}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4F3FA14B-B0C1-4AEF-A61D-D3AD95D6C46B}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 7, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C7AADAF8-1A43-4723-A9AF-D46F9ABFA65E}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CFE52435-C367-41D4-AE4B-D6E9FE2DEFDF}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{BAD6CC7A-4EBE-40E0-94D4-D78147BF6336}', '{A42711CD-D1AB-49DB-90FA-03C18CEDBA3B}', DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A18C4CC1-DFF1-4FB3-A70E-D80AB1A13E14}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{720F53F9-45D6-4755-B2A3-D9A984CBDBCF}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 12, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{196C3A54-C880-4706-89DF-DA223939D3B2}', '{906F9571-D0CE-4768-847A-F7D617506748}', DateAdd(hh, 6, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{AB335B5E-3019-4556-8C24-DE68EF874028}', '{56F6AB1E-C5DB-432B-A430-A70F1D5D9DD0}', DateAdd(hh, 1, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{1EA95EEA-3D65-4FCF-852C-E20F99C13F92}', '{C1056911-4F71-4F2B-BB59-EFBAC4BA263B}', DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{056FAF6D-631A-445F-9563-E3534B06E986}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 4, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{FA746FAC-1754-4AE0-9EF0-E44BA7D39466}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{1E944F17-7069-41AF-AB9D-E54A435EF835}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 10, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C8872DCA-AE8F-49B4-8D24-E622BDA7EED1}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 13, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{46BC3970-6BC0-435D-B315-E6A44D4BE2C6}', '{8F3414E1-CEFE-4C4B-BF75-EAD166F31885}', DateAdd(hh, 7, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{FF511D7C-9792-4EDB-95A1-E6A46FB43D52}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 5, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{0FBB4F28-D261-42C3-A19A-E6BA9CA44F55}', '{5162C029-6458-42B4-BA09-656AE9CF4A6E}', DateAdd(hh, 2, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{74B7F662-9907-43A0-9A7F-E878D8D35138}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{C8B77EEF-3145-4958-AD52-E91834526083}', '{D68A7F6F-7011-4557-8C02-1B54F8A3B638}', DateAdd(hh, 8, DateAdd(day, -3, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, -3, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D760E7DF-0FF3-416E-813C-E9EADB78912E}', '{6E43DDF4-B572-4A0E-A23C-CD600736607D}', DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A1EBEC9A-DCDF-4A9D-88C3-E9FB84EDCF3A}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 7, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{AA7A357A-9098-467B-94F0-EA0062800CF8}', '{AA7A6B74-14E3-43C5-9621-8B2D1F5814D7}', DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{A060EFE4-BBC7-449E-AEF6-EA19BC1F9D60}', '{D5F8A78B-9766-471D-AA79-AF9F551A9564}', DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 9, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{12EFD2AE-3D61-4AC1-BB89-ED344A047D10}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2DC3C39B-AB2E-4C1C-B2C8-EF2578997A18}', '{BE6446D9-D2FF-4713-B8DB-B8A8050C0607}', DateAdd(hh, 4, DateAdd(day, -15, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, -15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{28DEF8A6-29BC-46BF-BF65-EF410E0D8FE8}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 2, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{F18D6CE2-D908-46E6-AC81-F159BBBF7F31}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 2, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{DD19B99B-E3C7-43D0-AFDA-F276E880E9EE}', '{AD4EE832-73E5-4CE4-A0F1-CBE68AE15BDA}', DateAdd(hh, 9, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{2E4D3E64-2B92-47FA-BAE8-F41E0EC6CF65}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5BE9D1D2-D4F2-48D0-8B05-F4661596F58C}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 3, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D7CDF58F-648F-4F44-8B04-F4C8B6BC43C8}', '{83190621-D256-465C-B146-F08254398286}', DateAdd(hh, 3, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 4, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{83617CAD-5B7F-4553-96AF-F58794F450E2}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 11, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{5273F217-88DA-43EB-A8A3-F5C34B5CC690}', '{829221DC-4B09-4B4E-8425-1BC47A68D5CB}', DateAdd(hh, 9, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 10, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4BD898C6-6150-409C-B0AB-F5D6916362AD}', '{DB436540-4FA9-4220-A387-303D6CCEA02A}', DateAdd(hh, 4, DateAdd(day, 15, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 15, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{4AD7C5DD-24C8-4346-A82F-F6D3FEF7606E}', '{09C6DAD2-DBF7-454F-8909-383ADFB13CF0}', DateAdd(hh, 4, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 5, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D57817C4-9A61-4FF5-B418-F7F9C8FCEE37}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 1, DateAdd(day, 19, Convert(DateTime, @DateString))), DateAdd(hh, 2, DateAdd(day, 19, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B1DEEE1A-D40C-4ECC-985B-F94C7D44DB99}', '{78D1498F-F887-4226-8002-45298A260F7C}', DateAdd(hh, 7, DateAdd(day, 12, Convert(DateTime, @DateString))), DateAdd(hh, 8, DateAdd(day, 12, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D7A491D3-8E3C-4F46-8FC7-F9610B6C5BB0}', '{F3FCB805-7FEC-4323-869F-1DD054AD6CC1}', DateAdd(hh, 10, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{CDC716F9-291E-45DA-91A3-FADE0F5CB002}', '{65669836-2818-4490-984B-96DF3B00B8F6}', DateAdd(hh, 10, DateAdd(day, 25, Convert(DateTime, @DateString))), DateAdd(hh, 11, DateAdd(day, 25, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{D72A1898-E240-41E3-8137-FB80E4E23DA9}', '{B1784A7F-4EFB-4B25-B3C3-C32215DB985B}', DateAdd(hh, 11, DateAdd(day, 7, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 7, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{B58D76BC-5727-4FB7-B6AD-FC4EF612AE0D}', '{D68A7F6F-7011-4557-8C02-1B54F8A3B638}', DateAdd(hh, 11, DateAdd(day, 18, Convert(DateTime, @DateString))), DateAdd(hh, 12, DateAdd(day, 18, Convert(DateTime, @DateString))), NULL, 1)
INSERT INTO [dbo].[Slot] ([Id], [ProviderClinicTypeId], [StartDateTime], [EndDateTime], [UBRN], [Status]) VALUES ('{32481896-7CF2-4D40-A7E8-FD0DD65490F8}', '{462CFCD6-A9A2-4493-B4D2-1A3643C5EC5E}', DateAdd(hh, 5, DateAdd(day, 6, Convert(DateTime, @DateString))), DateAdd(hh, 6, DateAdd(day, 6, Convert(DateTime, @DateString))), NULL, 1)
GO

INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411001','IND','18.522967','73.875359')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411040','IND','18.500481','73.885897')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411014','IND','18.567138','73.917121')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411015','IND','18.592542','73.874409')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411003','IND','18.555223','73.848049')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411004','IND','18.518262','73.839669')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411005','IND','18.527382','73.850599')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411006','IND','18.56015','73.89201')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411007','IND','18.557903','73.807182')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411008','IND','18.539322','73.813861')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411012','IND','18.580196','73.833827')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411013','IND','18.508081','73.917604')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411017','IND','18.610906','73.797011')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411018','IND','18.632745','73.808684')
GO
INSERT INTO [dbo].[ZipCode] ([Zip],[Country],[Lat],[Lon]) VALUES ('411019','IND','18.639264','73.796322')
GO


