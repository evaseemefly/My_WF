select * from ActionInfo a
where a.ID in(select ActionInfoID from dbo.R_UserInfo_ActionInfo where UserInfoID=19)