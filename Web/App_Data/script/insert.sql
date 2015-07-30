INSERT INTO [dbo].[Nt_Language]([LanguageCode],[Published],[DisplayOrder],[Name])VALUES('cn',1,1,'简体中文');

INSERT INTO [dbo].[Nt_UserLevel]([Name],[IsAdmin],[Active],[Note])VALUES('admin',1,1,'');

INSERT INTO [dbo].[Nt_User]([UserName],[Password],[Email],[UserLevelId],[LastLoginIp],[LoginTimes],[LastLoginDate],[CreatedDate],[CreatedUserId],[Active],[Deleted],[Profile])VALUES('zxh','670B14728AD9902AECBA32E22FA4F6BD','1210372315@qq.com',1,':7125',0,getdate(),getdate(),0,1,0,'/upload/201412/201412191049185302.jpg');

INSERT INTO [dbo].[Nt_CustomerRole]([Name],[MeetPoints],[Note],[Active])VALUES('common',20,'',1)
INSERT INTO [dbo].[Nt_Customer]([CustomerRoleId],[Name],[Password],[Email],[Points],[CreatedDate],[RealName],[Address],[Phone],[Mobile],[Zip],[Active],[BirthDay],[Gender],[LastLoginIP],[LastLoginDate],[LoginTimes])     VALUES(1,'zxh','670B14728AD9902AECBA32E22FA4F6BD','1210372315@qq.com',0,GETDATE(),N'张星海','naite','0411-82527872','18742538743','116600',1,'1989-02-08',1,'192.168.17.1',GETDATE(),230)