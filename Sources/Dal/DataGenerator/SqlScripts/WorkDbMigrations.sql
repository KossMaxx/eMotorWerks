﻿USE [{0}]

CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201802140505325_vasiliy.stan_eaff28a0e72042c1b8ee27cde7ef12e5', N'Dal.Migrations.Configuration',  0x1F8B0800000000000400CD554D4FDB4010BD57EA7FB0F64E3609971639209A4085D4900A53EE137B6C56DDAF7AC751FCDB7AE84FEA5FE838B193100A04D4438FBB3BFBE6CD9BB7B3BF7FFE8ACF9646470B2C8372762406BDBE88D0A62E53B618898AF2A30FE2ECF4FDBBF82233CBE8AE8B3B6EE2F8A60D23714FE44FA40CE93D1A083DA3D2D20597532F754642E6E4B0DFFF280703890C21182B8AE29BCA9232B85AF072EC6C8A9E2AD05397A10EED3E9F242BD4E81A0C060F298EC485F1548BE85C2BE0E409EA5C4460AD2320A676F22D6042A5B345E27903F46DED91E372D0015BCA27DBF043D9F7870D7BB9BDD841A55520675E0938386EE590FBD7DF24AAD8C8C5825DB0B054B3A004CA62B9526E2426A09B2D5C9288F67336928D75D90A4565C53AC96D07E4BA055DABE413BD8AA7E03D9B66A777ED4E94AC1B373E4A5E5F9E5963C8343C57E52613B9120ADC3BE5D4CCF452958126403087C609E3CC3C0ADB55695781167E23C15EA9719BF665EF3EE2B10E11D1D7D22D54D67048EA40687A4D402FF9A1C75AA1A56DC014ACCA31D0ADFB8E6C36F6D270EF2DFC3FBE942164FA0073FEA53BCFF8EFB1D8B1DC1D27F104832A187D67B8584C9B2AB6A05DCC95CD1D4BEBB1A43A41DAE5DA8574C72DD9291264CCF3BC2495434A7C9C62086C0711DD81AE56136A8ED9959D55E42B3A0F01CD5CD70F6B7A3EFFEA913DE41CCF7CB30AFFA204A6A9B8049CD94F95D2D986F7E57A46CA03209A967D46DE5FB983072EC315F506E9DAD903815AF926E8D166ECF45B345E335898D90416F8166E3CCEBE600169DDBD99A7415E6EC443D9E38982A204135A8CEDFDE68B94CD1F79FA077D338D7A55070000 , N'6.2.0-61023')
GO

