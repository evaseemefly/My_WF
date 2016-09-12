select ActionInfoName from dbo.ActionInfo where ID in(
select ActionInfoID from dbo.R_UserInfo_ActionInfo 
where ActionInfoID in(
select ActionInfo_ID from dbo.RoleInfoActionInfo 
where RoleInfo_ID in(
select RoleInfo_ID from dbo.UserInfoRoleInfo where UserInfo_ID=19)
))