CREATE TABLE [dbo].[Accounts] (
    [Id] [bigint] NOT NULL IDENTITY,
    [UserId] [uniqueidentifier],
    [IsActivate] [bit],
    [AccountName] [nvarchar](max),
    [Description] [nvarchar](max),
    [Email] [nvarchar](max),
    [Note] [nvarchar](max),
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_dbo.Accounts] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[EnumTypes] (
    [RowId] [int] NOT NULL IDENTITY,
    [Type] [nvarchar](max),
    [Id] [uniqueidentifier] NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_dbo.EnumTypes] PRIMARY KEY ([RowId])
)
CREATE UNIQUE INDEX [IX_Id] ON [dbo].[EnumTypes]([Id])
CREATE TABLE [dbo].[EnumValues] (
    [RowId] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [Value] [int] NOT NULL,
    [Id] [uniqueidentifier] NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Type_RowId] [int],
    CONSTRAINT [PK_dbo.EnumValues] PRIMARY KEY ([RowId])
)
CREATE UNIQUE INDEX [IX_Id] ON [dbo].[EnumValues]([Id])
CREATE INDEX [IX_Type_RowId] ON [dbo].[EnumValues]([Type_RowId])
CREATE TABLE [dbo].[Seeding] (
    [Id] [int] NOT NULL IDENTITY,
    [IsSeed] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Seeding] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[webpages_Membership] (
    [UserId] [int] NOT NULL,
    [CreateDate] [datetime],
    [ConfirmationToken] [nvarchar](128),
    [IsConfirmed] [bit],
    [LastPasswordFailureDate] [datetime],
    [PasswordFailuresSinceLastSuccess] [int] NOT NULL,
    [Password] [nvarchar](128) NOT NULL,
    [PasswordChangedDate] [datetime],
    [PasswordSalt] [nvarchar](128) NOT NULL,
    [PasswordVerificationToken] [nvarchar](128),
    [PasswordVerificationTokenExpirationDate] [datetime],
    CONSTRAINT [PK_dbo.webpages_Membership] PRIMARY KEY ([UserId])
)
CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider] [nvarchar](30) NOT NULL,
    [ProviderUserId] [nvarchar](100) NOT NULL,
    [UserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.webpages_OAuthMembership] PRIMARY KEY ([Provider], [ProviderUserId])
)
CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId] [int] NOT NULL IDENTITY,
    [RoleName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_dbo.webpages_Roles] PRIMARY KEY ([RoleId])
)
CREATE UNIQUE INDEX [IX_RoleName] ON [dbo].[webpages_Roles]([RoleName])
CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] [int] NOT NULL,
    [RoleId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.webpages_UsersInRoles] PRIMARY KEY ([UserId], [RoleId])
)
CREATE INDEX [IX_RoleId] ON [dbo].[webpages_UsersInRoles]([RoleId])
CREATE TABLE [dbo].[webpages_RolesInRoles] (
    [Child] [int] NOT NULL,
    [Parent] [int] NOT NULL,
    CONSTRAINT [PK_dbo.webpages_RolesInRoles] PRIMARY KEY ([Child], [Parent])
)
CREATE INDEX [IX_Child] ON [dbo].[webpages_RolesInRoles]([Child])
CREATE INDEX [IX_Parent] ON [dbo].[webpages_RolesInRoles]([Parent])
ALTER TABLE [dbo].[EnumValues] ADD CONSTRAINT [FK_dbo.EnumValues_dbo.EnumTypes_Type_RowId] FOREIGN KEY ([Type_RowId]) REFERENCES [dbo].[EnumTypes] ([RowId])
ALTER TABLE [dbo].[webpages_UsersInRoles] ADD CONSTRAINT [FK_dbo.webpages_UsersInRoles_dbo.webpages_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]) ON DELETE CASCADE
ALTER TABLE [dbo].[webpages_RolesInRoles] ADD CONSTRAINT [FK_dbo.webpages_RolesInRoles_dbo.webpages_Roles_Child] FOREIGN KEY ([Child]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
ALTER TABLE [dbo].[webpages_RolesInRoles] ADD CONSTRAINT [FK_dbo.webpages_RolesInRoles_dbo.webpages_Roles_Parent] FOREIGN KEY ([Parent]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201802161447559_vasiliy.stan_9dc0430cf2cd43eba6af748007b63d9e', N'Dal.Migrations.Configuration',  0x1F8B0800000000000400ED5DCD6EE4B811BE07C83B083A25C1ACDBF6EC2E3646F72EBC6D7B61646C0FA63D8BDC0C5A62B789E8A757A23C36827DB21CF248798590FAE5BF4849DDB61783B9B825F2AB6255B15814AB38FFFBCF7FE73F3DC591F708B31CA5C9C23F3A38F43D9804698892CDC22FF0FA9B1FFC9F7EFCF39FE6E761FCE4FDDAB47B4FDB919E49BEF01F30DE9ECC6679F00063901FC428C8D23C5DE383208D67204C67C787877F9F1D1DCD2081F00996E7CD3F150946312C7F909FCB3409E0161720BA4A4318E5F573F26655A27AD72086F9160470E19F81C8F74E230408E9158CD6BE079224C50013C64E3EE77085B334D9ACB6E401886E9FB790B45B83288735C3275D735BDE0F8F29EFB3AE63031514394E6347C0A3F7B5306662F74122F55B6111719D13B1E2673AEA52640BFF34085222EB52ACBE27123C5946196D5C4AF5806DFBCE234FDEB50A277641FFBDF39645848B0C2E1258E08CB6F858DC4728F8077CBE4DFF059345524411CB11E189BCE31E90471FB3740B33FCFC09AE6B3E2F43DF9BF1FD6662C7B61BD3A7E2FE32C1DF7FEB7BD78438B88F60AB7066A42B9C66F01798C00C60187E0418C32CA118B01499445DA0450C2BEBE8FD5220995F91BBFC34C0E891506B7AFD9CA61104495FC75A0DF447D393D834998FBE77059E3EC064831F163EF9D3F72ED0130C9B27F5983F27884C5FD2096705ECA37506F32043DBCAF8764CEB3C0628DA3995EB14EF5E6C9FD22FF5CC68758B1290112BA29EACC832E2429FAF4AB89216C7C20F6A060618EF328DB705868229CE679D1730FA86F3A488E94F2BE7C035DEB77720F21EE220EA6EAD8F787FBC531F41E9ECDCF44427248DE70D5B2E61F71A3CA24DD95560FC57101530F7BD4F302A5FE70F685B0500A56996AF4BDBBCABD47091A5F1A734AACD5C787D770BB20DC44404A9BECD2A2DB2008E985B1DA0CDE4EA5A7F9D5D2A727B590F4B25F48DE9EB04554DD06ADA4D333D9BA9679A9ECD141E343D5710865633B36DF8E602E21D4FC7CB9C8A460A6D8DD66EAD9E2FF07E0B3630BFBB82F13D913831A63E4529BAEC5B65CD1EC1556DFCDE62B8EAAED3A4D7BD2C3348FA9C31BB12FAF72D8AFB7BA6C91A6571C941292783333E3AD67822B75027AF692AECCCDCF303C8F14790E75FD22CBC20BB0EA2ED21431620F2154A0248B1574510C03C1FB75034E813C87120E5E5034836301C239A1588F0CB0D804C6DB446C11E6D524BF9FC698BB2F267BF38DDFDE0CD69811F063843A1DFBE3D2279F2884298895235361EEA463B625A1B787FB8136B1418D79BE0E14EE8DBAD1FE30C90C661B9B5D995ADF7BF8D89E0B07D4CD56F5F9113A5D7B39939FEEEFB490C451BAC2F1F501466D45F2A02765E8D771F01698873366657B79076D59A66AA8DB589D99681297915B7183D43B2E5B585A19332BF4CEA7963E29C6D79274E38C5380CEDF51A30751AF5A14333604B47C1767A1DE1FAA4AE659F91FD546E4C49ABDFDEF76CE8DAE96B333B6C0CFD34CFD3009563517ED2ABBE63F0523A4F42CFE2A346E5F5E54F23640920A68EB6C4B809430BFF6F921ACC04DAC9CF13603EF1F3F887070747A23098619BA5A171BC3A7EFBBC70C7B26818F632E95B9506D218221493A9F7B26F65F78AC1F01E7880DCAC56165B291EF9A25FBD49CE600431F4E8C1283D2C5F823C00A1EC6DC8940CF927C41543AA4D04A225712E641D400996FD36D99FA32D88068C4BC072580528BF2D65F1CD19DCC284C68503743C8EA596B220DA3E496AADBD7293A40F263D60D61C209730183E61C59A4F06592FFB791DB18A2648215710F327DFC4943A9F5CDB1A9FC72099328FD3783D1590E0112D909A93272514EBBD7BB0E8F7D232D0978098AFD13D18CA0FA3129EB2952DB2F4A9410F2F35B5A5513B0C3D72DDC0168FF77C7A58BE9D80CED8BDC200D8C30BA6A5E18C439C9276D1413B46DEF8A4096E170A0868F5A410DD2D3F720BA9E8765DB2606CC2049740811990D9585C22036BD031A23246BF06C1598712C38309D5F84D536578F43085A49B88BD5D8ADA77F359952E583F98CF347985F32BB0DD1257CCE419D64FBC559564B8FC66E59E8C175718B32057E4E4B5DCB694C8F68B8C5F784BCF59427881B21C9F010CEE01DDB42DC3586AC62CBC1A27D91012D65659938DB76C3AD0BFDBD59D5F79E598A4EE7441C612D338A7DC55326A3677F76886278840A638C95CA6511127BAD35053EF66DBCD22E876FB061E98043E8E17E6B93D1A97D5C7C2712FECF1B8CC3D168F7B618F5767E7B148F5237B8C2AF78E85A89ED823B0D90F2C0EFB5C469BCF042B94A261C9C8A50D073F69ACA654B7A2BACE293E08759F543DFD0DD215A78532F7C7845145402C44F5C4615E8D9ADB6FCE42EA086E8889309B8B61366202D8A591C85ECED5BDD589582C44FDE8ABA1290CADD95BBA5A59B7F374373043DFDD68A44939E217E3EAD9ABD1846AEFEDAA150B0C0BFD58A1EC368462B38D5824F6B9039A9C81C481CAAF5D6C8BC935E20D8C79618FA7CD4062B1B58DECE9F4A728B104FB5BBB5356531882C4A523A940B906EEF855AE920AB87AE38EA8C83F52C12B9A4D404BCC38B2A22C767A7D7E53FCA838D879F600B978D05E28ADF2DA9C244E3BDAB4A87E24956336674E9950DDDCFC4B9B86EEA397A54128BBBB988106401F415687337CF4A83EB031A3C8F174F7F4F5A989FB7438585B261417A59971761BFFB859C0AEF5C67F51D5EC54D9730ED38EB46EE2B6EBA49F8DC5EF4EFAD30B5940B61BD61245B36B2D4F971B8861FC690F68476EA88DAC9130344465AEC1654ED37ADA941EBB818B1FD49D2D4477E66372B65D2BB34FED3FD630A61D89A744D68A916767991839DA68CCE74D23D8AB905CF8EBD7BB749E223669FD527BAE229C9FCCEBB38CFECB1BA4C38DAA89EF3521CCC25F3DE718C607B4C1C1EAB76819A172C44D832B90A035CC7195FBE71F1F1E1D0BD741BC9EAB1966791E468AB320267D527F3CB28F32B17BB44154B8BD19CDA332D28B04FD56405482AD110D7B3B38BB3A20F126857B845D4114B72A248F200B1E40F697183CFDD5154F7173C2283CEE76845148EC0D08A380E452D42CFDF2D83CE92D1C9DA602C170D4B1BFEAE69D4C922EC61AA927D34C9372B22E93103E2DFC7F9328E273D9FAC4BB2534BCDF27A8547E21F3D0C59B6FDC3E2673555C517BC9EAE092F63F8C81A9A7E39DAC55FDF868B713EFF29F775DCF77DE4D46629413EF901FAEACA50195EA7B8F0E7662D57CDDB8B0904F5D333E4064D314710F996572697648FEC665F5A8DB84D7966A376E442E8C758DC8A4CAEC0121594F95F6D0D1DB566D0FD19158B36D23D0B125D963E5C096684FCEAFB6027B2A53732CB8B613D6F802EC019EE5458BA195EAA0E5D093D43AAB957DE80EEFEA458716320F8A13A7282FDEC99A2A561477EA7089CEC4FAE38905AFFF0EFF4ACB3587AC0F0A655B0490552F5DF0389DD98F9F04BACFA30A07A6FC52D9AF839A829B00CB4E0EF2532D3315BB6E74AB5E13286E7CB52953D334BCFE534265CB9B46D4AD0E2ADFEB4B92B5DCCA7B2EF579BD59973251F359D3E46578B695B8CAB3106D498C535DA76D05AE3509D792DF71D5A0BA13E5DD167C5A9C733979DC17B42CD531D657CB7A7396A53F4BDCA7698DAA9477B73071AC236AD27B4AE0A6A8D17F8BE6D893B7F30A6BD8E58A3B51D1CA32F53AF6E9023A65A97A75C0BDF0C3FB94584415187675EE9685EC3A52C27B152D7DD9AF8A181BD429A9B10D74E4D435CBCA7A781D29E69D8A4A5B4ABFBB727915D917ABAB3732B3F32A7C23F5BD54EA1B393016F59B07EF280A77C1BC8EBB062417A22822B4BF61407D4781FAFE8AA9AE16684BD56D6F18D0A4821982087DC2AFFDCD02D698BBBB66407DC381B5D8D4296A7F7CB1BDD0ED0CD30851E554ED6F7398E6AA05390590C469CCFFF24402C61C6D3A089ACF9AC0808BD0DA3697C93A6D024581A3A689F0FDEE0A621092F0ED34C3680D024C5ED3B3BE324828FD13CDB0BA87E1657253E06D81C990C98A19719775D280D344BFBC4F82E7797E536680E5530C81B089E8F1D54DF27351EEF96BBE2F149F1135103492ADCF15A82E313D5FD83CB748F2D57E3AA05A7C6D007E0BE36D44C0F29B64051EE110DE88097E801B103C7747533A907E45F0629F9F21B0C9409CD7185D7FF293D870183FFDF87F61CEE53FEC6C0000 , N'6.2.0-61023')
GO